using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Models.Dtos
{
    public class ProductDto
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public float productPrice { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }
}
