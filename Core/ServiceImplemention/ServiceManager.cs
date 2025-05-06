using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository, UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : IServiceManager
    {
        //  Lazy Impementation of the ProductServices
        private readonly Lazy<IProductServices> _lazyProductServices = new Lazy<IProductServices>(() => new ProductServices(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _lazybasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        private readonly Lazy<IAuthenticationcService> _lazyAuthenticationcService = new Lazy<IAuthenticationcService>(() => new AuthenticationcService(_userManager, _mapper, _configuration));
        private readonly Lazy<IOrderServices> _lazyOrderServices = new Lazy<IOrderServices>(() => new OrderService(_mapper, _basketRepository, _unitOfWork));

        public IProductServices ProductServices => _lazyProductServices.Value;
        public IBasketService BasketServices => _lazybasketService.Value;

        public IAuthenticationcService AuthenticationcService => _lazyAuthenticationcService.Value;

        public IOrderServices OrderService => _lazyOrderServices.Value;

    }
}
