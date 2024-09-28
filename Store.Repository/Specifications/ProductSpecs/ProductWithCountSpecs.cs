using Store.Data.Entities;
using Store.Repository.Specifications.ProductSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public class ProductWithCountSpecs : BaseSpecifications<Product>
    {
        public ProductWithCountSpecs(ProductSpecifications specs) : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
                (!specs.CategoryId.HasValue || product.CategoryId == specs.CategoryId.Value) && (specs.Search == null || product.Name.Contains(specs.Search))
             )
        {

        }
    }
}
