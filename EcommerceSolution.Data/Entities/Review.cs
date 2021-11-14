using EcommerceSolution.Data.EntityBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.Data.Entities
{
    public class Review : IAuditEntity
    {
        //public int Id { set; get; }
        public DateTime ReviewDate { set; get; }
        public Guid UserId { set; get; }
        public string Comment { set; get; }
        public int Rate { set; get; }
        public Guid ProductId { set; get; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Product Product { set; get; }
        public AppUser AppUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}