using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.Data.Model
{
    public class ProductImageModel
    {
        public Guid Id { get; set; }
        public int SortOrder { get; set; }

        public Guid ProductId { get; set; }

        public string ImagePath { get; set; }
    }
}
