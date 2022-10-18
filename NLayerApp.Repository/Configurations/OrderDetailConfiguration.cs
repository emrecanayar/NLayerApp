using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Repository.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Configurations
{
    internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").UseIdentityColumn();
            builder.Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired(true).HasConversion<decimal>(); ;
            builder.Property(x => x.Quantity).HasColumnName("Quantity").IsRequired(true).HasConversion<short>();
            builder.Property(x => x.Discount).HasColumnName("Discount").IsRequired(true).HasConversion<float>();
            builder.ToTable(TableNameConstants.ORDER_DETAIL);
        }
    }
}
