using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceSolution.Application.Catolog.Orders;
using EcommerceSolution.ViewModels.Carts;
using EcommerceSolution.InterfaceService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        [ActionName(nameof(GetById))]
        public async Task<ActionResult> GetById(Guid orderId)
        {
            var order = await _orderService.GetById(orderId);
            if (order == null)
                return BadRequest("Cannot find product");
            return Ok(order);
        }

        //[HttpGet]
        //public async Task<ActionResult> GetAllOrder()
        //{
        //    var orders = await _orderService.GetAllOrder();
        //    return Ok(orders);
        //}

        [HttpGet("GetAllOrder/{userId}")]
        public async Task<ActionResult> GetAllOrder(Guid userId)
        {
            var orders = await _orderService.GetAllOrderByUser(userId);
            return Ok(orders);
        }

        [HttpPatch("ChangeStatusSuccess")]
        public async Task<ActionResult> ChangeStatusSuccess(Guid orderId)
        {
            await _orderService.ChangeStatusSuccess(orderId);
            return Ok();
        }
        [HttpPatch("ChangeStatusCancel")]
        public async Task<ActionResult> ChangeStatusCancel(Guid orderId)
        {
            await _orderService.ChangeStatusCancel(orderId);
            return Ok();
        }

        [HttpPost]
        [ActionName(nameof(CreateOrder))]
        public async Task<ActionResult> CreateOrder([FromBody] CheckoutRequest request)
        {
            var OrderId = await _orderService.Create(request);
            if (OrderId.ToString() == "0000-0000-000-000")
                return BadRequest();
            var order = await _orderService.GetById(OrderId);
            return CreatedAtAction(nameof(GetById), new { orderId = OrderId }, order);
        }
    }
}