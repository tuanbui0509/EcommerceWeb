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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.RowVersion)
                .HasColumnType("timestamp")
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();
            //builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.ReviewDate);

            builder.Property(x => x.Comment).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Rate);

            builder.HasOne(x => x.AppUser).WithMany(x => x.Reviews).HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Product).WithMany(x => x.Reviews).HasForeignKey(x => x.ProductId);
        }
    }
}