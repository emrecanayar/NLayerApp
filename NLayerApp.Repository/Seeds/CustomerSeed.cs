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
    public class CustomerSeed : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Customer { Id = 1, FirstName = "Nancy", LastName = "Davolio", Title = "Sales Representative" },
                new Customer { Id = 2, FirstName = "Andrew", LastName = "Fuller", Title = "Vice President, Sales" },
                new Customer { Id = 3, FirstName = "Janet", LastName = "Leverling", Title = "Sales Representative" },
                new Customer { Id = 4, FirstName = "Margaret", LastName = "Peacock", Title = "Sales Representative" },
                new Customer { Id = 5, FirstName = "Steven", LastName = "Buchanan", Title = "Sales Manager" });
        }
    }
}
