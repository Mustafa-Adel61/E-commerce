using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Model
{
    public class Countrie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Continent_Name { get; set; }
        public ICollection<User> Users { get; set; }
        public List<Merchant> merchants { get; set; }
    }
}
