using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using ProductGrpc.Protos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // wait for grpc server is running
            Console.WriteLine("Waiting for server is running");
            Thread.Sleep(2000);

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new ProductProtoService.ProductProtoServiceClient(channel);


            await GetProductAsync(client);
            await GetAllProductsAsync(client);
            //await AddProductAsync(client);
            //await UpdateProductAsync(client);
            //await DeleteProductAsync(client);
            //await InsertProductAsync(client);
           
            Console.ReadLine();
        }

        private static async Task InsertProductAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            //InsertProductAsync
            Console.WriteLine("InsertBulkProduct started...");
            using var clientBulk = client.InsertBultProduct();

            for (int i = 0; i < 3; i++)
            {
                var productModel = new ProductModel
                {
                    ProductCode = $"P1001-{i}",
                    Name = $"{i}",
                    Description = "Bulk Insert product",
                    Price = 399,
                    Status = ProductStatus.Instock,
                    CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                };

                await clientBulk.RequestStream.WriteAsync(productModel);

            }

            await clientBulk.RequestStream.CompleteAsync();

            var responseBulk = await clientBulk;
            Console.WriteLine($"Status: { responseBulk.Success}. Insert Count: {responseBulk.InsertCount}");
        }

        private static async Task UpdateProductAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            //UpdateProductAsync
            Console.WriteLine("UpdateProductAsync started..");
            var updateProductResponse = await client.UpdateProductAsync(
                    new UpdateProductRequest
                    {
                        Product = new ProductModel
                        {
                            Id = 1,
                            Name = "Red",
                            Description = "New Red Phone Mi10T",
                            Price = 699,
                            Status = ProductStatus.Instock,
                            CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                        }
                    }
                );
            Console.WriteLine("UpdateProductAsync Response: "+updateProductResponse.ToString());
        }

        private static async Task DeleteProductAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            //DeleteProductAsync
            Console.WriteLine("DeleteProductAsync started..");
            var deleteProductResponse = await client.DeleteProductAsync(
                    new DeleteProductRequest
                    {
                        ProductId=3
                    }
                );
            Console.WriteLine("DeleteProductAsync Response: " + deleteProductResponse.Success.ToString());
            Thread.Sleep(1000);
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
            try
            {
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
            catch (Exception ex)
            {

                throw ex;
            }
            
            
        }

    }
}
