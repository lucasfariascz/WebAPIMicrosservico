using WebAPIMicrosservico.Features.User.Domain.Models;

namespace WebAPIMicrosservico.Services
{
    public interface IQueueService
    {
        Task SendMessageQueue(UserModel userModel);
    }
}
