using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Model;
using EcommerceSolution.InterfaceRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EcommerceSolution.Repository.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        private readonly EcommerceDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderDetailRepository(EcommerceDBContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(OrderDetailModel orderDetailModel, string userName)
        {
            var orderDetail = new OrderDetail()
            {
                CreatedBy = userName,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UpdatedBy = userName,
                Price = orderDetailModel.Price,
                OrderId = orderDetailModel.OrderId,
                Quantity = orderDetailModel.Quantity,
                ProductId = orderDetailModel.ProductId,
            };
            await _context.OrderDetails.AddAsync(orderDetail);
        }

        public async Task<ICollection<OrderDetailModel>> GetAllAsync(Guid orderId)
        {
            return await _context.OrderDetails.Where(o => o.OrderId == orderId).Select(p => new OrderDetailModel
            {
                Quantity = p.Quantity,
                Price = p.Price,
                ProductId = p.ProductId,
                OrderId = p.OrderId,
            }).ToListAsync();
        }
    }
}
