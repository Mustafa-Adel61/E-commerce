using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core
{
    public interface IMerchantRepository:IBaseRepository<Merchant>
    {
        //Merchant MerchantDtoToMerchant(int id, MerchantOptionDto dto);
        Merchant MerchantDtoToMerchant(MerchantDto dto);

    }
}
