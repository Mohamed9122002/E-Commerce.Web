using AutoMapper;
using DomainLayer.Models.IdentityModule;
using Shared.DTOS.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.MappingProfiles
{
    public class IdentityProfile :Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap(); 
        }
    }
}
