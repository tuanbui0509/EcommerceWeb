using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EcommerceSolution.Data.Enums;
using EcommerceSolution.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EcommerceSolution.Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        EcommerceDBContext _context;
        protected readonly ILogger _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryRepository(EcommerceDBContext context, ILogger<CategoryRepository> logger,
            IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task AddAsync(CategoryModel categoryModel, string userName)
        {
            var category = new Category()
            {
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UpdatedBy = userName,
                CreatedBy = userName,
                Name = categoryModel.Name,

            };
            await _context.Set<Category>().AddAsync(category);
        }

        public async Task DeleteAsync(Guid id, string userName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            category.UpdatedDate = DateTime.Now;
            category.UpdatedBy = userName;
            category.IsDeleted = true;
        }

        public async Task<ICollection<CategoryModel>> GetAllAsync()
        {
            return await _context.Categories.Select(c => new CategoryModel()
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(x => new ProductModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    OriginalPrice = x.OriginalPrice,
                    Price = x.Price,
                    Stock = x.Stock,
                    ViewCount = x.ViewCount,
                    QuantityOrder = x.QuantityOrder,
                    CategoryId = x.CategoryId,
                    IsFeatured = x.IsFeatured,
                    CategoryName = x.Category.Name,
                    ProductImages = x.ProductImages.Select(i => new ProductImageModel()
                    {
                        Id = i.Id,
                        ImagePath = i.ImagePath,
                        SortOrder = i.SortOrder
                    }).ToList()
                }).ToList()
            }).ToListAsync();
        }


        public async Task<CategoryModel> GetByIdAsync(Guid id)
        {
            var c = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return new CategoryModel()
            {
                Id = c.Id,
                Name = c.Name,
            };
        }

        public async Task UpdateAsync(CategoryModel categoryModel, string userName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryModel.Id);
            category.UpdatedDate = DateTime.Now;
            category.UpdatedBy = userName;
            category.Name = categoryModel.Name ?? category.Name;
            _context.Update(category);
        }

        public async Task<ICollection<ProductModel>> GetAllProductByIdAsync(Guid id)
        {
            //var c = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            //return await _context.Products.Where(p => p.CategoryId == id).Select(x => new ProductModel()
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreatedDate = x.CreatedDate,
            //    Description = x.Description,
            //    OriginalPrice = x.OriginalPrice,
            //    Price = x.Price,
            //    Stock = x.Stock,
            //    ViewCount = x.ViewCount,
            //    QuantityOrder = x.QuantityOrder,
            //    CategoryId = x.CategoryId,
            //    IsFeatured = x.IsFeatured,
            //    CategoryName = x.Category.Name,
            //    ProductImages = x.ProductImages.Select(i => new ProductImageModel()
            //    {
            //        Id = i.Id,
            //        ImagePath = i.ImagePath,
            //        SortOrder = i.SortOrder
            //    }).ToList()
            //})
            //    .ToListAsync();
            var products = await _context.Products.FromSqlRaw("exec [SP_GetAllProductsByCategoryId] @p0", id).ToListAsync();
            return products.Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                OriginalPrice = x.OriginalPrice,
                Price = x.Price,
                Stock = x.Stock,
                ViewCount = x.ViewCount,
                QuantityOrder = x.QuantityOrder,
                CategoryId = x.CategoryId,
                IsFeatured = x.IsFeatured,
                //CategoryName = x.Category.Name,
                ProductImages = _context.ProductImages.Where(i => i.ProductId == x.Id).Select(i => new ProductImageModel()
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    SortOrder = i.SortOrder
                }).ToList()
            })
                .ToList();

        }
    }
}
