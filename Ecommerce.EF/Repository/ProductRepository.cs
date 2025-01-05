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
    public class ProductRepository:BaseRepository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
             MemoryStream memoryStream=new MemoryStream();
        //ماقبل ال AutoMapper

        public Product ProductDtoToProduct(ProductDto dto)
        {
             dto.Image.CopyTo(memoryStream); 
            Product product = new Product
            {
                CreateAt = DateTime.Parse(dto.CreateAt),
                merchantId=dto.merchantId,
                Name= dto.Name,
                Price= dto.Price,
                Status=dto.Status,
                Image=memoryStream.ToArray()
            };
            return product;
        }
        


    }
}
