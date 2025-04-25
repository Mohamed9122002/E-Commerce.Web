using DomainLayer.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string key);
        Task<CustomerBasket> CreateORUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeLive = null);
        Task<bool> DeleteBasketAsync(string key);


    }
}
