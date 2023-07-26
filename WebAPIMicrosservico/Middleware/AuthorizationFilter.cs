using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
namespace WebAPIMicrosservico.Middleware
{
    public class AuthorizationFilter : IAuthorizationFilter
    {

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
            // Por simplicidade, neste exemplo, estamos comparando o token recebido com um token fixo.
            return string.Equals(receivedToken.Substring(7), "986ghgrgtru989ASdsaerew13434545435", StringComparison.OrdinalIgnoreCase);
        }
    }
}
