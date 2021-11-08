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
        //public int Id { set; get; }
        public Status Status { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Thumb { set; get; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public List<Product> Products { get; set; }
    }
}