using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.Data.Model;
using EcommerceSolution.ViewModels.Carts;
using EcommerceSolution.ViewModels.Catalog.Orders;

namespace EcommerceSolution.InterfaceRepository.Interface
{
    public interface IOrderRepository
    {
        Task CreateAsync(OrderModel orderModel, string userName);
        Task DeleteAsync(Guid orderId, string userName);
        Task ChangeStatusSuccessAsync(Guid orderId, string userName);
        Task ChangeStatusCancelAsync(Guid orderId, string userName);
        Task<OrderModel> GetByIdAsync(Guid orderId);
        Task<ICollection<OrderModel>> GetAllOrderByUserAsync(Guid id);
        Task<ICollection<OrderModel>> GetAllAsync();
    }
}