using NLayerApp.Core.Entities.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IList<Product>> GetProductsWithCategoryAsync();
        Task<Product> GetProductByGrandTotal(decimal grandTotal);
    }

}
