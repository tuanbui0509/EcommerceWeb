using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EcommerceSolution.InterfaceService;
using EcommerceSolution.ViewModels.Catalog.ProductImage;
using EcommerceSolution.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace EcommerceWeb.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class ProductImageController : SuperController
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _productImageService = productImageService;
        }
        [HttpPost]
        public async Task<ActionResult> CreateProductImageAsync([FromForm] ProductImageCreateRequest request)
        {
            var result = await _productImageService.AddImageAsync(request, CurrentUsername);

            if (result == null)
            {
                return BadRequest("Can not find product image!");
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductImageAsync([FromForm] ProductImageUpdateRequest request)
        {
            await _productImageService.UpdateImageAsync(request, CurrentUsername);
            return Ok("Updated product image successful");
        }
    }
}
