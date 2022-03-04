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
        Task<ApiResult<CategoryViewModel>> AddAsync(CategoryCreateRequest request, string userName);
        Task<ApiResult<string>> UpdateAsync(CategoryUpdateRequest request, string userName);

        Task<ApiResult<string>> DeleteAsync(Guid id, string userName);
    }
}
