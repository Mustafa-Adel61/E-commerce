using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Product ProductDtoToProduct(ProductDto dto);

    }
}
