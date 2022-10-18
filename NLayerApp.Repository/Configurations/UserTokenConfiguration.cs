using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities;
using NLayerApp.Repository.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            builder.Property(t => t.LoginProvider).HasMaxLength(LengthContraints.TokenMaxLength);
            builder.Property(t => t.Name).HasMaxLength(LengthContraints.TokenMaxLength);
            builder.ToTable(TableNameConstants.USER_TOKEN);
        }
    }
}
