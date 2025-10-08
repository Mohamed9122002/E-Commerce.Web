using AutoMapper;
using DomainLayer.Models.ProductModul;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
             .ForMember(dest => dest.BarndName, options => options.MapFrom(src => src.ProductBrand.Name))
             .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.ProductType.Name));
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();

        }
    }
}
