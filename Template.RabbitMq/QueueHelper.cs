using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Refit;
using System.Text;

namespace Template.RabbitMq
{
    public class QueueHelper
    {
        private string FullExchangeName;
        private string FullQueueName;
        private string FullQueueNameError;
        private ushort PrefetchCount;
        private int RetryCount;
        private int RetryTime;

        ///// <summary>
        ///// Inicializa a fila e a exchange.
        ///// Tanto exchange quanto fila devem ser nomes unicos.
        ///// Estrutura usando Topicos.
        ///// </summary>
        ///// <param name="exchangeName">Obrigatório</param>
        ///// <param name="queueName">Obrigatório</param>
        //public QueueHelper(string exchangeName, string queueName)
        //{
        //    FullExchangeName = RabbitMqConfig.Ambiente + "." + exchangeName;
        //    FullQueueName = RabbitMqConfig.Ambiente + "." + queueName;
        //    FullQueueNameError = RabbitMqConfig.Ambiente + "." + queueName + ".Error";
        //    Validacoes();
        //}

        /// <summary>
        /// Inicializa a fila e a exchange.
        /// Estrutura usando Topicos.
        /// </summary>
        /// <param name="queueName">Obrigatório</param>
        public QueueHelper(string queueName)
        {
            FullExchangeName = RabbitMqConfig.Ambiente + "." + queueName;
            FullQueueName = RabbitMqConfig.Ambiente + "." + queueName;
            FullQueueNameError = RabbitMqConfig.Ambiente + "." + queueName + ".Error";
            Validacoes();
        }

        /// <summary>
        /// Cria o channel e as configuracoes necessarias para a mensageria funcionar.
        /// Utilizado no publish como parametro secundario em casos de alto fluxo de mensagens.
        /// </summary>
        /// <returns></returns>
        public IModel Configs(int tentativas = 0)
        {
            try
            {
                Validacoes();

                var factory = new ConnectionFactory()
                {
                    HostName = RabbitMqConfig.HostName,
                    UserName = RabbitMqConfig.UserName,
                    Password = RabbitMqConfig.Password
                };

                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.ExchangeDeclare(FullExchangeName, ExchangeType.Topic, true);
                channel.QueueDeclare(FullQueueName, true, false, false);
                channel.QueueDeclare(FullQueueNameError, true, false, false);
                channel.QueueBind(FullQueueName, FullExchangeName, RabbitMqConfig.RoutingKey);
                channel.QueueBind(FullQueueNameError, FullExchangeName, "Error");
                channel.BasicQos(0, PrefetchCount, true);

                return channel;
            }
            catch (BrokerUnreachableException)
            {
                if (tentativas <= RetryCount)
                {
                    Thread.Sleep(RetryTime * RetryCount);
                    tentativas++;
                    return Configs(tentativas);
                }
                else
                    throw;
            }
        }

        /// <summary>
        /// Publica uma mensagem na fila.
        /// Caso for disparos em lote com alta demanda passar o channel como segundo parametro.
        /// </summary>
        /// <param name="obj">Obrigatório</param>
        /// <param name="channelInjected">Opcional</param>
        public void Publish(dynamic obj, IModel channelInjected = null)
        {
            var channel = channelInjected ?? Configs();
            string message = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(message);
            IBasicProperties props = SetHeaders(channel);
            channel.BasicPublish(FullExchangeName, RabbitMqConfig.RoutingKey, props, body);
        }

        /// <summary>
        /// Vincula o consumo das mensagens a um metodo.
        /// </summary>
        /// <param name="func">Obrigatório</param>
        public void Consume(Func<string, Task<bool>> func)
        {
            var channel = Configs();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, ea) => await OnNewMessageReceived(func, model, ea, channel, 0);
            channel.BasicConsume(FullQueueName, false, consumer);
        }

        private async Task OnNewMessageReceived(Func<string, Task<bool>> func, object sender, BasicDeliverEventArgs eventArgs, IModel channel, int tentativas)
        {
            string message = "";
            try
            {
                message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var consumiu = await func(message);

                if (!consumiu)
                    await Retry(func, sender, eventArgs, channel, tentativas, new Exception("Consumiu - " + consumiu));
                else
                    channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception error)
            {
                switch (error)
                {
                    case ApiException e:
                        await Retry(func, sender, eventArgs, channel, tentativas, e); break;
                    case BrokerUnreachableException e:
                        await Retry(func, sender, eventArgs, channel, tentativas, e); break;
                    default:
                        await Retry(func, sender, eventArgs, channel, tentativas, error); break;
                }
            }
        }

        private async Task Retry(Func<string, Task<bool>> func, object sender, BasicDeliverEventArgs eventArgs, IModel channel, int tentativas, Exception e)
        {
            tentativas++;
            Thread.Sleep(RetryTime);

            if (tentativas <= RetryCount)
                await OnNewMessageReceived(func, sender, eventArgs, channel, tentativas);
            else
            {
                var errorObj = new { payload = Encoding.UTF8.GetString(eventArgs.Body.ToArray()) };
                string errorMessage = JsonConvert.SerializeObject(errorObj);
                var body = Encoding.UTF8.GetBytes(errorMessage);
                IBasicProperties props = SetHeaders(channel, e);
                channel.BasicPublish(FullExchangeName, "Error", props, body);
                channel.BasicAck(eventArgs.DeliveryTag, false);
            }
        }

        private IBasicProperties SetHeaders(IModel channel, Exception e = null)
        {
            IBasicProperties props = channel.CreateBasicProperties();
            props.Headers = new Dictionary<string, object> { { "Date", DateTime.Now.ToString() } };

            if (e != null)
                props.Headers.Add("Error", GetExceptionMessages(e));

            return props;
        }

        private void Validacoes()
        {
            if (string.IsNullOrEmpty(FullExchangeName))
                throw new ArgumentException("exchangeName nullo ou em branco.");

            if (string.IsNullOrEmpty(FullQueueName))
                throw new ArgumentException("queueName nullo ou em branco.");

            if (!ushort.TryParse(RabbitMqConfig.PrefetchCount, out PrefetchCount))
                throw new ArgumentException("PrefetchCount Invalido.");

            if (!int.TryParse(RabbitMqConfig.RetryTime, out RetryTime))
                throw new ArgumentException("RetryTime Invalido.");

            if (!int.TryParse(RabbitMqConfig.RetryCount, out RetryCount))
                throw new ArgumentException("RetryCount Invalido.");
        }

        private string GetExceptionMessages(Exception e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += "\r\nInnerException: " + GetExceptionMessages(e.InnerException);
            return msgs;
        }
    }

}