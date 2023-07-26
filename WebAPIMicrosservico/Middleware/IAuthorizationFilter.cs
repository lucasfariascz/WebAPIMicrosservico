using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIMicrosservico.Middleware
{
    public interface IAuthorizationFilter
    {
        OnAuthorization(AuthorizationFilterContext context);
    }
}
