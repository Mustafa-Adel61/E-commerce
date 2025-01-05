using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Model
{
    public class Merchant
    {
      
        public int Id { get; set; }
        public string Merchant_Name { get; set; }
        public int country_Code { get; set; }
        public DateTime CreatedAt { get; set; }
      //  public List<Product> products { get; set; }
        public Countrie countrie { get; set; }


    }
}
