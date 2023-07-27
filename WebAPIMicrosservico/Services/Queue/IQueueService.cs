using WebAPIMicrosservico.Features.User.Domain.Models;

namespace WebAPIMicrosservico.Services.Queue
{
    public interface IQueueService
    {
        Task SendMessageQueue(UserModel userModel);
    }
}
