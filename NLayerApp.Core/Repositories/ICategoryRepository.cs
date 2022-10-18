using NLayerApp.Core.Entities.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId);
        Task<List<Category>> GetAllCategoryByIdWithProductsAsync(int categoryId);
        Task<Category> GetCategoryByGrandTotalAsync(decimal grandTotal);
    }
}
