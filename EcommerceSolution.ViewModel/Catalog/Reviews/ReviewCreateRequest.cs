using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.ViewModels.Catalog.Reviews
{
    public class ReviewCreateRequest
    {
        public Guid UserId { set; get; }
        public int ProductId { set; get; }

        public string Comment { set; get; }
        public int Rate { set; get; }
    }
}