﻿using Store.Repository.Specifications.ProductSpecs;

namespace Store.Services.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDetailsDto>> GetAllProducts();
        public Task<IEnumerable<ProductDetailsDto>> GetAllProductsWithSpecs(ProductSpecifications specs);
        public Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsWithPaging(ProductSpecifications specs);
        public Task<ProductDetailsDto> GetProductById(int id);
        public Task<ProductDetailsDto> GetProductByIdWithSpecs(int id);
        public Task<IEnumerable<ProductDetailsDto>> GetProductsByCategory(int id);
        public Task<IEnumerable<ProductDetailsDto>> GetProductsByName(string name);
        public void RemoveProduct(int id);
    }
}
