using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.ViewModel.Common
{
    public class PagingResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalItem { get; set; }
        public T List { get; set; }
    }
}
