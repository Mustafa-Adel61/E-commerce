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
    public class MerchantRepository:BaseRepository<Merchant>,IMerchantRepository
    {
        private readonly ApplicationDbContext _context;

        public MerchantRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        //ماقبل ال AutoMapper
        public Merchant MerchantDtoToMerchant(MerchantDto dto)
        {
            DateTime time = DateTime.Parse(dto.CreatedAt);
            Merchant merchant = new Merchant()
            {
                CreatedAt = time,
                country_Code = dto.country_Code,
                Merchant_Name = dto.Merchant_Name,
            };
            return merchant;
        }

    }
}
