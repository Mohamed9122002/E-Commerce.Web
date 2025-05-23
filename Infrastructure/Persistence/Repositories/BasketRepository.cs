﻿using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        
        public async Task<CustomerBasket> CreateORUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeLive = null)
        {
          var JsonBasket = JsonSerializer.Serialize(basket);
           var IsCreatedOrUpdated =   await  _database.StringSetAsync(basket.Id, JsonBasket,TimeLive?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
                return await GetBasketAsync(basket.Id);
            else
                return null;
                  
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
          return  await  _database.KeyDeleteAsync(key);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var Basket = await _database.StringGetAsync(key);
            if (Basket.IsNullOrEmpty)
            {
                return null;
            }
            else
            {
                var customerBasket = JsonSerializer.Deserialize<CustomerBasket>(Basket!);
                return customerBasket;
            }
        }
    }
}
