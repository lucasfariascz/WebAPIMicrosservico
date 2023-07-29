using GrpcClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAPIMicrosservico.Features.User.Domain;
using WebAPIMicrosservico.Features.User.Domain.Models;
using WebAPIMicrosservico.Features.User.Infra.Repositories;
using WebAPIMicrosservico.Services.Queue;
using WebAPIMicrosservicoConsumer.Services.Grpc;

namespace WebAPIMicrosservico.Tests.Features.User.Infra.Repositories
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public async Task SubmitUser_ShouldReturnMessageNotSend_WhenContractWebAPIClientReturnsFailure()
        {
            // Arrange
            // Prepara o objeto UserModel para ser usado no teste
            var userModel = new UserModel
            {
                Id = "1",
                Name = "João Doe",
                Email = "joao@example.com",
                Message = "Olá, João!"
            };

            // Cria uma implementação mock de IContractWebAPIClient usando o Moq
            var contractWebAPIClientMock = new Mock<IContractWebAPIClient>();
            // Configura o método MessageGrpc do IContractWebAPIClient para retornar uma mensagem de falha simulada
            contractWebAPIClientMock.Setup(client => client.MessageGrpc(userModel)).ReturnsAsync(new UserResponse { Message = "Objeto não enviado." });

            // Cria uma implementação mock de IQueueService usando o Moq
            var queueServiceMock = new Mock<IQueueService>();

            // Cria uma instância da classe UserRepository com os mock IQueueService e  IContractWebAPIClient
            var userRepository = new UserRepository(queueServiceMock.Object, contractWebAPIClientMock.Object);

            // Act
            // Chama o método SubmitUser da classe UserRepository com o objeto UserModel preparado
            var result = await userRepository.SubmitUser(userModel);

            // Assert
            // Verifica se o resultado do método SubmitUser é igual à mensagem de falha simulada
            Assert.AreEqual("Objeto não enviado.", result);
        }

        [TestMethod]
        public async Task SubmitUser_ShouldReturnSuccessMessage_WhenContractWebAPIClientReturnsSuccess()
        {
            // Arrange
            // Prepara o objeto UserModel para ser usado no teste
            var userModel = new UserModel
            {
                Id = "1",
                Name = "João Silva",
                Email = "joao@example.com",
                Message = "Olá, João!"
            };

            // Cria uma implementação mock de IContractWebAPIClient usando o Moq
            var contractWebAPIClientMock = new Mock<IContractWebAPIClient>();
            // Configura o método MessageGrpc do IContractWebAPIClient para retornar uma mensagem de sucesso simulada
            contractWebAPIClientMock.Setup(client => client.MessageGrpc(userModel)).ReturnsAsync(new UserResponse { Message = "Objeto enviado com sucesso." });

            // Cria uma implementação mock de IQueueService usando o Moq
            var queueServiceMock = new Mock<IQueueService>();
            // Configura o método SendMessageQueue do IQueueService para retornar uma tarefa concluída
            queueServiceMock.Setup(queue => queue.SendMessageQueue(userModel)).Returns(Task.CompletedTask);

            // Cria uma instância da classe UserRepository com os mock IQueueService e  IContractWebAPIClient
            var userRepository = new UserRepository(queueServiceMock.Object, contractWebAPIClientMock.Object);

            // Act
            // Chama o método SubmitUser da classe UserRepository com o objeto UserModel preparado
            var result = await userRepository.SubmitUser(userModel);

            // Assert
            // Verifica se o resultado do método SubmitUser é igual à mensagem de sucesso simulada
            Assert.AreEqual("Objeto enviado com sucesso.", result);
        }
    }
}