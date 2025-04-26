﻿using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository) : IServiceManager
    {
        //  Lazy Impementation of the ProductServices
        private readonly Lazy<IProductServices> _lazyProductServices = new Lazy<IProductServices>(() => new ProductServices(_unitOfWork, _mapper));
        public IProductServices ProductServices => _lazyProductServices.Value;
        private readonly Lazy<IBasketService> _lazybasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IBasketService BasketServices => _lazybasketService.Value;
    }
}
