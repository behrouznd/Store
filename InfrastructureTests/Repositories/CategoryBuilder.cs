using Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureTests.Repositories
{
    public class CategoryBuilder
    {
        private Category _category;
        public int TestCategoryId => 123;
        public string TestCategoryName => "CategoryX";

        public CategoryBuilder()
        {
            _category = WithDefaultValues();
        }

        public Category WithDefaultValues()
        {
            return Category.Create(TestCategoryId, TestCategoryName);
        }
    }
}
