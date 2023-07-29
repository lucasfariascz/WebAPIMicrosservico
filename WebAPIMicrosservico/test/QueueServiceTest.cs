using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using WebAPIMicrosservico.Services.Queue;
using WebAPIMicrosservico.Features.User.Domain;
using WebAPIMicrosservico.Features.User.Domain.Models;

namespace WebAPIMicrosservico.Tests.Services.Queue
{
    [TestClass]
    public class QueueServiceTests
    {
        [TestMethod]
        public async Task SendMessageQueue_ShouldSendUserModelToQueue()
        {
            // Arrange
            var userModel = new UserModel
            {
                Id = "1",
                Name = "João Silva",
                Email = "john@example.com",
                Message = "Hello, World!"
            };

            var queueClientMock = new Mock<IQueueClient>();
            var queueService = new QueueService();

            // Substitui a instância de QueueClient criada pelo método SendMessageQueue pela implementação mock
            string messageBody = JsonSerializer.Serialize(userModel);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            queueClientMock.Setup(client => client.SendAsync(message));

            // Act
            await queueService.SendMessageQueue(userModel);

            // Assert
            // Verifica se o método SendAsync do QueueClient foi chamado uma vez com a mensagem correta
            queueClientMock.Verify(client => client.SendAsync(It.Is<Message>(message =>
                Encoding.UTF8.GetString(message.Body) == messageBody)), Times.Once);
            Assert.Equals(userModel.Id, "2");
        }
    }
    
}