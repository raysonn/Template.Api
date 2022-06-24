using AmostrasCommand = Template.Domain.Commands.Resultados.AmostrasCommand;

namespace Template.Domain.Interfaces.Service
{
    public interface ILabWareService
    {
        Task<string> Auth();
        Task<string> Resultados(AmostrasCommand command);
        Task<bool> Close(string authToken);
        Task<bool> SendSampleList(List<Commands.Amostras.AmostrasCommand> command);
    }
}