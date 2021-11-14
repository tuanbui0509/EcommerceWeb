using EcommerceSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using EcommerceSolution.ViewModels.Catolog.Products;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceService;
using EcommerceSolution.ViewModels.Common;
using EcommerceSolution.ViewModel.Catalog.Category;
using EcommerceSolution.InterfaceRepository.Interface;
using EcommerceSolution.ViewModels.Catalog.ProductImage;
using System.Transactions;
using EcommerceSolution.Application.Common;
using EcommerceSolution.Data.Enums;
using EcommerceSolution.Data.Model;
using EcommerceSolution.Utilities.Cache;
using EcommerceSolution.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EcommerceSolution.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryViewModel> AddAsync(CategoryCreateRequest request, string userName)
        {
            using var t = _unitOfWork.CreateTransaction();
            {
                var cate = new CategoryModel()
                {
                    Name = request.Name,
                };

                await _unitOfWork.Categories.AddAsync(cate, userName);
                await _unitOfWork.CompleteAsync();
                t.Commit();
                return new CategoryViewModel()
                {
                    Id = cate.Id,
                    Name = cate.Name,
                };
            }

        }

        public async Task<ApiResult<string>> DeleteAsync(Guid id, string userName)
        {
            await _unitOfWork.Categories.DeleteAsync(id, userName);
            await _unitOfWork.CompleteAsync();
            return new ApiSuccessResult<string>("Updated category successful");
        }

        public async Task<PagingResponse<List<CategoryViewModel>>> GetAllAsync(PagingRequestBase request)
        {
            var query = await _unitOfWork.Categories.GetAllAsync();
            int TotalItem = query.Count;
            var list = query.Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Products = x.Products.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    IsFeatured = p.IsFeatured,
                    CategoryName = p.CategoryName,
                    Name = p.Name,
                    CreatedDate = p.CreatedDate,
                    Description = p.Description,
                    OriginalPrice = p.OriginalPrice,
                    Price = p.Price,
                    Stock = p.Stock,
                    ViewCount = p.ViewCount,
                    QuantityOrder = p.QuantityOrder,
                    CategoryId = p.CategoryId,
                    ListImage = p.ProductImages.Select(i => new ProductImageViewModel()
                    {
                        Id = i.Id,
                        ImagePath = i.ImagePath,
                        SortOrder = i.SortOrder,
                        ProductId = i.ProductId,
                    }).ToList()
                }).ToList()
            }).Skip((request._page - 1) * request._limit).Take(request._limit).ToList();
            return new PagingResponse<List<CategoryViewModel>>()
            {
                List = list,
                TotalItem = TotalItem,
                CurrentPage = request._page
            };
        }

        public async Task<PagingResponse<List<ProductViewModel>>> GetAllProductByIdAsync(Guid categoryId, PagingRequestBase request)
        {
            var query = await _unitOfWork.Categories.GetAllProductByIdAsync(categoryId);
            var products = query.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                IsFeatured = p.IsFeatured,
                CategoryName = p.CategoryName,
                Name = p.Name,
                CreatedDate = p.CreatedDate,
                Description = p.Description,
                OriginalPrice = p.OriginalPrice,
                Price = p.Price,
                Stock = p.Stock,
                ViewCount = p.ViewCount,
                QuantityOrder = p.QuantityOrder,
                CategoryId = p.CategoryId,
                ListImage = p.ProductImages.Select(i => new ProductImageViewModel()
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    SortOrder = i.SortOrder,
                    ProductId = i.ProductId,
                }).ToList()
            }).ToList();
            int TotalItem = products.Count;
            var list = products.Skip((request._page - 1) * request._limit).Take(request._limit).ToList();
            return new PagingResponse<List<ProductViewModel>>()
            {
                List = list,
                TotalItem = TotalItem,
                CurrentPage = request._page
            };
        }

        public async Task<ApiResult<CategoryViewModel>> GetByIdAsync(Guid id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            var categoryViewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,

            };
            return new ApiSuccessResult<CategoryViewModel>(categoryViewModel);
        }

        public async Task<ApiResult<string>> UpdateAsync(CategoryUpdateRequest request, string userName)
        {
            using (var t = _unitOfWork.CreateTransactionScope(IsolationLevel.ReadCommitted))
            {
                var cate = await _unitOfWork.Categories.GetByIdAsync(request.Id);
                if (cate == null) throw new Exception($"Cannot find a category with id: {request.Id}");
                cate.Name = request.Name;
                await _unitOfWork.Categories.UpdateAsync(cate, userName);
                await _unitOfWork.CompleteAsync();
                t.Complete();
                return new ApiSuccessResult<string>("Updated category successful");

            }
        }
    }
}