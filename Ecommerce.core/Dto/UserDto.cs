using Ecommerce.core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Dto
{
    public class UserDto
    {
  

        public string Full_Name { get; set; }
        public string Email { get; set; }
        // this is enum 
        public string Gender { get; set; }
        // this is DateTime
        public string Date_of_birth { get; set; }
        public int CountrieCode { get; set; }
        public IFormFile Image { get; set; }
        public string CreatedAt { get; set; }
    }
}
