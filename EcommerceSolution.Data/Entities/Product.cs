using EcommerceSolution.Data.EntityBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EcommerceSolution.Data.Entities
{
    public class Product : AuditEntity<Guid>
    {
        public float Price { set; get; }
        public float OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; } = 0;
        public int QuantityOrder { set; get; } = 0;
        public bool? IsFeatured { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Guid CategoryId { set; get; }
        public Category Category { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<Review> Reviews { get; set; }
        public List<WishList> WishList { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}