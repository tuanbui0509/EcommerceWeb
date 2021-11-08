using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EcommerceSolution.ViewModel.Catalog.Category
{
    public class CategoryCreateRequest
    {
        public int Status { set; get; }

        public string Name { get; set; }
        //public string Thumb { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
