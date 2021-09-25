using Microsoft.EntityFrameworkCore;
using ProductGrpc.Data.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductGrpc.Data
{
    public class ProductsContext:DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options)
            :base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
