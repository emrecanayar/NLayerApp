using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities;
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
    public class LogConfiguration : BaseConfiguration<Log>
    {
        public override void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.Property(x => x.EventId).HasColumnName("EventId").HasMaxLength(36);
            builder.Property(x => x.LogDomain).HasColumnName("LogDomain").HasMaxLength(50);
            builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired(false);
            builder.Property(x => x.LogDate).HasColumnName("LogDate");
            builder.Property(x => x.Host).HasColumnName("Host").HasMaxLength(60);
            builder.Property(x => x.Path).HasColumnName("Path").HasMaxLength(60);
            builder.Property(x => x.Scheme).HasColumnName("Scheme").HasMaxLength(50);
            builder.Property(x => x.QueryString).HasColumnName("QueryString").HasMaxLength(255);
            builder.Property(x => x.RemoteIp).HasColumnName("RemoteIp").HasMaxLength(50);
            builder.Property(x => x.Headers).HasColumnName("Headers").HasColumnType("text");
            builder.Property(x => x.RequestBody).HasColumnName("RequestBody").HasColumnType("text");
            builder.Property(x => x.ResponseBody).HasColumnName("ResponseBody").HasColumnType("text");
            builder.Property(x => x.Exception).HasColumnName("Exception").HasColumnType("text");
            builder.Property(x => x.ExceptionMessage).HasColumnName("ExceptionMessage").HasColumnType("text");
            builder.Property(x => x.InnerException).HasColumnName("InnerException").HasColumnType("text");
            builder.Property(x => x.InnerExceptionMessage).HasColumnName("InnerExceptionMessage").HasColumnType("text");
            builder.Property(x => x.Status).HasColumnName("Status").HasConversion<int>();
            builder.Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsRequired(false).HasMaxLength(LengthContraints.CreatedByMaxLength);
            builder.Property(x => x.ModifiedBy).HasColumnName("ModifiedBy").IsRequired(false).HasMaxLength(LengthContraints.ModifiedByMaxLength);
            builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsRequired(true);
            builder.Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").IsRequired(true).HasDefaultValue(false);
            builder.HasQueryFilter(x => x.IsDeleted == false);
            base.Configure(builder);
            builder.ToTable(TableNameConstants.LOG);
        }
    }
}
