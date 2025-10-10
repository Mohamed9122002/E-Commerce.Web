using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BaskedModule;
using ServiceAbstraction;
using Shared.DTOS.BasketDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ServiceImplementation
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {

        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var IsCreatedOrUpdated = _basketRepository.CreateOrUpdatedBasketAsync(CustomerBasket);
            if (IsCreatedOrUpdated is not null)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Can't Updated Or Create Basket Now Try Again Later");

            }
        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket = await _basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            }
            else
            {
                throw new BasketNotFoundException(Key);
            }
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _basketRepository.DeleteBasketAsync(Key);
        }

    }
}
