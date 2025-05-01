using AutoMapper;
using DomainLayer.Models.IdentityModule;
using Shared.DataTransferObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention.MappingProfiles
{
  public  class IdentityProfile :Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDTo>().ReverseMap();


        }
    }
}
