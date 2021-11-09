using EcommerceSolution.Data.EntityBase;
using EcommerceSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceSolution.Data.Entities
{
    public class Category : AuditEntity<Guid>
    {
        public string Name { set; get; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public List<Product> Products { get; set; }
    }
}