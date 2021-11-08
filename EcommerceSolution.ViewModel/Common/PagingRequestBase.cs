using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.ViewModels.Common
{
    public class PagingRequestBase
    {
        public int _page { get; set; } = 1;
        public int _limit { get; set; } = 5;
    }
}
