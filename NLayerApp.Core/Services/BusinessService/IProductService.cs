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
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryResponseDto>>> GetProductsWithCategory();

        Task<CustomResponseDto<ProductWithCategoryResponseDto>> GetProductByGrandTotal(decimal grandTotal);
    }
}
