using Ecommerce.core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core
{
    public interface IUnitOfWork: IDisposable
    {
        // IBaseRepository<User> _User { get;}
         IBaseRepository<User> _User { get;}
         IBaseRepository<Cart> _Cart { get;}
         IProductRepository _Product { get;}
         IBaseRepository<OrderItem> _OrderItem { get;}
        // IBaseRepository<Order> _Order { get;}
         IOrderRepository _Order { get;}
        // IBaseRepository<Merchant> _Merchant { get;}
         IMerchantRepository _Merchant { get;}
         ICountrieRepository _Countrie { get;}
         void complete();
    }
}
