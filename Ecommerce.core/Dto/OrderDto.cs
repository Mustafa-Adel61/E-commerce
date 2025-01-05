using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Dto
{
    public class OrderDto
    {
        public int UserId { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
    }
}
