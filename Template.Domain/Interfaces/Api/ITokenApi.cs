using Refit;
using Template.Domain.Commands;
using Template.Domain.ViewModels;

namespace Template.Domain.Interfaces.Api
{
    public interface ITokenApi
    {
        [Post("/api/tokenJWT")]
        Task<ResponseApi<TokenViewModel>> GetToken([Body(BodySerializationMethod.Serialized)] TokenCommand command);
    }
}
