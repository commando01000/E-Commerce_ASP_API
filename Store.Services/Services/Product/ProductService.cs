using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductDetailsDto>> GetAllProducts()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAll().Include(p => p.Category)
                                     .Include(p => p.Brand)
                                     .ToListAsync();
            // map products to ProductDetailsDto

            var mappedProducts = products.Select(p => new ProductDetailsDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryName = p.Category.Name,
                PictureUrl = p.PictureUrl,
                BrandName = p.Brand.Name
            });

            return mappedProducts;
        }

        public async Task<ProductDetailsDto> GetProductById(int id)
        {
            var product = await _unitOfWork.Repository<Product, int>().GetAll().Include(p => p.Category)
                                     .Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id);

            var mappedProduct = new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName = product.Category.Name,
                PictureUrl = product.PictureUrl,
                BrandName = product.Brand.Name
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
    }
}
