using CosmosDBExemple.Data;
using WebAPIMicrosservico.Data;
using WebAPIMicrosservico.Features.User.Domain.Models;
using WebAPIMicrosservico.Features.User.Domain.Repository;
using WebAPIMicrosservico.Services.Queue;
using WebAPIMicrosservicoConsumer.Services.Grpc;

namespace WebAPIMicrosservico.Features.User.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        public string messageNotSend = "Objeto não enviado.";
        public string container = "WebAPIMicro";
        private readonly INoSqlDatabase<UserModel> noSQLDataBase;
        private readonly IQueueService queueService;
        private readonly IContractWebAPIClient contractWebAPIClient;
        

        // Construtor da classe  que recebe IQueueService da injeção de dependência e inicializa a instância de NoSQLDatabase
        public UserRepository(INoSqlDatabase<UserModel> noSQLDatabase, IQueueService queueService, IContractWebAPIClient contractWebAPIClient)
        {
            this.noSQLDataBase = noSQLDatabase;
            this.queueService = queueService;
            this.contractWebAPIClient = contractWebAPIClient;
        }

        public async Task<string> SubmitUser(UserModel userModel)
        {
            var userResponse = await this.contractWebAPIClient.MessageGrpc(userModel);
            if (userResponse.Message == "Objeto enviado com sucesso.")
            {
                // Adiciona o objeto userModel à base de dados NoSQL
                await noSQLDataBase.Add(container, userModel, userModel.Id.ToString());
                // Envia uma mensagem do objeto userModel para a fila
                await this.queueService.SendMessageQueue(userModel);
                return userResponse.Message;
            }
            return messageNotSend;
        }
    }
}
