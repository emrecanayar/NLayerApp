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
    internal class OrderConfiguration : BaseConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.OrderDate).HasColumnName("OrderDate").IsRequired();
            base.Configure(builder);
            builder.ToTable(TableNameConstants.ORDER);
        }

    }
}
