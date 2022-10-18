using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Controllers.Base;
using NLayerApp.API.Filters;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Services.BusinessService;
using NLayerApp.Service.Mapping;

namespace NLayerApp.API.Controllers
{

    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productService.GetProductsWithCategory());
        }

        [HttpGet("[action]/{grandTotal}")]
        public async Task<IActionResult> GetProductByGrandTotal(decimal grandTotal)
        {
            return CreateActionResult(await _productService.GetProductByGrandTotal(grandTotal));
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productService.GetAllAsync();
            var result = ObjectMapper.Mapper.Map<List<ProductReadResponseDto>>(products.ToList());
            return CreateActionResult(CustomResponseDto<List<ProductReadResponseDto>>.Success(200, data: result, isSuccess: true));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var result = ObjectMapper.Mapper.Map<ProductReadResponseDto>(product);
            return CreateActionResult(CustomResponseDto<ProductReadResponseDto>.Success(200, data: result, isSuccess: true));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateRequestDto productCreateRequestDto)
        {
            var product = await _productService.AddAsync(ObjectMapper.Mapper.Map<Product>(productCreateRequestDto));
            var result = ObjectMapper.Mapper.Map<ProductCreateResponseDto>(product);
            return CreateActionResult(CustomResponseDto<ProductCreateResponseDto>.Success(201, data: result, isSuccess: true));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateRequestDto productUpdateRequestDto)
        {
            await _productService.UpdateAsync(ObjectMapper.Mapper.Map<Product>(productUpdateRequestDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, isSuccess: true));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, isSuccess: true));
        }
    }
}
