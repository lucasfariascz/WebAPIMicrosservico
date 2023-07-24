using CosmosDBExemple.Data;
using WebAPIMicrosservico.Features.User.Controllers.dto;
using WebAPIMicrosservico.Features.User.Domain.Models;
using WebAPIMicrosservico.Features.User.Domain.Repository;

namespace WebAPIMicrosservico.Features.User.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NoSQLDatabase<UserModel> _noSQLDataBase;

        public string container = "WebAPIMicro";

        public UserRepository()
        {
            _noSQLDataBase = new();
        }

        public async Task<UserModel> SubmitUser(UserModel userModel)
        {
            await _noSQLDataBase.Add(container, userModel, userModel.Id.ToString());
            return userModel;
        }

        
    }
}
