using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;
using WebAPIMicrosservico.Config.ServiceBus;
using WebAPIMicrosservico.Features.User.Domain.Models;

namespace WebAPIMicrosservico.Services
{
    public class QueueService : IQueueService
    {
        private static readonly string QueueName = AppSettings.QueueName;
        private static readonly string AzureServiceBus = AppSettings.AzureServiceBus;

        public async Task SendMessageQueue(UserModel userModel)
        {
            var client = new QueueClient(AzureServiceBus, QueueName, ReceiveMode.PeekLock);
            string messageBody = JsonSerializer.Serialize(userModel);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await client.SendAsync(message);
            await client.CloseAsync();
        }
    }
}
