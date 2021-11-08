using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.ViewModel.Catalog.Category;
using EcommerceSolution.ViewModels.Catalog.Categories;
using EcommerceSolution.ViewModels.Catolog.Products;

namespace EcommerceSolution.InterfaceRepository.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task AddAsync(Category category);

        Task UpdateAsync(Category category);
    }
}