using Ecommerce.core;
using Ecommerce.core.Model;
using Ecommerce.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<User> _User { get;private set; }
        public IBaseRepository<Cart> _Cart { get;private set; }
        public ICountrieRepository _Countrie { get;private set; }
       // public IBaseRepository<Merchant> _Merchant { get; private set; }
        public IMerchantRepository _Merchant { get; private set; }
      //  public IBaseRepository<Order> _Order { get; private set; }
        public IOrderRepository _Order { get; private set; }
        public IBaseRepository<OrderItem> _OrderItem { get; private set; }
        public IProductRepository _Product { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _User = new BaseRepository<User>(_context);
            _Cart = new BaseRepository<Cart>(_context);
            _Countrie= new CountrieRepository(_context);
            _Merchant= new MerchantRepository(_context);
            _Order= new OrderRepository(_context);
            _Product= new ProductRepository(_context);
            _OrderItem= new BaseRepository<OrderItem>(_context);

        }
        public void complete() {
            _context.SaveChanges();    
        }
        public void Dispose()
        {
            _context.Dispose();             
        }
    }
}