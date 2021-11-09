using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.ViewModels.Catolog.Products
{
    public class ProductCreateRequest
    {
        public string Name { set; get; }
        public float Price { set; get; }
        public float OriginalPrice { set; get; }
        public int Stock { set; get; }
        public string Description { set; get; }
        public bool? IsFeatured { get; set; }
        public Guid CategoryId { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}