using CosmosDBExemple.Data;
using WebAPIMicrosservico.Features.User.Controllers.dto;
using WebAPIMicrosservico.Features.User.Domain.Models;
using WebAPIMicrosservico.Features.User.Domain.Repository;
using WebAPIMicrosservico.Services;

namespace WebAPIMicrosservico.Features.User.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NoSQLDatabase<UserModel> _noSQLDataBase;
        public string container = "WebAPIMicro";
        private readonly IQueueService _queueService;

        public UserRepository(IQueueService queueService)
        {
            _noSQLDataBase = new();
            _queueService = queueService;
        }

        public async Task<UserModel> SubmitUser(UserModel userModel)
        {
            await _noSQLDataBase.Add(container, userModel, userModel.Id.ToString());
            await _queueService.SendMessageQueue(userModel);
            return userModel;
        }

        
    }
}
