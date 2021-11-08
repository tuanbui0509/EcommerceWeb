using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceSolution.InterfaceService;
using EcommerceSolution.ViewModels.Catalog.ProductImage;
using EcommerceSolution.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceWeb.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;
        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductImageAsync([FromForm] ProductImageCreateRequest request)
        {
            var result = await _productImageService.AddImageAsync(request);

            if (result == null)
            {
                return BadRequest("Can not find product image!");
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductImageAsync([FromForm] ProductImageUpdateRequest request)
        {
            await _productImageService.UpdateImageAsync(request);
            return Ok("Updated product image successful");
        }
    }
}
