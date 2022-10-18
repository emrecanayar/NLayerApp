using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services.BusinessService;
using NLayerApp.Core.UnitOfWorks;
using NLayerApp.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Services.BusinessService
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDataProtector _dataProtector;

        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IDataProtectionProvider dataProtectionProvider) : base(repository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("CategoryService");
        }

        public async Task<CustomResponseDto<CategoryWithProductsResponseDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);
            var categoryDto = ObjectMapper.Mapper.Map<CategoryWithProductsResponseDto>(category);
            return CustomResponseDto<CategoryWithProductsResponseDto>.Success((int)HttpStatusCode.OK, data: categoryDto, isSuccess: true);

        }

        public async Task<CustomResponseDto<CategoryWithProductsResponseDto>> GetAllCategoryByIdWithProductsAsync(int categoryId)
        {
            var categories = await _categoryRepository.GetAllCategoryByIdWithProductsAsync(categoryId);
            var categoryDto = ObjectMapper.Mapper.Map<CategoryWithProductsResponseDto>(categories);
            return CustomResponseDto<CategoryWithProductsResponseDto>.Success((int)HttpStatusCode.OK, data: categoryDto, isSuccess: true);
        }

        public async Task<CustomResponseDto<CategoryWithProductsResponseDto>> GetCategoryByGrandTotalAsync(decimal grandTotal)
        {
            var category = await _categoryRepository.GetCategoryByGrandTotalAsync(grandTotal);
            var categoryDto = ObjectMapper.Mapper.Map<CategoryWithProductsResponseDto>(category);
            return CustomResponseDto<CategoryWithProductsResponseDto>.Success((int)HttpStatusCode.OK, data: categoryDto, isSuccess: true);
        }


        public async Task<CustomResponseDto<List<CategoryReadResponseDto>>> GetAllWithEncrypIdAsync()
        {
            var categories = await GetAllAsync();
            categories.ToList().ForEach(x =>
            {
                x.EncrypedId = _dataProtector.Protect(x.Id.ToString());
            });
            var categoryReadDto = ObjectMapper.Mapper.Map<List<CategoryReadResponseDto>>(categories);
            return CustomResponseDto<List<CategoryReadResponseDto>>.Success((int)HttpStatusCode.OK, data: categoryReadDto, isSuccess: true);
        }

        public async Task<CustomResponseDto<CategoryReadResponseDto>> GetCategoryByCategoryEncryptId(string categoryEncryptId)
        {
            int decryptedCategoryId = int.Parse(_dataProtector.Unprotect(categoryEncryptId));
            var category = await GetByIdAsync(decryptedCategoryId);
            var categoryDto = ObjectMapper.Mapper.Map<CategoryReadResponseDto>(category);
            return CustomResponseDto<CategoryReadResponseDto>.Success((int)HttpStatusCode.OK, data: categoryDto, isSuccess: true);
        }

    }
}
