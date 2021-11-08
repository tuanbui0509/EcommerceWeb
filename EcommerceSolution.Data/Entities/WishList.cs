using EcommerceSolution.Data.EntityBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.Data.Entities
{
    public class WishList : AuditEntity<Guid>
    {
        //public int Id { set; get; }

        public Guid ProductId { set; get; }
        public Product Product { set; get; }
        public Guid UserId { set; get; }
        public AppUser AppUser { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}