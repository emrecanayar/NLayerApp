using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities.BusinessEntities;

namespace NLayerApp.Repository.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Pencils" },
                new Category { Id = 2, Name = "Notebooks" },
                new Category { Id = 3, Name = "Books" });
        }
    }
}
