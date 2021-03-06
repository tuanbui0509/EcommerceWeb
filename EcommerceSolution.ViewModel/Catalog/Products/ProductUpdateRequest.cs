using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.ViewModels.Catolog.Products
{
    public class ProductUpdateRequest
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public bool? IsFeatured { get; set; }
    }
}