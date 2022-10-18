using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Repositories;
using NLayerApp.Repository.Contexts;

namespace NLayerApp.Repository.Repositories
{
    public class JoinExampleRepository : GenericRepository<Address>, IJoinExampleRepository
    {
        public JoinExampleRepository(ApplicationDbContext context) : base(context)
        {
        }

        //Inner Join Örneği - Eğer navigation Property olarak ayarlı bir yapı yoksa bu şekilde Inner Join işlemi gerçekleştirebiliriz.
        //Not (select new den sonra dilersek bir Dto yada farklı bir class tipinde de veri geriye dönebiliriz.)
        public async Task InnerJoinExample()
        {
            //İki tabloyu birbirine bağlamak için. (Linq To Entity)

            var result = await _context.Set<Address>().Join(_context.Set<Customer>(), x => x.CustomerId, y => y.Id, (a, c) => new
            {
                CustomerName = $"{c.FirstName} {c.LastName}",
                AddressName = $"{a.City} {a.Country}"

            }).ToListAsync();


            //Linq To Sql
            var result2 = await (from a in _context.Set<Address>()
                                 join c in _context.Set<Customer>() on a.CustomerId equals c.Id
                                 select new
                                 {
                                     CustomerName = $"{c.FirstName} {c.LastName}",
                                     AddressName = $"{a.City} {a.Country}"
                                 }).ToListAsync();



            //Üç tabloyu birbirine bağlamak için. 
            var resultVersion = await _context.Set<Category>()
                                .Join(_context.Set<Product>(), c => c.Id, p => p.CategoryId, (c, p) => new { c, p })
                                .Join(_context.Set<ProductFeature>(), x => x.p.Id, y => y.Id, (c, pf) => new
                                {
                                    CategoryName = c.c.Name,
                                    ProductName = c.p.Name,
                                    ProductFeature = pf.Color,

                                }).ToListAsync();

            //Linq To Sql 
            var resultVersion2 = await (from c in _context.Set<Category>()
                                        join p in _context.Set<Product>() on c.Id equals p.CategoryId
                                        join pf in _context.Set<ProductFeature>() on p.ProductFeature.Id equals pf.Id
                                        select new
                                        {
                                            CategoryName = c.Name,
                                            ProductName = p.Name,
                                            ProductFeature = pf.Color,
                                        }).ToListAsync();
        }

        //Left Join Örneği - Eğer navigation Property olarak ayarlı bir yapı yoksa bu şekilde Left Join işlemi gerçekleştirebiliriz.
        //Left Join Örnekleri sadece Linq To Sql ile yapılmaktadır.

        //Eğer right join yapmak istersek bu sefer Left Join yaptığımız tablolar için yer değiştirmemiz gerekmektedir.
        public async Task LeftJoinExample()
        {
            //Linq To Sql

            //Not: Left join olduğundan dolayı eğer ikinci tablo daki veriler boş gelirse bunun kontrolünü ternary ile de gerçekleştirebiliriz.
            var result = await (from p in _context.Set<Product>()
                                join pf in _context.Set<ProductFeature>() on p.Id equals pf.ProductId into pfList
                                from pf in pfList.DefaultIfEmpty()
                                select new
                                {
                                    ProductName = p.Name,
                                    ProductColor = pf.Color,
                                    ProductWith = (int?)pf.Width, //Left join olduğu için int alanını nullable olarak belirttik.
                                    ProductHeight = pf != null ? pf.Height : default,

                                }).ToListAsync();

        }

        //Full Outer Join Örneği - Eğer navigation Property olarak ayarlı bir yapı yoksa bu şekilde Left Join işlemi gerçekleştirebiliriz.
        //Bir Left Join ve Bir de Right Join yapalım ve bunları bir metotta toplarsak full outer join yapmıs olacağız.
        public async Task FullOuterJoinExample()
        {
            var left = await (from p in _context.Set<Product>()
                              join pf in _context.Set<ProductFeature>() on p.Id equals pf.Id into pfList
                              from pf in pfList.DefaultIfEmpty()
                              select new
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Color = pf.Color
                              }
                              ).ToListAsync();

            var right = await (from pf in _context.Set<ProductFeature>()
                               join p in _context.Set<Product>() on pf.Id equals p.Id into pList
                               from p in pList.DefaultIfEmpty()
                               select new
                               {
                                   Id = p.Id,
                                   Name = p.Name,
                                   Color = pf.Color
                               }
                           ).ToListAsync();


            var outerJoin = left.Union(right);
        }
    }
}
