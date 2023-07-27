using CosmosDBExemple.Data;
using WebAPIMicrosservico.Features.User.Domain.Models;
using WebAPIMicrosservico.Features.User.Domain.Repository;
using WebAPIMicrosservico.Services.Queue;

namespace WebAPIMicrosservico.Features.User.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NoSQLDatabase<UserModel> noSQLDataBase;
        public string container = "WebAPIMicro";
        private readonly IQueueService queueService;

        // Construtor da classe  que recebe IQueueService da injeção de dependência e inicializa a instância de NoSQLDatabase
        public UserRepository(IQueueService queueService)
        {
            this.noSQLDataBase = new();
            this.queueService = queueService;
        }

        public async Task<UserModel> SubmitUser(UserModel userModel)
        {
            // Adiciona o objeto userModel à base de dados NoSQL
            await this.noSQLDataBase.Add(container, userModel, userModel.Id.ToString());
            // Envia uma mensagem do objeto userModel para a fila
            await this.queueService.SendMessageQueue(userModel);
            return userModel;
        }
    }
}
