using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebAPIMicrosservico.Features.User.Controllers.dto;
using WebAPIMicrosservico.Features.User.Domain.UseCases;
using WebAPIMicrosservico.Middleware;

namespace WebAPIMicrosservico.Features.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitUserController : ControllerBase
    {
        private readonly ISubmitUserUseCase _submitUserUseCase;

        public SubmitUserController(ISubmitUserUseCase submitUserUseCase)
        {
            _submitUserUseCase = submitUserUseCase;
        }

        [HttpPost]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public async Task<IActionResult> Post([FromBody] SubmitUserDTO submitUserDTO)
        {
            var result = await _submitUserUseCase.SubmitUser(submitUserDTO);

            return Ok(result);
        }
    }
}
