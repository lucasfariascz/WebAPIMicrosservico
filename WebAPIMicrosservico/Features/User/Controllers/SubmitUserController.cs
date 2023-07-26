using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebAPIMicrosservico.Features.User.Controllers.dto;
using WebAPIMicrosservico.Features.User.Domain.UseCases;
using WebAPIMicrosservico.Middleware;

namespace WebAPIMicrosservico.Features.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitUserController : ControllerBase
    {
        private readonly ISubmitUserUseCase submitUserUseCase;

        // Construtor da classe que recebe ISubmitUserUseCase da injeção de dependência 
        public SubmitUserController(ISubmitUserUseCase submitUserUseCase)
        {
            this.submitUserUseCase = submitUserUseCase;
        }

        [HttpPost]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public async Task<IActionResult> Post([FromBody] SubmitUserDTO submitUserDTO)
        {
            // Chama o método SubmitUser da instância de ISubmitUserUseCase
            var result = await this.submitUserUseCase.SubmitUser(submitUserDTO);

            return Ok(result);
        }
    }
}
