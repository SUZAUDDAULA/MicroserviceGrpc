using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductGrpc.Data.Entities.Product
{
    public class Product:Base
    {
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float price { get; set; }
        public ProductStatus Status { get; set; }
    }

    public enum ProductStatus
    {
        INSTOCK=0,
        LOW=1,
        NONE=2
    }
}
