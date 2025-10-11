using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DTOS.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.MappingProfiles
{
    class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureURL))
            {
                return string.Empty;
            }
            else
            {
                var Url = $"{_configuration.GetSection("URLS")["BaseUrl"]}{source.Product.PictureURL}";
                return Url;
            }
        }
    }
}
