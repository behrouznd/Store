using Application.Services;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using Domain.Repositories;
using Domain.Repositories.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationTests.Services
{
    public class ProductTests
    {
        public int ProductId1 => 123;
        public int ProductId2 => 124;
        public int ProductId3 => 125;
        public string ProductName1 => "ProductX";
        public string ProductName2 => "ProductY";
        public string ProductName3 => "ProductZ";

        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IRepository<Category>> _mockCategoryRepository;
        private Mock<ProductService> _mockAppLogger;

        public ProductTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockCategoryRepository = new Mock<IRepository<Category>>();
            _mockAppLogger = new Mock<ProductService>();
        }

        [Fact]
        public async Task Get_Product_List()
        {
            var category = Category.Create(It.IsAny<int>(), It.IsAny<string>());
            var product1 = Product.Create(It.IsAny<int>(), category.Id, It.IsAny<string>());
            var product2 = Product.Create(It.IsAny<int>(), category.Id, It.IsAny<string>());

            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(category);
            _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product1);
            _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product2);

            var productService = new ProductService(_mockProductRepository.Object);
            var productList = await productService.GetProductList();

            _mockProductRepository.Verify(x => x.GetProductListAsync(), Times.Once);
        }

 


        [Fact]
        public async Task Create_New_Product_Validate_If_Exist()
        {
            var category = Category.Create(It.IsAny<int>(), It.IsAny<string>());
            var product = Product.Create(It.IsAny<int>(), category.Id, It.IsAny<string>());

            _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.AddAsync(product)).ReturnsAsync(product);

            var productService = new ProductService(_mockProductRepository.Object);

            await Assert.ThrowsAsync<ApplicationException>(async () =>
                await productService.Create(new Application.Models.ProductModel { Id = product.Id, CategoryId = product.CategoryId, ProductName = product.ProductName }));
        }

    }
}
