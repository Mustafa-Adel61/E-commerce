using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int merchantId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreateAt { get; set; }
        public List<OrderItem> orderItem { get; set; }
        public Merchant merchant { get; set; }
        public List<Cart> Carts { get; set; }


    }
}
