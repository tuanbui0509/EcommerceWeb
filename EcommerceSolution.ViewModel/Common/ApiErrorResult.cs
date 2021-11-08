using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceSolution.ViewModels.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public ApiErrorResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }

        public ApiErrorResult()
        {
            IsSuccessed = false;
        }

        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }
    }
}