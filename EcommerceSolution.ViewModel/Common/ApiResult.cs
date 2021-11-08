using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceSolution.ViewModels.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }

        public string Message { get; set; }
        public IList<string> Roles { get; set; }

        public T ResultObj { get; set; }
    }
}