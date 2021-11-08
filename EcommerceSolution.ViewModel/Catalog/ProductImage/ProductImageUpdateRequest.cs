using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.ViewModels.Catalog.ProductImage
{
    public class ProductImageUpdateRequest
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
