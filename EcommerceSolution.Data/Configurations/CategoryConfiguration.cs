using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.Data.Enums;

namespace EcommerceSolution.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RowVersion)
                .HasColumnType("timestamp")
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}