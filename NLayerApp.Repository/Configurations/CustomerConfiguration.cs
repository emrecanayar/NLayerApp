using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Repository.Configurations.Base;
using NLayerApp.Repository.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Configurations
{
    public class CustomerConfiguration : BaseConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(30);
            builder.Property(x => x.LastName).HasMaxLength(30);
            builder.Property(x => x.Title).HasMaxLength(LengthContraints.NameMaxLength);
            builder.Property(x => x.BirthDate);
            base.Configure(builder);
            builder.ToTable(TableNameConstants.CUSTOMER);
        }
    }
}
