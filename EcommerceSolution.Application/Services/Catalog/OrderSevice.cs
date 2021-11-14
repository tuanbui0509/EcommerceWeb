using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.ViewModels.Carts;
using EcommerceSolution.ViewModels.Catalog.Orders;
using EcommerceSolution.InterfaceRepository.Interface;
using EcommerceSolution.InterfaceService;
using System.Transactions;
using EcommerceSolution.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EcommerceSolution.Application.Catolog.Orders
{
    public class OrderSevice : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        public OrderSevice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Create(CheckoutRequest request, string userName)
        {
            using (var t = _unitOfWork.CreateTransactionScope((IsolationLevel.ReadCommitted)))
            {

                var order = new OrderModel()
                {
                    ShipAddress = request.Address,
                    OrderDate = DateTime.Now,
                    ShipEmail = request.Email,
                    ShipPhoneNumber = request.PhoneNumber,
                    ShipName = request.Name,
                    Status = Data.Enums.OrderStatus.InProgress,
                    OrderDetails = request.OrderDetails.Select(d => new OrderDetailModel()
                    {
                        Price = d.Price,
                        OrderId = d.OrderId,
                        Quantity = d.Quantity,
                        ProductId = d.ProductId
                    }).ToList()
                };

                await _unitOfWork.Orders.CreateAsync(order, userName);
                var orderDetails = new List<OrderDetailModel>();
                foreach (var item in request.OrderDetails)
                {
                    var orderDetail = new OrderDetailModel()
                    {
                        OrderId = item.OrderId,
                        Quantity = item.Quantity,
                        ProductId = item.ProductId,
                        Price = item.Price,
                    };
                    await _unitOfWork.OrderDetails.CreateAsync(orderDetail, userName);
                    await _unitOfWork.Products.UpdateOrderQuantity(orderDetail.ProductId, orderDetail.Quantity, userName);
                    orderDetails.Add(orderDetail);
                }
                order.OrderDetails = orderDetails;
                await _unitOfWork.CompleteAsync();
                t.Complete();
                return order.Id;
            }
        }

        public async Task ChangeStatusSuccess(Guid orderId, string userName)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) throw new Exception($"Cannot find a Order with id: {orderId}");
            await _unitOfWork.Orders.ChangeStatusSuccessAsync(orderId, userName);
            await _unitOfWork.CompleteAsync();
        }

        public async Task ChangeStatusCancel(Guid orderId, string userName)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) throw new Exception($"Cannot find a Order with id: {orderId}");
            await _unitOfWork.Orders.ChangeStatusSuccessAsync(orderId, userName);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<OrderViewModel> GetById(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            //var orderDetail = _unitOfWork.Orders.e;
            //List<ProductImage> images = await productImage.Where(x => x.ProductId == productId).ToListAsync();
            //var productImages = new List<ProductImageViewModel>();
            //foreach (var item in images)
            //{
            //    productImages.Add(new ProductImageViewModel()
            //    {
            //        Id = item.Id,
            //        ImagePath = item.ImagePath,
            //        IsDefault = item.IsDefault,
            //        ProductId = item.ProductId,
            //    });
            //}

            return new OrderViewModel()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(o => new OrderDetailViewModel()
                {
                    Price = o.Price,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity
                }).ToList(),
                ShipAddress = order.ShipAddress,
                ShipEmail = order.ShipEmail,
                ShipName = order.ShipName,
                ShipPhoneNumber = order.ShipPhoneNumber,
                Status = order.Status,
            };
        }

        public async Task<ICollection<OrderViewModel>> GetAllOrderByUser(Guid id)
        {
            var query = await _unitOfWork.Orders.GetAllOrderByUserAsync(id);
            return query.Select(x => new OrderViewModel()
            {
                Id = x.Id,
                ShipAddress = x.ShipAddress,
                OrderDate = x.OrderDate,
                ShipEmail = x.ShipEmail,
                ShipPhoneNumber = x.ShipPhoneNumber,
                ShipName = x.ShipName,
                UserId = x.UserId,
                Status = x.Status,
            }).ToList();
        }

        public async Task<ICollection<OrderViewModel>> GetAllOrder()
        {
            var query = await _unitOfWork.Orders.GetAllAsync();
            return query.Select(x => new OrderViewModel()
            {
                Id = x.Id,
                ShipAddress = x.ShipAddress,
                OrderDate = x.OrderDate,
                ShipEmail = x.ShipEmail,
                ShipPhoneNumber = x.ShipPhoneNumber,
                ShipName = x.ShipName,
                UserId = x.UserId,
                Status = x.Status,
            }).ToList();
        }
    }
}