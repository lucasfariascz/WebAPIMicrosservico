using WebAPIMicrosservico.Features.User.Controllers.dto;
using WebAPIMicrosservico.Features.User.Domain.Models;
using WebAPIMicrosservico.Features.User.Domain.Repository;

namespace WebAPIMicrosservico.Features.User.Domain.UseCases
{
    public class SubmitUserUseCase : ISubmitUserUseCase
    {
        private readonly IUserRepository userRepository;

        // Construtor da classe que recebe IUserRepository da injeção de dependência 
        public SubmitUserUseCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserModel> SubmitUser(SubmitUserDTO submitUserDTO)
        {
            // Cria uma nova instância de UserModel e recebe seus dados com base no submitUserDTO
            var userModel = new UserModel
            {
                Id = submitUserDTO.Id,
                Name = submitUserDTO.Name,
                Email = submitUserDTO.Email,
                Message = submitUserDTO.Message,
            };

            // Chama o método SubmitUser da instância de IUserRepository
            var userModelResult = await this.userRepository.SubmitUser(userModel);


            return userModelResult;
        }
    }
}
