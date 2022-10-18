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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            //EF Eager Loading (use Include)
            //TagWith ile beraber loglanan SQL sorgularının başına bu sorgunun ne yaptığını belirtebiliriz.
            return await _context.Set<Category>().TagWith($"This query returns a product by category id. {DateTime.Now}").Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();
        }

        public async Task<List<Category>> GetAllCategoryByIdWithProductsAsync(int categoryId)
        {
            //EF Eager Loading (use Include and ThenInclude)
            return await _context.Set<Category>().Include(x => x.Products).ThenInclude(x => x.ProductFeature).ToListAsync();
        }

        public async Task<Category> GetCategoryByGrandTotalAsync(decimal grandTotal)
        {
            var category = await _context.Set<Category>().AsNoTracking().FirstAsync();
            if (grandTotal > 100)
            {
                //EF Explicit Loading  One to Many RelationShip
                await _context.Entry(category).Collection(x => x.Products).LoadAsync();
                return category;
            }

            return category;
        }
    }
}
