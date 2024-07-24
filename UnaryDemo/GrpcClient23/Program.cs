using Grpc.Net.Client;
using gRPCService;
using System;

namespace GrpcClient23
{
    public class Program(string[] args)
    {
        static async Task Main()
        {
            var sampleReq = new SampleRequest()
            {
                FirstName = "arash",
                LastName = "ashrafi"
            };
            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:5269");
                var client = new Sample.SampleClient(channel);

                var reply = await client.GetFullNAmeAsync(sampleReq);

                Console.WriteLine(reply.Message);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();

        }
    }
}