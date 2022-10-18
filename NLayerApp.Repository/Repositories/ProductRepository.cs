using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Repositories;
using NLayerApp.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<Product>> GetProductsWithCategoryAsync()
        {
            return await _context.Set<Product>().Include(x => x.Category).ToListAsync();
        }


        public async Task<Product> GetProductByGrandTotal(decimal grandTotal)
        {
            var product = await _context.Set<Product>().FirstAsync();
            if (grandTotal > 100)
            {
                //EF Explicit Loading  One to One RelationShip
                await _context.Entry(product).Reference(x => x.ProductFeature).LoadAsync();
                return product;
            }
            return product;
        }


    }
}
