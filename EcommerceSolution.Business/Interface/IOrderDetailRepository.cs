using EcommerceSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Model;

namespace EcommerceSolution.InterfaceRepository.Interface
{
    public interface IOrderDetailRepository
    {
        Task CreateAsync(OrderDetailModel orderDetail, string userName);

        //Task<OrderDetail> GetById(int orderId);

        Task<ICollection<OrderDetailModel>> GetAllAsync(Guid orderId);
    }
}
