using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core
{
    public interface IUsersRepository:IBaseRepository<User>
    {
      //  User UserDto_To_User(UserDto dto);
    }
}
