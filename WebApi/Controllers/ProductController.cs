using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productAppService;
        private readonly ICategoryService _categoryAppService;
        private readonly IMapper _mapper;



        public ProductController(IProductService productAppService, ICategoryService categoryAppService, IMapper mapper )
        {
            _productAppService = productAppService ?? throw new ArgumentNullException(nameof(productAppService));
            _categoryAppService = categoryAppService ?? throw new ArgumentNullException(nameof(categoryAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
          
        }

        [HttpGet("{productId}")]
        public async Task<ProductViewModel> GetProductById(int productId)
        {
            var product = await _productAppService.GetProductById(productId);
            var mapped = _mapper.Map<ProductViewModel>(product);
            return mapped;
        }


        [HttpPost]
        public async Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel)
        {
            var mapped = _mapper.Map<ProductModel>(productViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await _productAppService.Create(mapped);
            
            var mappedViewModel = _mapper.Map<ProductViewModel>(entityDto);
            return mappedViewModel;
        }

        [HttpPut()]
        public async Task UpdateProduct(ProductViewModel productViewModel)
        {
            var mapped = _mapper.Map<ProductModel>(productViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await _productAppService.Update(mapped);
             
        }

        [HttpDelete()]
        public async Task DeleteProduct(ProductViewModel productViewModel)
        {
            var mapped = _mapper.Map<ProductModel>(productViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await _productAppService.Delete(mapped);
           
        }
    }
}
