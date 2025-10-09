using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParameters
    {
        //int? BrandId,int? TypeId, ProductSortingOptions sortingOptions
        public int? BrandId { get; set; } 
        public int? TypeId { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }

    }
}
