using Domain.Entities.Categories;
using Domain.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Specifications
{
    public class CategoryWithProductsSpecification : BaseSpecification<Category>
    {
        public CategoryWithProductsSpecification(int categoryId)
            : base(b => b.Id == categoryId)
        {
            AddInclude(b => b.Products);
        }
    }
}
