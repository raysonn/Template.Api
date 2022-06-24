using Refit;
using Template.Domain.Commands;

namespace Template.Domain.Interfaces.Api
{
    public interface ILabWareApi
    {
        [Post("/XISOAPAdapter/MessageServlet")]
        Task<ViewModels.Auth.Envelope> Auth(EndpointLabWareCommand command, [Body(BodySerializationMethod.Serialized)] Commands.Auth.Envelope envelope, [Header("Authorization")] string token);
        
        [Post("/XISOAPAdapter/MessageServlet")]
        Task<ViewModels.Resultados.Envelope> Resultados(EndpointLabWareCommand command, [Body(BodySerializationMethod.Serialized)] Commands.Resultados.Envelope envelope, [Header("Authorization")] string token);

        [Post("/XISOAPAdapter/MessageServlet")]
        Task<ViewModels.Close.Envelope> Close(EndpointLabWareCommand command, [Body(BodySerializationMethod.Serialized)] Commands.Close.Envelope envelope, [Header("Authorization")] string token);
    }
}
