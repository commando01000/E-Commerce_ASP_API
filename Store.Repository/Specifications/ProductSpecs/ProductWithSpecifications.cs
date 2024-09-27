using Store.Data.Entities;
using Store.Repository.Specifications.ProductSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public class ProductWithSpecifications : BaseSpecifications<Product>
    {
        public ProductWithSpecifications(ProductSpecifications specs) :
             base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
            (!specs.CategoryId.HasValue || product.CategoryId == specs.CategoryId.Value))
        {
            AddInclude(product => product.Brand);
            AddInclude(product => product.Category);
        }
    }
}
