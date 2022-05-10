using System.Configuration;
using System.Reflection;

namespace Template.RabbitMq
{
    public static class RabbitMqConfig
    {
        public static string Ambiente;
        public static string HostName;
        public static string UserName;
        public static string Password;
        public static string RoutingKey;
        public static string PrefetchCount;
        public static string RetryTime;
        public static string RetryCount;

        private static void SetEnviromentVariables(string ambiente)
        {
            Ambiente = ambiente.ToLower();

            if (string.IsNullOrEmpty(Ambiente))
                throw new ArgumentException("Ambiente não cadastrado no config");

            RoutingKey = "P0";
            PrefetchCount = "25";
            RetryTime = "600";
            RetryCount = "3";

            switch (Ambiente)
            {
                case "dev":
                    HostName = "Dev-queue";
                    UserName = "admDEV";
                    Password = "senhaDEV";
                    break;

                case "qa":
                    HostName = "QA-queue";
                    UserName = "admQA";
                    Password = "senhaQA";
                    break;

                case "prod":
                    HostName = "Prod-queue";
                    UserName = "admProd";
                    Password = "senhaPROD";
                    break;

                default:
                    throw new ArgumentException("Ambiente invalido cadastrado no config");
            }
        }

        /// <summary>
        ///  Carrega configs do rabbit em um WS .net framework
        ///  Usado somente em projetos antigos da V3
        /// </summary>
        public static void SetEnviromentVariablesWS() => SetEnviromentVariables(ConfigurationManager.AppSettings["Ambiente"]);

        /// <summary>
        ///  Carrega configs do rabbit em uma API .net core.
        ///  Chamar no startup após a configuração interna de configs
        /// </summary>
        public static void SetEnviromentVariablesApi() => SetEnviromentVariables(Environment.GetEnvironmentVariable("Ambiente"));

        public static void SetEnviromentVariablesFromConfig()
        {
            FieldInfo[] Fields = typeof(RabbitMqConfig).GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var field in Fields)
            {
                var value = Environment.GetEnvironmentVariable(field.Name);

                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(field.Name + " não cadastrado no config de " + Ambiente);
                else
                    field.SetValue(field, value);
            }
        }
    }

}