using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Dtos.Responses;
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
    public class ProjectionExampleRepository : GenericRepository<Product>, IProjectionExampleRepository
    {
        public ProjectionExampleRepository(ApplicationDbContext context) : base(context)
        {
        }

        /* Projection - Yansıtma veri tabanından çekilen verileri C# tarafında 3 farklı tipi yansıtabiliriz. Bunlar aşağıdaki gibidir
        
        1.) Entity
        2.) DTO/ View Model
        3.) Anonymous Types

         */

        //Projection - Entity örneği => Veri tabanından çekilen veririnin Entity ye yansıtılması.
        public async Task EntityProjection()
        {
            var products = await _context.Set<Product>().Include(x => x.Category).ToListAsync();
        }

        //Projection - AnonymousTypes => Veri tabanından çekilen veririnin AnonymousTypes ye yansıtılması.
        public async Task AnonymousTypesProjection1()
        {
            var products = await _context.Set<Product>().Include(x => x.Category).Include(x => x.ProductFeature).Select(x => new
            {
                //Burada AnonymousType şeklinde bir yapı kuruyoruz ve geriye dönen tipi bu şekilde biçimlendiriyoruz.
                CategoryName = x.Category.Name,
                ProductName = x.Name,
                ProductPrice = x.Price,
                width = (int?)x.ProductFeature.Width

            }).Where(x => x.width > 10).ToListAsync();



            var categories = await _context.Set<Category>().Include(x => x.Products).ThenInclude(x => x.ProductFeature).Select(x => new
            {
                // Burada AnonymousType şeklinde bir yapı kuruyoruz ve geriye dönen tipi bu şekilde biçimlendiriyoruz.
                CategoryName = x.Name,
                Products = String.Join(",", x.Products.Select(p => p.Name)), //Select içinde select kullanabiliriz.
                TotalPrice = x.Products.Sum(x => x.Price) //Listenin metotlarından sum dan yararlanarak product lar içerisinde price larıda toplayabiliriz.

            }).Where(y => y.TotalPrice > 100).OrderBy(y => y.TotalPrice).ToListAsync();
        }

        public async Task AnonymousTypesProjection2()
        {
            var products = await _context.Set<Product>().Select(x => new
            {
                //Burada AnonymousType şeklinde bir yapı kuruyoruz ve geriye dönen tipi bu şekilde biçimlendiriyoruz.
                CategoryName = x.Category.Name, // => Burada navigation property olarak kullandığımızda ekstra olarak yukarıdaki gibi Include metodunu kullanmamıza gerek kalmıyor EF Core bunu Select in içerisinde otomatik olarak algılıyor.
                ProductName = x.Name,
                ProductPrice = x.Price,
                width = (int?)x.ProductFeature.Width // => Burada navigation property olarak kullandığımızda ekstra olarak yukarıdaki gibi Include metodunu kullanmamıza gerek kalmıyor EF Core bunu Select in içerisinde otomatik olarak algılıyor.

            }).Where(x => x.width > 10).ToListAsync();


        }

        public async Task DTOProjection()
        {
            //Burada da gördüğünüz üzere Select içerisinde bir dto tanımlayarak sorgu sonucu dönen verileri dto içerisindeki propertylere eşledik.
            var products = await _context.Set<Product>().Select(x => new ProductReadResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Amount = x.Amount,
                Stock = x.Stock,


            }).Where(x => x.Price > 10).ToListAsync();


        }

        //AutoMapper kullanarak oluşturduğumuz bir DTO ya bu şekilde veri ekleyebiliriz.
        public async Task DTOAutoMapperProjection()
        {
            //var product = await _context.Set<Product>().ToListAsync();

            ////var productDto = ObjectMapper.Mapper.Map<List<ProductReadResponseDto>>(product);

        }

        //AutoMapper kullanarak oluşturduğumuz bir DTO ya bu şekilde veri ekleyebiliriz.
        public async Task DTOAutoMapperProjection2()
        {
            //ProjectTo metodu AutoMapper kütüphanesinden gelir ve direkt olarak veri tabanından gelen veriyi belirlediğimiz DTO ya mapler.

            //var product = await _context.Set<Product>().ProjectTo<ProductReadResponseDto>(ObjectMapper.Mapper.ConfigurationProvider).ToListAsync();


        }
    }
}