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
    public class RawExampleRepository : GenericRepository<Address>, IRawExampleRepository
    {
        public RawExampleRepository(ApplicationDbContext context) : base(context)
        {
        }

        //Entity Framework Raw ile bildiğimiz Sql Cümleciğini Entity Framework üzerinden çalıştırabiliriz.
        //1.Kullanım
        public async Task FirstUsed()
        {
            var products = await _context.Set<Product>().FromSqlRaw("Select * from Products").ToListAsync();

        }

        //Entity Framework Raw ile bildiğimiz Sql Cümleciğini Entity Framework üzerinden çalıştırabiliriz.
        //2.Kullanım Parametreli
        public async Task SecondUsed(int productId)
        {
            var products = await _context.Set<Product>().FromSqlRaw("Select * from Products Where Id = {0}", productId).ToListAsync();

        }

        //Entity Framework FromSqlInterpolated ile bildiğimiz Sql Cümleciğini Entity Framework üzerinden çalıştırabiliriz.
        //3.Kullanım Parametreli (FromSqlInterpolated) bu metot sayesinde sorgu içerisinde daha özgür paramatreler ve filtreler tanımlayabiliyoruz.
        public async Task ThirdUsed(decimal price)
        {
            var products = await _context.Set<Product>().FromSqlInterpolated($"Select * from Products Where Id >{price}").FirstAsync();

        }

        //Entity Framework FromSqlRaw ile bildiğimiz Sql Cümleciğini Entity Framework üzerinden çalıştırabiliriz.
        //4.Kullanım Inner Join Parametreli FromSqlRaw ile kullanıldığı gibi FromSqlInterpolated ile de kullanılabilir.
        public async Task FourthUsed()
        {
            var products = await _context.Set<ProductFeature>().FromSqlRaw("Select pf.Color,of.Height from Products p inner join ProductFeatures pd on p.Id = pf.Id").ToListAsync();

        }

    }
}