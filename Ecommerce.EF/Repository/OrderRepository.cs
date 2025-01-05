using Ecommerce.core;
using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.EF.Repository
{
    public class OrderRepository:BaseRepository<Order>,IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        //ماقبل ال AutoMapper
        public Order OrderDtoToOrder(OrderDto dto)
        {
            Order order = new Order()
            {
                CreatedAt = DateTime.Parse(dto.CreatedAt),
                Status = dto.Status,
                UserId = dto.UserId,
            };
            return order;

        }
      

    }
}
