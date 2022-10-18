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
    public class StudentConfiguration : BaseConfiguration<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(LengthContraints.NameMaxLength);
            builder.Property(x => x.LastName).HasMaxLength(LengthContraints.NameMaxLength);
            builder.Property(x => x.StudentNo);
            builder.ToTable(TableNameConstants.STUDENT);
            base.Configure(builder);
        }
    }
}
