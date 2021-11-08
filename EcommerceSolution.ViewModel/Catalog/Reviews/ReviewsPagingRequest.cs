using EcommerceSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.ViewModel.Catalog.Reviews
{
    public class ReviewsPagingRequest : PagingRequestBase
    {
        public int ProductId { set; get; }
    }
}
