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
    public class StoredProceduredExampleRepository : GenericRepository<Product>, IStoredProceduredExampleRepository
    {
        public StoredProceduredExampleRepository(ApplicationDbContext context) : base(context)
        {
        }


    }
}
