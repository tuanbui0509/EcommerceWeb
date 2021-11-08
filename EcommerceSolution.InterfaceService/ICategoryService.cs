using EcommerceSolution.ViewModel.Catalog.Category;
using EcommerceSolution.ViewModels.Catalog.Categories;
using EcommerceSolution.ViewModels.Catolog.Products;
using EcommerceSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.ViewModel.Common;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSolution.InterfaceService
{
    public interface ICategoryService
    {
        Task<PagingResponse<List<CategoryViewModel>>> GetAllAsync(PagingRequestBase request);

        Task<PagingResponse<List<ProductViewModel>>> GetAllProductByIdAsync(Guid categoryId, PagingRequestBase request);

        Task<ApiResult<CategoryViewModel>> GetByIdAsync(Guid id);
        Task<CategoryViewModel> AddAsync(CategoryCreateRequest request);
        Task UpdateAsync(CategoryUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
