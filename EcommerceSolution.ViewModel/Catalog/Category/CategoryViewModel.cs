using System;
using System.Collections.Generic;
using System.Text;
using EcommerceSolution.ViewModels.Catolog.Products;

namespace EcommerceSolution.ViewModels.Catalog.Categories
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }

    }
}