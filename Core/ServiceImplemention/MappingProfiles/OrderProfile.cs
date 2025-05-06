﻿using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObject.IdentityDTOS;
using Shared.OrderDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention.MappingProfiles
{
public    class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTo,OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDTo>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName));
            CreateMap<OrderItem, OrderItemDTo>()
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());

        }
    }
}
