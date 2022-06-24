using Refit;
using System.ComponentModel.DataAnnotations;

namespace Template.Domain.Commands
{
    public class EndpointLabWareCommand
    {
        public string senderParty { get; set; }
        public string senderService { get; set; }
        public string receiverParty { get; set; }
        public string receiverService { get; set; }

        [AliasAs("interface")]
        public string Interface { get; set; }
        public string CONTROLECLIENTE { get; set; }
        public string interfaceNamespace { get; set; }

        public EndpointLabWareCommand(string EndPoint)
        {
            switch (EndPoint)
            {
                case "AUTENTICACAO2":
                    senderService = "BC_LABS_PARCEIROS";
                    Interface = "SI_AUTENTICACAO_LABS_PARCEIROS_OUT";
                    interfaceNamespace = "http://www.brf.com/labs_parceiros/AUTENTICACAO2";
                    break;
                
                case "RESULTADOS":
                    senderService = "BC_LABS_PARCEIROS";
                    Interface = "SI_RESULTADOS_LABS_PARCEIROS_OUT";
                    interfaceNamespace = "http://www.brf.com/labs_parceiros/RESULTADOS";
                    break;

                case "CLOSE":
                    senderService = "BC_LABS_PARCEIROS";
                    Interface = "SI_CLOSE_LABS_PARCEIROS_OUT";
                    interfaceNamespace = "http://www.brf.com/labs_parceiros/CLOSE";
                    break;

                default:
                    throw new ValidationException($"EndPoint {EndPoint} não implementado.");
            }
        }
    }
}