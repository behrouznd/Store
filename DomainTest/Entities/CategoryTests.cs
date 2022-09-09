using Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomainTest.Entities
{
    public class CategoryTests
    {
       
        private int _testCategoryId = 5;
        
        [Fact]
        public void Adds_Product_Into_Category()
        {
            var category = Category.Create(_testCategoryId, "newCategory");
            Assert.Equal(_testCategoryId, category.Id);
        }

    }
}
