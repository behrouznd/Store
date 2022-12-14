using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomainTest.Entities
{
    public class ProductTests
    {
        private int _testProductId = 2;
        private int _testCategoryId = 3;
        private string _testProductName = "Reason";
        private decimal _testUnitPrice = 1.23m;
        private short _testQuantity = 2;

        [Fact]
        public void Create_Product()
        {
            var product = Product.Create(_testProductId, _testCategoryId, _testProductName, _testUnitPrice, _testQuantity, null, null, false);

            Assert.Equal(_testProductId, product.Id);
            Assert.Equal(_testCategoryId, product.CategoryId);
            Assert.Equal(_testProductName, product.ProductName);
            Assert.Equal(_testUnitPrice, product.UnitPrice);
            Assert.Equal(_testQuantity, product.UnitsInStock);
        }
    }
}
