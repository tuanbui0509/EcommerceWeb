using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceSolution.ViewModels.Catalog.Categories
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public int Status { set; get; }

        public string Name { get; set; }
    }
}