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
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.ToTable("WishList");
            builder.Property(x => x.RowVersion)
                .HasColumnType("timestamp")
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.AppUser).WithMany(x => x.WishList).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Product).WithMany(x => x.WishList).HasForeignKey(x => x.ProductId);
        }
    }
}