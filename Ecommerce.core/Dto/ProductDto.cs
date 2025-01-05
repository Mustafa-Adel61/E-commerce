using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Dto
{
    public class ProductDto
    {
        public int merchantId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public IFormFile Image { get; set; }
        public string CreateAt { get; set; }
    }
}
