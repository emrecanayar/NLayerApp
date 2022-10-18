using NLayerApp.Core.ComplexTypes;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.UnitOfWorks;
using NLayerApp.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Repositories
{
    public class TransactionExampleRepository : GenericRepository<Product>, ITransactionExampleRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionExampleRepository(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        //Eğer birbirleriyle ilişkili bir tablo varsa ve ekleme işlemi sırasında birine veri eklendikten sonra elde edilen Id ile diğerine bir veri eklemek gerkeiyorsa bu gibi durumlarda _context nesnesi üzerindeki BeginTransaction yapısı aşağıdaki gibi kullanılır. Burada yapılması gereken olay şudur. Ekleme işlemleri yapıldıktan sonra (SaveChangesAsync) bu işlemler bir transaction scope içerisinde olduug için direkt olarak veri tabanına yansımaz hafıza tutulur ve id verileri elde edilebilir. Bütün işlemlerin tamamen veri tabanına yansıması için ise gerekli olan komut ise transaction nesnesi üzerinden gelen commit metodudur.
        public async Task TransactionExample()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var category = new Category
                {
                    Name = "Test Transaction",
                    Status = RecordStatu.Active,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "",
                    ModifiedDate = DateTime.Now,
                };
                _context.Set<Category>().Add(category);
                await _context.SaveChangesAsync();

                var product = new Product
                {
                    Name = "Test Transactipn Product",
                    Price = 100,
                    Amount = 10,
                    Stock = 200,
                    CategoryId = category.Id,
                    Status = RecordStatu.Active,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "",
                    ModifiedDate = DateTime.Now
                };
                _context.Set<Product>().Add(product);
                await _context.SaveChangesAsync();

                throw new Exception("Exception test for Transaction");
                transaction.CommitAsync();
            }


            //Yukarıdaki işlem transaction kullanmadan da sadece navigation properylerden de faydalanarakta aşağıdaki gibi ekleme işlemi gerçekleştirelebilir. Bu sayede EF Core bizim için transaction işlemini gerçekleştiriyor.

            #region Transaction işlemine alternatif
            /*
               var category = new Category
                {
                    Name = "Test Transaction",
                    Status = RecordStatu.Active,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "",
                    ModifiedDate = DateTime.Now,
                };
                _context.Set<Category>().Add(category);
             

                var product = new Product
                {
                    Name = "Test Transactipn Product",
                    Price = 100,
                    Amount = 10,
                    Stock = 200,
                    Category = category,   //Navigation property olarak ekleyerek bu işlemi transaction olmadan halledebiliriz.
                    Status = RecordStatu.Active,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "",
                    ModifiedDate = DateTime.Now
                };

                _context.Set<Product>().Add(product);
                await _context.SaveChangesAsync();
             */

            #endregion
        }

        //Unit Of Work ile kapalı transaction
        public async Task UnitOfWorkTransactionExampleClosed()
        {
            var category = new Category
            {
                Name = "Test Transaction",
                Status = RecordStatu.Active,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "",
                ModifiedDate = DateTime.Now,
            };

            _context.Set<Category>().Add(category);

            var product = new Product
            {
                Name = "Test Transactipn Product",
                Price = 100,
                Amount = 10,
                Stock = 200,
                Category = category,   //Navigation property olarak ekleyerek bu işlemi transaction olmadan halledebiliriz.
                Status = RecordStatu.Active,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "",
                ModifiedDate = DateTime.Now
            };

            _context.Set<Product>().Add(product);

            throw new Exception("Exception test for Transaction");
            await _unitOfWork.SaveAsync();
        }

        //Unit Of Work ile açık transaction
        public async Task UnitOfWorkTransactionExampleOpened()
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            var category = new Category
            {
                Name = "Test Transaction",
                Status = RecordStatu.Active,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "",
                ModifiedDate = DateTime.Now,
            };

            _context.Set<Category>().Add(category);
            await _unitOfWork.SaveAsync();

            var product = new Product
            {
                Name = "Test Transaction Product",
                Price = 100,
                Amount = 10,
                Stock = 200,
                CategoryId = category.Id,   //Navigation property olarak ekleyerek bu işlemi transaction olmadan halledebiliriz.
                Status = RecordStatu.Active,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "",
                ModifiedDate = DateTime.Now
            };

            _context.Set<Product>().Add(product);
            await _unitOfWork.SaveAsync();

            throw new Exception("Exception test for Transaction");

            await transaction.CommitAsync();
        }
    }
}
