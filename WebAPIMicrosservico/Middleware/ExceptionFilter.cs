using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIMicrosservico.Shared.Errors;

namespace WebAPIMicrosservico.Middleware
{
    public class ExceptionFilter : IExceptionFilter
    {
        // Método OnException da interface IExceptionFilter
        public void OnException(ExceptionContext context)
        {
            // Cria um objeto Error para representar exceção
            var error = new Error
            {
                StatusCode = "500",
                Message = context.Exception.Message
            };

            context.Result = new JsonResult(error) { StatusCode = 500 };
        }
    }
}
