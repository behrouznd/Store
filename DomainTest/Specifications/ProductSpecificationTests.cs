using Domain.Entities.Products;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomainTest.Specifications
{
    public class ProductSpecificationTests
    {
        public int ProductId1 => 123;
        public int ProductId2 => 124;
        public int ProductId3 => 125;
        public string ProductName1 => "ProductX";
        public string ProductName2 => "ProductY";
        public string ProductName3 => "ProductZ";

        public List<Product> GetProductCollection()
        {
            return new List<Product>()
            {
                Product.Create(ProductId1, 1, ProductName1),
                Product.Create(ProductId2, 1, ProductName2),
                Product.Create(ProductId3, 1, ProductName3)
            };
        }

      
        [Fact]
        public void Matches_Product_With_Category_Spec()
        {
            var spec = new ProductWithCategorySpecification(ProductName1);

            var result = GetProductCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal(ProductId1, result.Id);
        }

    }
}
