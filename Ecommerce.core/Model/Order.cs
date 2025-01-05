using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set;}
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public List<OrderItem> orderItem { get; set;}

    }
}
