using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.Data.Enums;

namespace EcommerceSolution.Data.Extensions
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tuanbui0509@gmail.com",
                NormalizedEmail = "tuanbui0509@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "admin"),
                SecurityStamp = string.Empty,
                FirstName = "Tuan",
                LastName = "Ngoc",
                Dob = new DateTime(2021, 07, 11)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            // any guid
            var roleIdUser = new Guid("CE7E4C06-84B0-4114-912F-7E27F245DC47");
            var userId = new Guid("AA16F18B-95B9-4E6A-837E-EFB5B8E63E84");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleIdUser,
                Name = "User",
                NormalizedName = "user",
                Description = "User role"
            });

            var hasherUser = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = userId,
                UserName = "user",
                NormalizedUserName = "user",
                Email = "user@gmail.com",
                NormalizedEmail = "user@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasherUser.HashPassword(null, "User@123"),
                SecurityStamp = string.Empty,
                FirstName = "User",
                LastName = "My",
                Dob = new DateTime(2021, 07, 15)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleIdUser,
                UserId = userId
            });
        }
    }
}