using Microsoft.EntityFrameworkCore.Storage;
using NLayerApp.Core.Interfaces;
using NLayerApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveAsync();
        void Save();
    }
}
