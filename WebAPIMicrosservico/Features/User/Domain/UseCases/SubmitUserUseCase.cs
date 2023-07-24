using WebAPIMicrosservico.Features.User.Controllers.dto;
using WebAPIMicrosservico.Features.User.Domain.Models;
using WebAPIMicrosservico.Features.User.Domain.Repository;

namespace WebAPIMicrosservico.Features.User.Domain.UseCases
{
    public class SubmitUserUseCase : ISubmitUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public SubmitUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> SubmitUser(SubmitUserDTO submitUserDTO)
        {
            var userModel = new UserModel
            {
                Id = submitUserDTO.Id,
                Name = submitUserDTO.Name,
                Email = submitUserDTO.Email,
                Message = submitUserDTO.Message,
            };
            var userModelResult = await _userRepository.SubmitUser(userModel);


            return userModelResult;
        }
    }
}
