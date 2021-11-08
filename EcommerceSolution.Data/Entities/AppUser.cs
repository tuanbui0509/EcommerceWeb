using EcommerceSolution.Data.EntityBase;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceSolution.Data.Entities
{
    public class AppUser : IdentityUser<Guid>, IAuditEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Avatar { get; set; }

        //Date of birth
        public DateTime Dob { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }
        public List<WishList> WishList { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}