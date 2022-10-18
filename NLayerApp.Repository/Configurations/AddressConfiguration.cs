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
    public class AddressConfiguration : BaseConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.CustomerId).HasColumnName("CustomerId").IsRequired(true);
            builder.Property(x => x.Country).HasColumnName("Country").IsRequired(true).HasMaxLength(LengthContraints.NameMaxLength);
            builder.Property(x => x.City).HasColumnName("City").IsRequired(true).HasMaxLength(LengthContraints.NameMaxLength);
            builder.Property(x => x.PostalCode).HasColumnName("PostalCode").IsRequired(true).HasMaxLength(10);
            builder.Property(x => x.Phone).HasColumnName("Phone").IsRequired(true).HasMaxLength(20);
            builder.ToTable(TableNameConstants.ADDRESS);
        }
    }
}
