using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Controllers.Base;
using NLayerApp.API.Filters;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Services.BusinessService;
using NLayerApp.Repository.Logs;
using NLayerApp.Service.Mapping;
using System.Net;

namespace NLayerApp.API.Controllers
{

    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;


        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }

        [Authorize]
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryId));
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetAllCategoryByIdWithProducts(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetAllCategoryByIdWithProductsAsync(categoryId));
        }

        [HttpGet("[action]/{grandTotal}")]
        public async Task<IActionResult> GetCategoryByGrandTotalAsync(decimal grandTotal)
        {
            return CreateActionResult(await _categoryService.GetCategoryByGrandTotalAsync(grandTotal));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryCreateRequestDto categoryCreateRequestDto)
        {
            var category = await _categoryService.AddAsync(ObjectMapper.Mapper.Map<Category>(categoryCreateRequestDto));
            var result = ObjectMapper.Mapper.Map<CategoryCreateResponseDto>(category);
            return CreateActionResult(CustomResponseDto<CategoryCreateResponseDto>.Success((int)HttpStatusCode.Created, data: result, isSuccess: true));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithEncrypIdAsync()
        {
            return CreateActionResult(await _categoryService.GetAllWithEncrypIdAsync());
        }

        [HttpGet("GetAllWithEncrypIdAsync")]
        public async Task<IActionResult> GetAllWithEncrypIdAsync(string categoryEncryptId)
        {
            return CreateActionResult(await _categoryService.GetCategoryByCategoryEncryptId(categoryEncryptId));
        }


    }
}
