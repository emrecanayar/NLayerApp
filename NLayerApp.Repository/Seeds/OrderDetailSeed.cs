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
    internal class OrderDetailSeed : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasData(
                new OrderDetail
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1,
                    UnitPrice = 12.30M,
                    Quantity = 2,
                    Discount = 1.5F
                },
                new OrderDetail
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 2,
                    UnitPrice = 11.30M,
                    Quantity = 3,
                    Discount = 1.4F
                }
                );
        }
    }
}
