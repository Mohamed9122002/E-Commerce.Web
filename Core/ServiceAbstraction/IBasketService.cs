using Shared.DataTransferObject.BasketModuleDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDTo> GetBasketAsync(string key);
        Task<BasketDTo> CreateORUpdateBasketAsync(BasketDTo basket);
        Task<bool> DeleteBasketAsync(string key);
    }
}
