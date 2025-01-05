using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Model
{
   public enum Gender {male,female }
    public class User
    {
        public int Id { get; set; }
        public string Full_Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public DateTime Date_of_birth { get; set; }
        public int CountrieCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Image { get; set; }
        public Countrie countrie { get; set; }
        public Cart cart { get; set; }

    }
}
