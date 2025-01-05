using AutoMapper;
using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ecommerce.core.Helper
{

    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CartDto, Cart>().ReverseMap();
           CreateMap<CountrieDto, Countrie>().ReverseMap();
           CreateMap<UserDto, User>()
                .ForMember(c => c.Image, m => m.Ignore())
                .ForMember(c => c.CreatedAt, m => m.Ignore())
                .ForMember(c => c.Date_of_birth, m => m.Ignore())
                .ForMember(c => c.Gender, m => m.Ignore());
            CreateMap<ProductDto, Product>()
                .ForMember(c => c.Image, m => m.Ignore())
                .ForMember(c => c.CreateAt, m => m.Ignore());
            CreateMap<OrderDto, Order>()
                .ForMember(c =>c.CreatedAt, m => m.Ignore());
            CreateMap<MerchantDto, Merchant>()
                .ForMember(c=>c.CreatedAt,m => m.Ignore());
        }
    }
}
