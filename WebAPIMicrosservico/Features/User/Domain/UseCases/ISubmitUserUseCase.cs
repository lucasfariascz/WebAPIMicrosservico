using WebAPIMicrosservico.Features.User.Controllers.dto;
using WebAPIMicrosservico.Features.User.Domain.Models;

namespace WebAPIMicrosservico.Features.User.Domain.UseCases
{
    public interface ISubmitUserUseCase
    {
        Task<UserModel> SubmitUser(SubmitUserDTO submitUserDTO);
    }
}
