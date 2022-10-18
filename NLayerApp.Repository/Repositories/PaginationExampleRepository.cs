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
    public class PaginationExampleRepository : GenericRepository<Product>, IPaginationExampleRepository
    {
        public PaginationExampleRepository(ApplicationDbContext context) : base(context)
        {
        }

        //Pagination => Sayfalama veri tabanından bütün verileri değilde belirtilen aralıklarda belirtilen sayıdaki datayı almak için kullanacağımız yapıdır. Bu sayede entitiy framework bütün datayı çekmek için bir emek hacamak yerine beliritlen aralıklardaki datayı çekmek için bir emek harcayacaktır. Bu da performansa olumlu yönde yansıyacaktır.

        //Pagination Example
        public async Task<List<Product>> GetProductsPagination(int page, int pageSize)
        {
            //Skip => Atla demek
            //Take => Atladıktan sonra kaç data istiyorsak onu al demek.

            //page = 1 pageSize = 3 => İlk 3 data => skip:0 take:3 => (page-1)*pageSize => (1-1)*3 :0
            //page = 2 pageSize = 3 => İlk 3 data => skip:3 take:3 => (page-1)*pageSize => (2-1)*3 :3
            //page = 3 pageSize = 3 => ilk 3 data => skip:6 take:3 => (page-1)*pageSize => (3-1)*3 :6
            var productsWithPage = await _context.Set<Product>().OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return productsWithPage;
        }
    }
}
