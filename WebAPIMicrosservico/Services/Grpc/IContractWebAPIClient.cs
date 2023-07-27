using GrpcClient;
using WebAPIMicrosservico.Features.User.Domain.Models;

namespace WebAPIMicrosservicoConsumer.Services.Grpc
{
    public interface IContractWebAPIClient
    {
        Task<UserResponse> MessageGrpc(UserModel userModel);
    }
}
