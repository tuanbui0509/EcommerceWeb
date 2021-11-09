using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.Data.Model;
using EcommerceSolution.ViewModel.Catalog.Category;
using EcommerceSolution.ViewModels.Catalog.Categories;
using EcommerceSolution.ViewModels.Catolog.Products;

namespace EcommerceSolution.InterfaceRepository.Interface
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAllAsync();

        Task<CategoryModel> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id, string userName);
        Task AddAsync(CategoryModel categoryModel, string userName);

        Task UpdateAsync(CategoryModel categoryModel, string userName);
    }
}