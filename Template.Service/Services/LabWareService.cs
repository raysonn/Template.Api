using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Template.Domain.Commands;
using Template.Domain.Commands.Amostras;
using Template.Domain.Commands.Resultados;
using Template.Domain.Interfaces.Api;
using Template.Domain.Interfaces.Service;
using Template.Domain.ViewModels.Resultados;
using Template.Service.Services;
using Template.Service.Validators.LabWare;

namespace LabWare.Service.Services
{
    public class LabWareService : BaseService, ILabWareService
    {
        private readonly ILabWareApi _labWareApi;

        public LabWareService
        (
            ILabWareApi labWareApi
        )
        {
            _labWareApi = labWareApi;
        }

        public async Task<string> Auth()
        {
            var envelope = new Template.Domain.Commands.Auth.Envelope();
            var endPoint = new EndpointLabWareCommand("AUTENTICACAO2");
            var retorno = await _labWareApi.Auth(endPoint, envelope, GetToken());
            var auth = retorno.Body.authenticateResponse.@return;

            return auth;
        }

        public async Task<string> Resultados(AmostrasCommand command)
        {
            Validate(command, new ResultadosValidator());

            var envelope = new Template.Domain.Commands.Resultados.Envelope(command);
            var endPoint = new EndpointLabWareCommand("RESULTADOS");
            var retorno = await _labWareApi.Resultados(endPoint, envelope, GetToken());
            var json = retorno.Body.invokeResponse.@return;

            if (string.IsNullOrEmpty(json))
                throw new ValidationException("Retorno Nullo");

            json = json
                .Replace("False", "false")
                .Replace("True", "true");

            var jsonDeRetorno = JsonConvert.DeserializeObject<JsonRetorno>(json);

            if (!jsonDeRetorno.success)
                throw new ValidationException("Erro no jsonDeRetorno: " + jsonDeRetorno.msg);

            return jsonDeRetorno.msg;
        }

        public async Task<bool> Close(string authToken)
        {
            if (string.IsNullOrEmpty(authToken))
                throw new ValidationException("authToken Nullo ou em Branco");

            var envelope = new Template.Domain.Commands.Close.Envelope(authToken);
            var endPoint = new EndpointLabWareCommand("CLOSE");
            var retorno = await _labWareApi.Close(endPoint, envelope, GetToken());

            return retorno.Body.closeResponse.@return;
        }

        public async Task<bool> SendSampleList(List<SampleCommand> amostras)
        {
            Validate(amostras, new SamplesValidator());

            var authToken = await Auth();

            foreach (var amostra in amostras)
                await Resultados(new AmostrasCommand(amostra, authToken));

            return await Close(authToken);
        }

        private static string GetToken()
        {
            var userName = "SRV_API_LABEXT";
            var password = "r6AdYjrB";
            var token = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + password));

            return token;
        }
    }
}
