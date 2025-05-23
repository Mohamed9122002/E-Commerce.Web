﻿using AutoMapper;
using DomainLayer.Models.ProductModel;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention.MappingProfiles
{
  public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDtos>()
                .ForMember(dest => dest.BarndName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<PictureUrResolver>());
            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
        }
    }
}
