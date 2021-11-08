using EcommerceSolution.ViewModels.Catalog.Orders;
using EcommerceSolution.ViewModels.Common;
using EcommerceSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.InterfaceService
{
    public interface IUserService
    {
        Task<ApiResult<string>> AuthencateAsync(LoginRequest request);

        Task<ApiResult<bool>> RegisterAsync(RegisterRequest request);

        Task<ApiResult<bool>> UpdateUserAsync(UserUpdateRequest request);

        Task<List<UserViewModel>> GetUsersAsync();

        Task<ApiResult<UserViewModel>> GetByIdAsync(Guid id);

        Task<List<OrderViewModel>> GetOrdersByUserAsync(Guid userId);

        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
