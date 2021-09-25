using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using ProductGrpc.Protos;
using System;
using System.Threading.Tasks;

namespace ProductGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new ProductProtoService.ProductProtoServiceClient(channel);


            await GetProductAsync(client);
            await GetAllProductsAsync(client);
            await AddProductAsync(client);
           
            Console.ReadLine();
        }

        

        //Get Product Async
        private static async Task GetProductAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            Console.WriteLine("GetProductAsync started...");

            var response = await client.GetProductAsync(
                new GetProductRequest
                {
                    ProductId = 1
                });
            Console.WriteLine("GetProductAsync Response :" + response.ToString());
        }

        //GetAllProducts
        private static async Task GetAllProductsAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            
            //Console.WriteLine("GetAllProduct started...");
            //using (var clientData = client.GetAllProducts(new GetAllProdctsRequest()))
            //{
            //    while (await clientData.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
            //    {
            //        var currentProduct = clientData.ResponseStream.Current;
            //        Console.WriteLine(currentProduct);
            //    }
            //}

            //GetAllProducts with C# 9
            Console.WriteLine("GetAllProduct with C# 9 started...");

            using var clientData = client.GetAllProducts(new GetAllProdctsRequest());
            await foreach (var responseData in clientData.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(responseData);
            }

        }

        private static async Task AddProductAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            Console.WriteLine("AddProductAsync started..");
            var addProductResponse = await client.AddProductAsync(
                    new AddProductRequest
                    {
                        Product = new ProductModel
                        {
                            ProductCode = "P-1004",
                            Name = "Red",
                            Description = "New Red Phone Mi10T",
                            Price = 699,
                            Status = ProductStatus.Instock,
                            CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                        }
                    }
                );
            Console.WriteLine("AddProduct Response: " + addProductResponse.ToString());
        }

    }
}
