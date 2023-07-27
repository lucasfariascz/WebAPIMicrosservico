using Grpc.Net.Client;
using GrpcClient;
using WebAPIMicrosservico.Features.User.Domain.Models;

namespace WebAPIMicrosservicoConsumer.Services.Grpc
{
    public class ContractWebAPIClient : IContractWebAPIClient
    {
        public async Task<UserResponse> MessageGrpc(UserModel userModel)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7298");
            var client = new MicrosservicoService.MicrosservicoServiceClient(channel);
            var reply = await client.ContractWebAPIAsync(new UserRequest { Id = userModel.Id, Message = userModel.Message });
            Console.WriteLine(reply.Message);
            return reply;
        }
    }
}
