using EcommerceSolution.Data.EntityBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceSolution.Data.Entities
{
    public class OrderDetail : IAuditEntity
    {
        public Guid OrderId { set; get; }
        public Guid ProductId { set; get; }
        public int Quantity { set; get; }
        public float Price { set; get; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Order Order { get; set; }

        public Product Product { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
