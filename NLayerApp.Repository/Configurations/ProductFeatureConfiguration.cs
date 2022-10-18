using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities.BusinessEntities;
using NLayerApp.Repository.Constants;

namespace NLayerApp.Repository.Configurations
{
    internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").UseIdentityColumn();
            builder.Property(x => x.Color).HasColumnName("Color").IsRequired(true).HasMaxLength(LengthContraints.NameMaxLength);
            builder.Property(x => x.Height).HasColumnName("Stock").IsRequired(true).HasConversion<int>();
            builder.Property(x => x.Width).HasColumnName("Amount").IsRequired(true).HasConversion<int>();
            builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature).HasForeignKey<ProductFeature>(x => x.ProductId);
            builder.ToTable(TableNameConstants.PRODUCT_FEATURE);
        }
    }
}
//Navigation Property Mapping Product(Master) and ProductFeature(Child) RelationShip
//builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature).HasForeignKey<ProductFeature>(x => x.ProductId);