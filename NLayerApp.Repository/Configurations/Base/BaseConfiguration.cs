using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Base;
using NLayerApp.Core.ComplexTypes;
using NLayerApp.Repository.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Configurations.Base
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").UseIdentityColumn();
            builder.Property(x => x.Status).HasColumnName("Status").IsRequired(true).HasConversion<int>().HasDefaultValue(RecordStatu.Active);
            builder.Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsRequired(true).HasMaxLength(LengthContraints.CreatedByMaxLength).ValueGeneratedOnAdd();
            builder.Property(x => x.ModifiedBy).HasColumnName("ModifiedBy").IsRequired(false).HasMaxLength(LengthContraints.CreatedByMaxLength);
            builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsRequired(true).ValueGeneratedOnAdd();
            builder.Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").IsRequired(true).HasDefaultValue(false);
            //Bunu bir queryFilter olarak ekledik sorgularımıza her seferinde bu alanı dahil etmek zorunda kalmıyoruz. Soft delete için sorgularımıza bunu Entity Framework kendi ekliyor.
            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
