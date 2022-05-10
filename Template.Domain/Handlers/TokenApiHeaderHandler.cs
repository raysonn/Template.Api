using System.Net.Http.Headers;
using Template.Domain.Commands;
using Template.Domain.Interfaces.Api;

namespace Template.Domain.Handlers
{
    public class TokenApiHeaderHandler : DelegatingHandler
    {
        private readonly ITokenApi _tokenApi;

        public TokenApiHeaderHandler(ITokenApi tokenApi) => _tokenApi = tokenApi ?? throw new ArgumentNullException(nameof(tokenApi));

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenApi.GetToken(new TokenCommand() { PublicKey = "Chave Publica", PrivateKey = "Chave Privada" });

            if (!token.Success)
                throw new Exception("Erro ao gerar token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Data.Token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
