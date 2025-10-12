using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DTOS.AuthDTOs;
using Shared.DTOS.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDTo>()
                .ForMember(dest => dest.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureURL, O => O.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<DeliveryMethod, DeliveryMethodDTO>();
        }
    }
}
