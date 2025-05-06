using AutoMapper;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.OrderDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention.MappingProfiles
{
    public class OrderItemPictureUrlResolver (IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDTo, string>
    {
        private readonly IConfiguration _configuration = configuration;

        public string Resolve(OrderItem source, OrderItemDTo destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.ProductUrl))
                return string.Empty;
            else
            {
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.ProductUrl}";
                return Url;
            }
        }
    }
}
