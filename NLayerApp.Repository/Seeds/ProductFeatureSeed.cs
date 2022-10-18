using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Seeds
{
    internal class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(
            new ProductFeature
            {
                Id = 1,
                ProductId = 1,
                Color = "White",
                Width = 100,
                Height = 200,
            },
            new ProductFeature
            {
                Id = 2,
                ProductId = 2,
                Color = "Red",
                Width = 120,
                Height = 130,
            });
        }
    }
}
