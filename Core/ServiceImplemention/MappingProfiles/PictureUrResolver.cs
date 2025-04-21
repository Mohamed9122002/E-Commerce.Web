using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention.MappingProfiles
{
    public class PictureUrResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDtos, string>
    {

        public string Resolve(Product source, ProductDtos destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            else
            {
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return Url;
            }

        }
    }
}
