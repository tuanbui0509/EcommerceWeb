using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EcommerceSolution.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EcommerceSolution.Repository.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDBContext _context;


        public OrderRepository(EcommerceDBContext context)
        {
            _context = context;

        }

        public async Task ChangeStatusSuccessAsync(Guid orderId, string userName)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(d => d.Id == orderId);
            order.Status = (Data.Enums.OrderStatus.Success);
            order.UpdatedDate = DateTime.UtcNow;
            order.UpdatedBy = userName;
        }

        public async Task ChangeStatusCancelAsync(Guid orderId, string userName)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(d => d.Id == orderId);
            order.Status = (Data.Enums.OrderStatus.Canceled);
            order.UpdatedDate = DateTime.UtcNow;
            order.UpdatedBy = userName;
        }


        public async Task CreateAsync(OrderModel orderModel, string userName)
        {
            var order = new Order()
            {
                UserId = orderModel.UserId,
                CreatedBy = userName,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                UpdatedBy = userName,
                Status = orderModel.Status,
                ShipName = orderModel.ShipName,
                OrderDate = orderModel.OrderDate,
                ShipPhoneNumber = orderModel.ShipPhoneNumber,
                ShipEmail = orderModel.ShipEmail,
                ShipAddress = orderModel.ShipAddress,
                OrderDetails = orderModel.OrderDetails.Select(x => new OrderDetail()
                {
                    CreatedBy = userName,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedBy = userName,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    OrderId = x.OrderId
                }).ToList()
            };
            await _context.Orders.AddAsync(order);
        }

        public async Task<ICollection<OrderModel>> GetAllOrderByUserAsync(Guid id)
        {
            try
            {
                return await _context.Orders
                    .Where(o => o.UserId == id)
                    .Select(x => new OrderModel
                    {
                        Id = x.Id,
                        OrderDate = x.OrderDate,
                        ShipName = x.ShipName,
                        ShipEmail = x.ShipEmail,
                        ShipPhoneNumber = x.ShipPhoneNumber,
                        Status = x.Status,
                        ShipAddress = x.ShipAddress,
                        OrderDetails = x.OrderDetails.Where(s => s.OrderId == x.Id).Select(d => new OrderDetailModel()
                        {
                            Price = d.Price,
                            OrderId = d.OrderId,
                            Quantity = d.Quantity,
                            ProductId = d.ProductId
                        }).ToList()
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<OrderModel> GetByIdAsync(Guid orderId)
        {
            return await _context.Orders.Where(o => o.Id == orderId).Select(x => new OrderModel()
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                ShipName = x.ShipName,
                ShipEmail = x.ShipEmail,
                ShipPhoneNumber = x.ShipPhoneNumber,
                Status = x.Status,
                ShipAddress = x.ShipAddress,
                OrderDetails = x.OrderDetails.Where(s => s.OrderId == x.Id).Select(d => new OrderDetailModel()
                {
                    Price = d.Price,
                    OrderId = d.OrderId,
                    Quantity = d.Quantity,
                    ProductId = d.ProductId
                }).ToList()
            }).FirstOrDefaultAsync();

        }

        public async Task<ICollection<OrderModel>> GetAllAsync()
        {
            try
            {
                return await _context.Orders
                    .Select(x => new OrderModel
                    {
                        Id = x.Id,
                        OrderDate = x.OrderDate,
                        ShipName = x.ShipName,
                        ShipEmail = x.ShipEmail,
                        ShipPhoneNumber = x.ShipPhoneNumber,
                        Status = x.Status,
                        ShipAddress = x.ShipAddress,
                        OrderDetails = x.OrderDetails.Where(s => s.OrderId == x.Id).Select(d => new OrderDetailModel()
                        {
                            Price = d.Price,
                            OrderId = d.OrderId,
                            Quantity = d.Quantity,
                            ProductId = d.ProductId
                        }).ToList()
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task DeleteAsync(Guid orderId, string userName)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(d => d.Id == orderId);
            order.IsDeleted = true;
            order.UpdatedDate = DateTime.UtcNow;
            order.UpdatedBy = userName;
        }
    }
}
