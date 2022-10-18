using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Base;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Core.Interfaces;
using NLayerApp.Repository.Extensions;
using System.Reflection;

namespace NLayerApp.Repository.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RegisterAllEntities<IEntity>(Assembly.GetExecutingAssembly());
            modelBuilder.RegisterAllConfigurations(Assembly.GetExecutingAssembly());
            modelBuilder.RegisterAllManyToManyRelationShips(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            TrackAndApplyChanges();
            return base.SaveChanges();
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackAndApplyChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void TrackAndApplyChanges()
        {
            ChangeTracker.Entries().ToList().ForEach(e =>
            {
                if (e.Entity is BaseEntity entity)
                {
                    if (e.State == EntityState.Added) entity.CreatedDate = DateTime.Now;
                    if (e.State == EntityState.Modified) entity.ModifiedDate = DateTime.Now;
                }
            });
        }

    }
}
