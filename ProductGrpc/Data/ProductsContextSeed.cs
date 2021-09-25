using ProductGrpc.Data.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductGrpc.Data
{
    public class ProductsContextSeed
    {
        public static void SeedAsync(ProductsContext productsContext)
        {
            if (!productsContext.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        ProductCode="P-1001",
                        Name="Mi10T",
                        Description="New Xiaomi Phone Mi10T",
                        price=699,
                        Status=ProductStatus.INSTOCK,
                        createdAt=DateTime.UtcNow
                    },
                    new Product
                    {
                        ProductCode="P-1002",
                        Name="P40",
                        Description="New Huawei Phone P40",
                        price=899,
                        Status=ProductStatus.INSTOCK,
                        createdAt=DateTime.UtcNow
                    },
                    new Product
                    {
                        ProductCode="P-1003",
                        Name="A50",
                        Description="New Samsung Phone A50",
                        price=899,
                        Status=ProductStatus.INSTOCK,
                        createdAt=DateTime.UtcNow
                    }
                };
                productsContext.Products.AddRange(products);
                productsContext.SaveChanges();
            }
        }
    }
}
