using AutoMapper;
using DomainLayer.Models.BasketModule;
using Shared.DataTransferObject.BasketModuleDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention.MappingProfiles
{
   public class BaketProfile:Profile
    {
        public BaketProfile()
        {
            CreateMap<CustomerBasket,BasketDTo>().ReverseMap();
            CreateMap<BasketItem,BasketItemDTo>().ReverseMap();

        }
    }
}
