using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Entities.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services.BusinessService
{
    public interface ICategoryService : IService<Category>
    {
        Task<CustomResponseDto<CategoryWithProductsResponseDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId);
        Task<CustomResponseDto<CategoryWithProductsResponseDto>> GetAllCategoryByIdWithProductsAsync(int categoryId);
        Task<CustomResponseDto<CategoryWithProductsResponseDto>> GetCategoryByGrandTotalAsync(decimal grandTotal);
        Task<CustomResponseDto<List<CategoryReadResponseDto>>> GetAllWithEncrypIdAsync();
        Task<CustomResponseDto<CategoryReadResponseDto>> GetCategoryByCategoryEncryptId(string categoryEncryptId);



    }
}
