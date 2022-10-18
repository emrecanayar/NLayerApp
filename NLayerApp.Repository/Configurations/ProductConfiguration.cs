using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.ComplexTypes;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Repository.Configurations.Base;
using NLayerApp.Repository.Constants;

namespace NLayerApp.Repository.Configurations
{
    internal class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired(true).HasMaxLength(LengthContraints.NameMaxLength);
            //builder.HasIndex(x => x.Name); //=> Create Index Sample
            //builder.HasIndex(x => x.Name).IncludeProperties(x => new { x.Price, x.Stock }); //=> Create Index Include Properties
            builder.Property(x => x.Stock).HasColumnName("Stock").IsRequired(true).HasConversion<int>();
            builder.Property(x => x.Amount).HasColumnName("Amount").IsRequired(true).HasConversion<int>();
            builder.Property(x => x.Price).HasColumnName("Price").IsRequired(true).HasColumnType("decimal(18,2)");
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            //builder.HasCheckConstraint("PriceDiscountCheck", "[Price]>[DiscountPrice]"); //Create CheckConstraint (Kısıtlama)
            base.Configure(builder);
            builder.ToTable(TableNameConstants.PRODUCT);
        }
    }
}

//Navigation Property Mapping Product(Child) and Category(Master) RelationShip
//builder.HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId);