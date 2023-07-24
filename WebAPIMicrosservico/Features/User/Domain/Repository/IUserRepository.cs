using WebAPIMicrosservico.Features.User.Controllers.dto;
using WebAPIMicrosservico.Features.User.Domain.Models;

namespace WebAPIMicrosservico.Features.User.Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> SubmitUser(UserModel userModel);
    }
}
