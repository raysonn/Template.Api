using Template.Domain.Commands.Amostras;
using Template.Domain.Commands.Resultados;

namespace Template.Domain.Interfaces.Service
{
    public interface ILabWareService
    {
        Task<string> Auth();
        Task<string> Resultados(AmostrasCommand command);
        Task<bool> Close(string authToken);
        Task<bool> SendSampleList(List<SampleCommand> command);
    }
}