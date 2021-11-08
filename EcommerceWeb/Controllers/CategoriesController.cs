using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EcommerceSolution.ViewModel.Catalog.Category;
using EcommerceSolution.InterfaceService;
using EcommerceSolution.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;


        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
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
            var categories = await _categoryService.GetAllAsync(request);
            return Ok(categories);
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
            var cate = await _categoryService.AddAsync(request);
            if (cate == null)
                return BadRequest("Can not add category");
            return Ok(cate);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] CategoryUpdateRequest request)
        {
            await _categoryService.UpdateAsync(request);
            return Ok();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteAsync(Guid categoryId)
        {
            var category = await _categoryService.DeleteAsync(categoryId);
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