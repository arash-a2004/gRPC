using Grpc.Net.Client;
using gRPCService;
using System;
namespace grpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5000");
            var client = new Sample.SampleClient(channel);
             var ressponse = await client.GetFullNAmeAsync(new SampleRequest { FirstName = "arash", LastName = "ashrafi" });            
            Console.WriteLine(ressponse.Message);
        }
    }
}