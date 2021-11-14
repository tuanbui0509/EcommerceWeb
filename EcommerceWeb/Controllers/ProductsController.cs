using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EcommerceSolution.ViewModels.Catolog.Products;
using EcommerceSolution.InterfaceService;
using EcommerceSolution.Utilities.Cache;
using EcommerceSolution.ViewModels.Common;
using EcommerceWeb.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EcommerceSolution.BackendApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : SuperController
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService, ILogger<ProductsController> logger,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _productService = productService;
            _logger = logger;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProductAsync([FromQuery] PagingRequestBase request)
        {
            try
            {
                var products = await _productService.GetAllProductAsync(request);
                _logger.LogInformation("[{@DateTime}] GetAllProduct {@products}", DateTime.UtcNow, products);
                return Ok(products);
            }
            catch (Exception e)
            {
                _logger.LogError("Error: ", e.Data);
                return BadRequest("Error: " + e);
            }
        }

        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetByIdAsync(Guid productId)
        {
            try
            {
                var product = await _productService.GetByIdAsync(productId);
                _logger.LogInformation("[{@DateTime}] GetByIdAsync {@product}", DateTime.UtcNow, product);

                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogError("Error: ", e);
                return BadRequest("Can not find Product by id: " + e);
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreateProductAsync([FromForm] ProductCreateRequest request)
        {
            var result = await _productService.CreateAsync(request, CurrentUsername);
            if (result == null)
            {
                return BadRequest("Can not find product!");
            }
            return Ok(result);

        }

        [HttpGet("featured")]
        [AllowAnonymous]
        public async Task<ActionResult> GetFeaturedProductsAsync([FromQuery] PagingRequestBase request)
        {

            var products = await _productService.GetFeaturedProductsAsync(request);
            _logger.LogInformation("[{@DateTime}] GetFeaturedProductsAsync {@products}", DateTime.UtcNow, products);
            return Ok(products);

        }

        [HttpGet("latest")]
        [AllowAnonymous]
        public async Task<ActionResult> GetLatestProductsAsync([FromQuery] PagingRequestBase request)
        {
            var products = await _productService.GetLatestProductsAsync(request);
            _logger.LogInformation("[{@DateTime}] GetLatestProductsAsync {@products}", DateTime.UtcNow, products);
            return Ok(products);
        }

        [HttpGet("best-seller")]
        [AllowAnonymous]
        public async Task<ActionResult> GetBestSellerProductsAsync([FromQuery] PagingRequestBase request)
        {
            var products = await _productService.GetBestSellerProductsAsync(request);
            _logger.LogInformation("[{@DateTime}] GetBestSellerProductsAsync {@products}", DateTime.UtcNow, products);
            return Ok(products);
        }

        [HttpDelete]

        public async Task<ActionResult> DeleteAsync([FromForm] Guid productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
                return BadRequest("Delete Unsuccessful");
            var result = await _productService.DeleteAsync(productId, CurrentUsername);
            return Ok(result);
        }



        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateAsync(request, CurrentUsername);
            return Ok(result);
        }
        #region Api other

        //[HttpPatch("AddViewcount/{productId}")]
        //public async Task<IActionResult> AddViewcountAsync(Guid productId)
        //{
        //    await _productService.AddViewCountAsync(productId);
        //    var product = await _productService.GetByIdAsync(productId);
        //    return Ok(product);
        //}

        //[HttpPatch("ChangeActive/{productId}")]
        //public async Task<IActionResult> ChangeActiveAsync(Guid productId)
        //{
        //    await _productService.ChangeActiveAsync(productId);
        //    return Ok();
        //}


        //[HttpDelete("{productId}")]
        //public async Task<IActionResult> Delete(Guid productId)
        //{
        //    var affectedResult = await _productService.Delete(productId);
        //    if (affectedResult == 0)
        //        return BadRequest();
        //    return Ok();
        //}

        //[HttpPatch("{productId}/{newPrice}")]
        //public async Task<IActionResult> UpdatePrice(Guid productId, decimal newPrice)
        //{
        //    var isSuccessful = await _productService.UpdatePrice(productId, newPrice);
        //    if (isSuccessful)
        //        return Ok();

        //    return BadRequest();
        //}

        //[HttpPost("{productId}/images")]
        //[AllowAnonymous]
        //public async Task<IActionResult> CreateImage(Guid productId, [FromForm] ProductImageCreateRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var imageId = await _productService.AddImage(productId, request);
        //    if (imageId == 0)
        //        return BadRequest();

        //    var image = await _productService.GetImageById(imageId);

        //    return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        //}

        //[HttpPut("{productId}/images/{imageId}")]
        //[Authorize]
        //public async Task<IActionResult> UpdateImage(Guid imageId, [FromForm] ProductImageUpdateRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var result = await _productService.UpdateImage(imageId, request);
        //    if (result == 0)
        //        return BadRequest();

        //    return Ok();
        //}

        //[HttpDelete("{productId}/images/{imageId}")]
        //[Authorize]
        //public async Task<IActionResult> RemoveImage(Guid imageId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var result = await _productService.RemoveImage(imageId);
        //    if (result == 0)
        //        return BadRequest();

        //    return Ok();
        //}

        //[HttpGet("{productId}/images/{imageId}")]
        //public async Task<IActionResult> GetImageById(Guid productId, Guid imageId)
        //{
        //    var image = await _productService.GetImageById(imageId);
        //    if (image == null)
        //        return BadRequest("Cannot find product");
        //    return Ok(image);
        //}

        #endregion Api other
    }
}