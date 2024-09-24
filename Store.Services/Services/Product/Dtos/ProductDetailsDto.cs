﻿using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string PictureUrl { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public ProductCategory Category { get; set; }
        public ProductBrand Brand { get; set; }

    }
}
