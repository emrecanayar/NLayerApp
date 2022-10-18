using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities;
using NLayerApp.Repository.Configurations.Base;
using NLayerApp.Repository.Constants;

namespace NLayerApp.Repository.Configurations
{
    public class UserRefreshTokenConfiguration : BaseConfiguration<UserRefreshToken>
    {
        public override void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {

            builder.Property(u => u.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(u => u.Token).HasColumnName("Token").IsRequired();
            builder.Property(u => u.ReplacedByToken).HasColumnName("ReplacedByToken");
            builder.Property(u => u.TokenExpireDate).HasColumnName("TokenExpireDate").IsRequired();
            builder.Property(u => u.CreatorIp).HasColumnName("CreatorIp");
            builder.Property(u => u.RevokerIp).HasColumnName("RevokerIp");
            builder.Property(u => u.RevokeDate).HasColumnName("RevokeDate");
            base.Configure(builder);
            builder.ToTable(TableNameConstants.USER_REFRESH_TOKEN);
        }
    }
}
