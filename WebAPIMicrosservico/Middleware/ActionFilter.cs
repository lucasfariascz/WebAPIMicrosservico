using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebAPIMicrosservico.Middleware
{
    public class ActionFilter : IActionFilter
    {
        private Stopwatch _stopwatch;

        public ActionFilter()
        {
            // Inicializa o stopwatch no construtor
            _stopwatch = new Stopwatch();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();

            var time = _stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Tempo de execução da ação: {time} ms");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();
        }
    }
}
