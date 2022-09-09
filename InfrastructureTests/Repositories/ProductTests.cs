using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace InfrastructureTests.Repositories
{
    public class ProductTests
    {
        private readonly StoreContext _aspnetRunContext;
        private readonly ProductRepository _productRepository;
        private readonly ITestOutputHelper _output;
        private ProductBuilder ProductBuilder { get; } = new ProductBuilder();
        private CategoryBuilder CategoryBuilder { get; } = new CategoryBuilder();

        public ProductTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "Store")
                .Options;
            _aspnetRunContext = new StoreContext(dbOptions);
            _productRepository = new ProductRepository(_aspnetRunContext);
        }

        [Fact]
        public async Task Get_Existing_Product()
        {
            var existingProduct = ProductBuilder.WithDefaultValues();
            _aspnetRunContext.Products.Add(existingProduct);
            _aspnetRunContext.SaveChanges();

            var productId = existingProduct.Id;
            _output.WriteLine($"ProductId: {productId}");

            var productFromRepo = await _productRepository.GetByIdAsync(productId);
            Assert.Equal(ProductBuilder.TestProductId, productFromRepo.Id);
            Assert.Equal(ProductBuilder.TestCategoryId, productFromRepo.CategoryId);
        }
    }


}

