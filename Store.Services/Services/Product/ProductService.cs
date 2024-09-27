using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specifications;
using Store.Repository.Specifications.ProductSpecs;
using Store.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public ProductService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;
        }
        public async Task<IEnumerable<ProductDetailsDto>> GetAllProducts()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAll().Include(p => p.Category)
                                     .Include(p => p.Brand)
                                     .ToListAsync();

            // map products to ProductDetailsDto
            // concat baseUrl located in Appsetting.json using IConfiguring with image url
            var baseUrl = _configuration["BaseUrl"];
            var mappedProducts = products.Select(p => new ProductDetailsDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryName = p.Category.Name,
                PictureUrl = baseUrl + p.PictureUrl,
                BrandName = p.Brand.Name,
            });

            return mappedProducts;
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetAllProductsWithSpecs(ProductSpecifications specs)
        {
            var ProductSpec = new ProductWithSpecifications(specs);
            
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecifications(ProductSpec);
            
            var mappedProducts = products.Select(product => new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName = product.Category?.Name, // Handle nulls if needed
                PictureUrl = product.PictureUrl,
                BrandName = product.Brand?.Name // Handle nulls if needed
            }).ToList();

            return mappedProducts;
        }

        public async Task<ProductDetailsDto> GetProductById(int id)
        {
            var product = await _unitOfWork.Repository<Product, int>().GetAll().Include(p => p.Category)
                                     .Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id);

            // concat baseUrl located in Appsetting.json using IConfiguring with image url
            var baseUrl = _configuration["BaseUrl"];
            var mappedProduct = new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName = product.Category.Name,
                PictureUrl = baseUrl + product.PictureUrl,
                BrandName = product.Brand.Name,
                Category = product.Category,
                Brand = product.Brand
            };
            return mappedProduct;
        }

        public Task<IEnumerable<ProductDetailsDto>> GetProductsByCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDetailsDto>> GetProductsByName(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(int id)
        {
            var productEntity = _unitOfWork.Repository<Product, int>().GetAll().FirstOrDefault(p => p.Id == id);

            this._unitOfWork.Repository<Product, int>().DeleteAsync(productEntity);
        }
    }
}
