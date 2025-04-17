﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public class ProductDtos
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public string BarndName { get; set; }
        public string TypeName { get; set; } 


    }
}
