using Grpc.Net.Client;
using gRPCService;
using System;

namespace GrpcClient23
{
    public class Program(string[] args)
    {
        static async Task Main()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5269");
            var sampleReq = new SampleRequest()
            {
                FirstName = "arash",
                LastName = "ashrafi"
            };
            var productReq = new ProductModel()
            {
                ProductName = "laptop",
                ProductCode = "2",
                Price = 12.0
            };
            try
            {
                //client1
                var client = new Sample.SampleClient(channel);
                var reply = await client.GetFullNAmeAsync(sampleReq);
                Console.WriteLine(reply.Message);

                //client2
                var client2 = new Product.ProductClient(channel);
                var reply2 = await client2.saveProductAsync(productReq);
                Console.WriteLine(reply2.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                await channel.ShutdownAsync();
            }

            Console.ReadLine();

        }
    }
}