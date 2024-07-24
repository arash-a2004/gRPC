using Grpc.Core;

namespace gRPCService.Services
{
    public class SampleService : Sample.SampleBase
    {
        private readonly ILogger<SampleService> _logger;

        public SampleService(ILogger<SampleService> logger)
        {
            _logger = logger;
        }

        public override async Task<SampleRespond> GetFullNAme(SampleRequest request, ServerCallContext context)
        {
            string res = $"{request.FirstName} {request.LastName}";
            return new SampleRespond { Message = res};
        }
    }
}
