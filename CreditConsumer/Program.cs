using CreditService.Protos;
using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CreditConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Threading.Thread.Sleep(5000);
            bool repeat = true;
            while (repeat)
            {
                var channel = GrpcChannel.ForAddress("http://localhost:5000");
                Console.WriteLine("Inform credit limit");
                int limit = Convert.ToInt32(Console.ReadLine());
                var client = new CreditServiceCheck.CreditServiceCheckClient(channel);
                var creditRequest = new CreditRequest { CustomerId = "id0201", Credit = limit };
                var reply = await client.CheckCreditRequestAsync(creditRequest);
                Console.WriteLine($"Credit for customer {creditRequest.CustomerId} {(reply.IsAccepted ? "aproved" : "rejected")}!");                
                Console.WriteLine("press 1 to continue");
                repeat = Console.ReadLine().Trim() == "1";
            }
        }
    }   
}
