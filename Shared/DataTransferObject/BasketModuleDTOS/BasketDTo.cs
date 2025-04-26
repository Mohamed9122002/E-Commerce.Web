using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.BasketModuleDTOS
{
  public  class BasketDTo
    {
        public string Id { get; set; }
        public ICollection<BasketItemDTo> Items { get; set; } = [];
    }
}
