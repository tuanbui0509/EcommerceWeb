using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceSolution.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj, IList<string> roles, string message)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
            Roles = roles;
            Message = message;
        }

        public ApiSuccessResult(T resultObj, IList<string> roles)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
            Roles = roles;
        }

        public ApiSuccessResult(T resultObj, string message)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
            Message = message;
        }

        public ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }
        public ApiSuccessResult(string message)
        {
            IsSuccessed = true;
            Message = message;
        }

        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}