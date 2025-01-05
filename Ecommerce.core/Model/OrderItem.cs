using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Model
{
    public class OrderItem
    {
        public int orderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public Order order { get; set; }
        public Product product { get; set; }
       
    }
}
