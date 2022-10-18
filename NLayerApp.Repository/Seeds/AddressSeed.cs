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
    public class AddressSeed : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(
                 new Address { Id = 1, CustomerId = 1, Country = "Germany", City = "Berlin", PostalCode = "12209", Phone = "030-0074321" },
                 new Address { Id = 2, CustomerId = 1, Country = "Mexico", City = "México D.F.", PostalCode = "05021", Phone = "030-0074321" },
                 new Address { Id = 3, CustomerId = 2, Country = "UK", City = "London", PostalCode = "WA1 1DP", Phone = "(171) 555-7788" },
                 new Address { Id = 4, CustomerId = 2, Country = "Sweden", City = "Luleå", PostalCode = "S-958 22", Phone = "0921-12 34 65" },
                 new Address { Id = 5, CustomerId = 3, Country = "France", City = "Strasbourg", PostalCode = "67000", Phone = "88.60.15.32" },
                 new Address { Id = 6, CustomerId = 4, Country = "Spain", City = "Madrid", PostalCode = "28023", Phone = "(91) 555 22 82" });

        }
    }
}