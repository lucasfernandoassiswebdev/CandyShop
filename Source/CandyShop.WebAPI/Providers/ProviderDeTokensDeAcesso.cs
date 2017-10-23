using CandyShop.Core.Services.Usuario;
using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CandyShop.WebAPI.Providers
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ProviderDeTokensDeAcesso(IUsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Encontrando o usuário
            var user = _usuarioRepository.ListarUsuario().FirstOrDefault(x => x.Cpf == context.UserName);

            // Cancelando a emissão do token se o usuário não for encontrado
            if (user == null)
            {
                context.SetError("invalid_grant", "Usuário não encontrado ou você não tem acesso aos tokens.");
                return;
            }

            // Emitindo o token com informações extras se o usuário existe
            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identidadeUsuario);
        }
    }
}