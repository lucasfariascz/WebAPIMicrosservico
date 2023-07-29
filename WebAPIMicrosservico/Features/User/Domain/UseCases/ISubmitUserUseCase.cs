using WebAPIMicrosservico.Features.User.Controllers.dto;

namespace WebAPIMicrosservico.Features.User.Domain.UseCases
{
    public interface ISubmitUserUseCase
    {
        Task<string> SubmitUser(SubmitUserDTO submitUserDTO);
    }
}
