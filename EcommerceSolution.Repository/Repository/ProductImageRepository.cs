using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSolution.Repository.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly EcommerceDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductImageRepository(EcommerceDBContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public IQueryable<ProductImage> Entities => _context.Set<ProductImage>();

        public async Task AddAsync(ProductImageModel productImageModel, string userName)
        {
            var productImage = new ProductImage()
            {
                CreatedBy = userName,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UpdatedDate = DateTime.Now,
                UpdatedBy = userName,
                ProductId = productImageModel.ProductId,
                ImagePath = productImageModel.ImagePath,
                SortOrder = productImageModel.SortOrder
            };
            await _context.ProductImages.AddAsync(productImage);
        }

        public async Task DeleteAsync(Guid id, string userName)
        {
            var productImage = await _context.ProductImages.FirstOrDefaultAsync(d => d.Id == id);
            productImage.IsDeleted = true;
            productImage.UpdatedDate = DateTime.Now;
            productImage.UpdatedBy = userName;
        }

        public async Task<ICollection<ProductImageModel>> GetAllByProductIdAsync(Guid id)
        {
            return await _context.ProductImages.Select(d => new ProductImageModel()
            {
                Id = d.Id,
                ImagePath = d.ImagePath,
                SortOrder = d.SortOrder,
                ProductId = d.ProductId
            }).ToListAsync();
        }


        public async Task<ProductImageModel> GetByIdAsync(Guid id)
        {
            var image = await _context.ProductImages.FirstOrDefaultAsync(d => d.Id == id);
            return new ProductImageModel()
            {
                Id = image.Id,
                ImagePath = image.ImagePath,
                SortOrder = image.SortOrder,
                ProductId = image.ProductId
            };
        }


        public async Task UpdateAsync(ProductImageModel productImageModel, string userName)
        {
            var productImage = await _context.ProductImages.FirstOrDefaultAsync(d => d.Id == productImageModel.Id);
            productImage.UpdatedDate = DateTime.Now;
            productImage.UpdatedBy = userName;
            _context.ProductImages.Update(productImage);
        }
    }
}
