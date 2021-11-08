using EcommerceSolution.ViewModels.Carts;
using EcommerceSolution.ViewModels.Catalog.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.InterfaceService
{
    public interface IOrderService
    {
        Task<Guid> Create(CheckoutRequest request);

        Task ChangeStatusSuccess(Guid orderId);
        Task ChangeStatusCancel(Guid orderId);

        Task<OrderViewModel> GetById(Guid orderId);

        Task<ICollection<OrderViewModel>> GetAllOrderByUser(Guid id);
        Task<ICollection<OrderViewModel>> GetAllOrder();
    }
}
