﻿using Store.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDetailsDto>> GetAllProducts();
        public Task<ProductDetailsDto> GetProductById(int id);
        public Task<IEnumerable<ProductDetailsDto>> GetProductsByCategory(int id);
        public Task<IEnumerable<ProductDetailsDto>> GetProductsByName(string name);
    }
}