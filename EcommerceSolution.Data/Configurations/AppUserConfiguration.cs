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
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Avatar);
            builder.Property(x => x.Dob).IsRequired();
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