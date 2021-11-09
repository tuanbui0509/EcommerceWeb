using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.ViewModels.Catalog.ProductImage;

namespace EcommerceSolution.ViewModels.Catolog.Products
{
    public class ProductViewModel
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public float Price { set; get; }
        public float OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Description { set; get; }
        public bool? IsFeatured { get; set; }
        public List<ProductImageViewModel> ListImage { get; set; }
        public string CategoryName { get; set; }
        public Guid CategoryId { set; get; }
        public int QuantityOrder { set; get; }
    }
}