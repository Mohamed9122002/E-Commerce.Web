using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDTo> CreateORUpdateBasketAsync(BasketDTo basket)
        {
            var customerBasket = _mapper.Map<BasketDTo, CustomerBasket>(basket);
            var IsCreatedOrUpdatedBasket = await _basketRepository.CreateORUpdateBasketAsync(customerBasket);
            if (IsCreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can Not Update Or Create Basket Now ,Try Agin Later ");

        }

        public async Task<BasketDTo> GetBasketAsync(string key)
        {
            var Basket = await _basketRepository.GetBasketAsync(key);
            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDTo>(Basket);
            else
                throw new BasketNotFoundException(key);
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _basketRepository.DeleteBasketAsync(key);
        }
    }
}
