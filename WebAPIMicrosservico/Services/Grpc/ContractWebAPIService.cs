using Grpc.Core;
using GrpcService;

namespace WebAPIMicrosservico.Services.Grpc
{
    public class ContractWebAPIService : MicrosservicoService.MicrosservicoServiceBase
    {
        private readonly ILogger<ContractWebAPIService> _logger;
        public ContractWebAPIService(ILogger<ContractWebAPIService> logger)
        {
            _logger = logger;
        }

        public override Task<UserResponse> ContractWebAPI(UserRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserResponse
            {
                Message = "Objeto armazenado e mensagem enviada para a fila."
            });
        }
    }
}
