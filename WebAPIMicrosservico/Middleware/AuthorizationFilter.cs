using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
namespace WebAPIMicrosservico.Middleware
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly string token;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Verifica se o token está presente no cabeçalho da requisição
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedResult(); // Retorna erro 401
                return;
            }

            // Obtém o token do cabeçalho da requisição
            string receivedToken = context.HttpContext.Request.Headers["Authorization"].ToString();

            // Valida o token enviado com o token esperado
            if (!IsValidToken(receivedToken))
            {
                context.Result = new UnauthorizedResult(); // Retorna erro 401
                return;
            }
        }

        private bool IsValidToken(string receivedToken)
        {
            // Aqui você pode implementar a lógica de validação do token
            // Pode ser uma chamada a um serviço de autenticação, verificação em banco de dados, etc.
            // Por simplicidade, neste exemplo, estamos comparando o token recebido com um token fixo.
            return string.Equals(receivedToken.Substring(7), "986ghgrgtru989ASdsaerew13434545435", StringComparison.OrdinalIgnoreCase);
        }
    }
}
