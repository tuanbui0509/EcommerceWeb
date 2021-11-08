using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;

namespace EcommerceSolution.Data.Configurations
{
    class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRoles");
            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x => x.CreatedBy).HasMaxLength(200);
            builder.Property(x => x.CreatedDate);
            builder.Property(x => x.IsDeleted);
            builder.Property(x => x.UpdatedBy).HasMaxLength(200);
            builder.Property(x => x.UpdatedDate);
            builder.Property(x => x.RowVersion)
                .HasColumnType("timestamp")
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
