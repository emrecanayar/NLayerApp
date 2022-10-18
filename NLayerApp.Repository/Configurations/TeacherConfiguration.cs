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
    internal class TeacherConfiguration : BaseConfiguration<Teacher>
    {
        public override void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(LengthContraints.NameMaxLength);
            builder.Property(x => x.LastName).HasMaxLength(LengthContraints.NameMaxLength);
            builder.ToTable(TableNameConstants.TEACHER);
            base.Configure(builder);
        }
    }
}
