using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
        public async Task AddAsync(Category category)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(userName);
            category.CreatedDate = DateTime.Now;
            category.IsDeleted = false;
            category.UpdatedDate = DateTime.Now;
            category.UpdatedBy = user.FirstName + " " + user.LastName;
            category.CreatedBy = user.FirstName + " " + user.LastName;
            await _context.Set<Category>().AddAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<Category>().FindAsync(id);
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(userName);
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = user.FirstName + " " + user.LastName;
            entity.IsDeleted = true;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Set<Category>().ToListAsync();
        }


        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.Set<Category>()
              .FindAsync(id);
        }

        public async Task UpdateAsync(Category category)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(userName);
            category.UpdatedDate = DateTime.Now;
            category.UpdatedBy = user.FirstName + " " + user.LastName;
            _context.Update(category);
        }
    }
}
