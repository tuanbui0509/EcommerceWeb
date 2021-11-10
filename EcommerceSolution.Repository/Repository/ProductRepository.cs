using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Model;

namespace EcommerceSolution.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDBContext _context;
        public ProductRepository(EcommerceDBContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Entities => _context.Set<Product>();

        public async Task AddProductAsync(ProductModel productModel, string userName)
        {
            var product = new Product()
            {
                OriginalPrice = productModel.OriginalPrice,
                Stock = productModel.Stock,
                ViewCount = 0,
                Name = productModel.Name,
                Description = productModel.Description,
                CategoryId = productModel.CategoryId,
                IsFeatured = productModel.IsFeatured,
                Price = productModel.Price,
                CreatedBy = userName,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                UpdatedBy = userName,
            };
            await _context.Products.AddAsync(product);
        }

        public async Task<ICollection<ProductModel>> GetAllProductsAsync()
        {

            return await _context.Products
                .Where(x => x.IsDeleted == false)
                .Select(x => new ProductModel
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
                }).ToListAsync();
        }

        public async Task<ICollection<ProductModel>> GetBestSellerProducts()
        {
            return await _context.Products.Where(p => p.IsDeleted == false)
                .OrderByDescending(p => p.QuantityOrder).Select(x => new ProductModel
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
                }).ToListAsync();
        }

        public async Task<ProductModel> GetByIdAsync(Guid productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(d => d.Id == productId);
            var productImages = await _context.ProductImages.Where(d => d.ProductId == productId).ToListAsync();
            return new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                CreatedDate = product.CreatedDate,
                Description = product.Description,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
                QuantityOrder = product.QuantityOrder,
                CategoryId = product.CategoryId,
                IsFeatured = product.IsFeatured,
                ProductImages = productImages.Select(i => new ProductImageModel()
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    SortOrder = i.SortOrder
                }).ToList()
            };
        }

        public async Task<ICollection<ProductModel>> GetFeaturedProducts()
        {
            return await _context.Products.Where(p => p.IsDeleted == false && p.IsFeatured == true).OrderByDescending(p => p.ViewCount).Select(x => new ProductModel
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
            }).ToListAsync();
        }

        public async Task<ICollection<ProductModel>> GetLatestProducts()
        {
            return await _context.Products.Where(p => p.IsDeleted == false).OrderBy(p => p.CreatedDate).Select(x => new ProductModel
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
            }).ToListAsync();
        }


        public async Task UpdateProductAsync(ProductModel productModel, string userName)
        {
            var product = await _context.Products.FirstOrDefaultAsync(d => d.Id == productModel.Id);
            product.Name = productModel.Name ?? product.Name;
            product.Description = productModel.Description ?? product.Description;
            product.IsFeatured = productModel.IsFeatured ?? product.IsFeatured;
            product.UpdatedDate = DateTime.UtcNow;
            product.UpdatedBy = userName;
        }

        public async Task DeleteProductAsync(Guid productId, string userName)
        {
            var product = await _context.Products.FirstOrDefaultAsync(d => d.Id == productId);
            product.UpdatedDate = DateTime.UtcNow;
            product.UpdatedBy = userName;
            product.IsDeleted = true;
        }

        //public async Task<bool> UpdateStockAsync(Guid productId, int addedQuantity)
        //{
        //    var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var user = await _userManager.FindByNameAsync(userName);
        //                var product = await _context.Products.FirstOrDefaultAsync(d => d.Id == productId);
        //    if (product == null)
        //        return false;
        //    product.Stock += addedQuantity;
        //    product.UpdatedDate = DateTime.UtcNow;
        //    product.UpdatedBy = user.FirstName + " " + user.LastName;
        //    return true;
        //}
        public async Task<bool> UpdateOrderQuantity(Guid productId, int addedQuantity, string userName)
        {
            var product = await _context.Products.FirstOrDefaultAsync(d => d.Id == productId);
            product.Stock -= addedQuantity;
            product.QuantityOrder += addedQuantity;

            product.UpdatedDate = DateTime.UtcNow;
            product.UpdatedBy = userName;
            return true;
        }
        //public async Task<bool> UpdatePriceAsync(Guid productId, float newPrice)
        //{
        //    var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var user = await _userManager.FindByNameAsync(userName);
        //                var product = await _context.Products.FirstOrDefaultAsync(d => d.Id == productId);
        //    if (product == null)
        //        return false;
        //    product.Price = newPrice;
        //    product.UpdatedDate = DateTime.UtcNow;
        //    product.UpdatedBy = user.FirstName + " " + user.LastName;
        //    return true;
        //}
        //public async Task<bool> ChangeActiveAsync(Guid productId)
        //{
        //    var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var user = await _userManager.FindByNameAsync(userName);
        //    var product = await _context.Set<Product>().FindAsync(productId);
        //    if (product == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        product.IsActive = !product.IsActive;
        //        product.UpdatedDate = DateTime.UtcNow;
        //        product.UpdatedBy = user.FirstName + " " + user.LastName;
        //        return true;
        //    }
        //}


    }
}
