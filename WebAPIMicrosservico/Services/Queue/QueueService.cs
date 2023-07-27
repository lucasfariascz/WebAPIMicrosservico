using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;
using WebAPIMicrosservico.Config.ServiceBus;
using WebAPIMicrosservico.Features.User.Domain.Models;

namespace WebAPIMicrosservico.Services.Queue
{
    public class QueueService : IQueueService
    {
        private static readonly string QueueName = AppSettings.QueueName;
        private static readonly string AzureServiceBus = AppSettings.AzureServiceBus;

        // Implementação do método SendMessageQueue da interface IQueueService
        public async Task SendMessageQueue(UserModel userModel)
        {
            // Cria uma nova instância de QueueClient para se conectar na fila no serviço do Azure
            var client = new QueueClient(AzureServiceBus, QueueName, ReceiveMode.PeekLock);

            // Converte o objeto UserModel em uma representação JSON(string) usando o JsonSerializer.
            string messageBody = JsonSerializer.Serialize(userModel);

            // Cria uma nova instância de Message para representar a mensagem a ser enviada para a fila
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            // Envia a mensagem para a fila
            await client.SendAsync(message);

            // Fecha a conexão com a fila
            await client.CloseAsync();
        }
    }
}
