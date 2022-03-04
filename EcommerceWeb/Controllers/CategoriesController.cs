using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EcommerceSolution.ViewModel.Catalog.Category;
using EcommerceSolution.InterfaceService;
using EcommerceSolution.Utilities.Cache;
using EcommerceSolution.ViewModels.Common;
using EcommerceWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace EcommerceSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CategoriesController : SuperController
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMemoryCacheHelper _memoryCache;


        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger, IMemoryCacheHelper memoryCache,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _categoryService = categoryService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> GetAllAsync([FromQuery] PagingRequestBase request)
        {
            //_logger.LogInformation("Log message generated with INFORMATION severity level.");
            //_logger.LogWarning("Log message generated with WARNING severity level.");
            //_logger.LogError("Log message generated with ERROR severity level.");
            //_logger.LogCritical("Log message log generated with CRITICAL severity level.");
            string key = "categories";
            var value = _memoryCache.GetValue(key);
            if (value != null)
            {
                _logger.LogInformation("Get categories in cache");
                return Ok(value);
            }
            else
            {
                _logger.LogInformation("Add categorie in cache with key = ", key);
                var categories = await _categoryService.GetAllAsync(request);
                _memoryCache.Add(key, categories, DateTimeOffset.UtcNow);
                return Ok(categories);

            }
        }
        [AllowAnonymous]

        [HttpGet("{categoryId}")]
        [ActionName(nameof(GetByIdAsync))]

        public async Task<IActionResult> GetByIdAsync(Guid categoryId)
        {
            var category = await _categoryService.GetByIdAsync(categoryId);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromForm] CategoryCreateRequest request)
        {
            var res = await _categoryService.AddAsync(request, CurrentUsername);
            if (res.IsSuccessed)
                return Ok(res);
            return BadRequest("Can not add category");

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] CategoryUpdateRequest request)
        {
            await _categoryService.UpdateAsync(request, CurrentUsername);
            return Ok();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteAsync(Guid categoryId)
        {
            var category = await _categoryService.DeleteAsync(categoryId, CurrentUsername);
            return Ok(category);
        }

        [HttpGet("{categoryId}/products")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAllProductByIdAsync(Guid categoryId, [FromQuery] PagingRequestBase request)
        {
            var products = await _categoryService.GetAllProductByIdAsync(categoryId, request);
            return Ok(products);
        }
    }
}