using Ecommerce.core;
using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.EF.Repository
{
    public class UsersRepository:BaseRepository<User>,IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context):base(context)
        {  
            _context = context;
        }
      

    }
}
