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
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "Pencil 1",
                Price = 100,
                Stock = 20,
                Amount = 30,
            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "Pencil 2",
                Price = 200,
                Stock = 30,
                Amount = 40,
            },
            new Product
            {
                Id = 3,
                CategoryId = 1,
                Name = "Pencil 3",
                Price = 400,
                Stock = 40,
                Amount = 60,
            },
            new Product
            {
                Id = 4,
                CategoryId = 2,
                Name = "Notebook 1",
                Price = 100,
                Stock = 40,
                Amount = 60,
            },
            new Product
            {
                Id = 5,
                CategoryId = 3,
                Name = "Book 1",
                Price = 300,
                Stock = 50,
                Amount = 60,
            }
            );
        }
    }
}
