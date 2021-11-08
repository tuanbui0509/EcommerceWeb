using EcommerceSolution.Data.EntityBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceSolution.Data.Entities
{
    public class ProductImage : AuditEntity<Guid>
    {
        public Guid ProductId { get; set; }

        public string ImagePath { get; set; }

        public int SortOrder { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Product Product { get; set; }
    }
}