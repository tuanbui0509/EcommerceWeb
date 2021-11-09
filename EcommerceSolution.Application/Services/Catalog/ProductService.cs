using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using EcommerceSolution.Application.Common;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.Data.Model;
using EcommerceSolution.ViewModels.Catalog.ProductImage;
using EcommerceSolution.ViewModels.Catolog.Products;
using EcommerceSolution.ViewModels.Common;
using EcommerceSolution.InterfaceService;
using EcommerceSolution.InterfaceRepository.Interface;
using EcommerceSolution.ViewModel.Common;
using Microsoft.Extensions.Logging;
namespace EcommerceSolution.Application.Services.Catalog
{
    public class ProductService : IProductService
    {
        private readonly IBlobService _blobService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<ProductService> _logger;
        private const string ContainerName = "images";
        public ProductService(IUnitOfWork unitOfWork, IBlobService blobService,
            IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _blobService = blobService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task AddViewCountAsync(Guid productId)
        {
            var product = _unitOfWork.Products.GetByIdAsync(productId);
            product.Result.ViewCount += 1;
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ProductViewModel> CreateAsync(ProductCreateRequest request)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(userName);
            using (var t = _unitOfWork.CreateTransactionScope((IsolationLevel.ReadCommitted)))
            {
                var productModel = new ProductModel
                {
                    Name = request.Name,
                    Description = request.Description,
                    OriginalPrice = request.OriginalPrice,
                    Price = request.Price,
                    Stock = request.Stock,
                    CategoryId = request.CategoryId,
                    IsFeatured = request.IsFeatured,
                };
                await _unitOfWork.Products.AddProductAsync(productModel, user.FirstName + " " + user.LastName);
                //Save image
                if (request.ThumbnailImage != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ThumbnailImage.FileName.Replace(" ", ""));
                    await _blobService.UploadFileBlobAsync(fileName, request.ThumbnailImage, ContainerName);
                    var pImage = new ProductImageModel()
                    {
                        ImagePath = fileName,
                        SortOrder = 1,
                    };
                    await _unitOfWork.ProductImages.AddAsync(pImage, user.FirstName + " " + user.LastName);
                    productModel.ProductImages = new List<ProductImageModel>() { pImage };
                }

                await _unitOfWork.CompleteAsync();
                t.Complete();
                var listProductImage = new List<ProductImageViewModel>();
                if (productModel.ProductImages != null)
                {
                    listProductImage = productModel.ProductImages.Select(x => new ProductImageViewModel()
                    {
                        Id = x.Id,
                        ImagePath = x.ImagePath,
                        SortOrder = x.SortOrder,
                    }).ToList();

                }
                return new ProductViewModel()
                {
                    Name = productModel.Name,
                    OriginalPrice = productModel.OriginalPrice,
                    Stock = productModel.Stock,
                    ViewCount = 0,
                    Description = productModel.Description,
                    CategoryId = productModel.CategoryId,
                    IsFeatured = productModel.IsFeatured,
                    ListImage = listProductImage
                };
            }
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid productId)
        {
            using var t = _unitOfWork.CreateTransactionScope();
            {
                var product = _unitOfWork.Products.GetByIdAsync(productId).Result;
                if (product == null)
                {
                    return null;
                }

                var category = await _unitOfWork.Categories.GetByIdAsync(product.CategoryId);

                var productImage = _unitOfWork.ProductImages.Entities;

                List<ProductImage> images = await productImage.Where(x => x.ProductId == productId).ToListAsync();
                var productImages = new List<ProductImageViewModel>();
                foreach (var item in images)
                {
                    productImages.Add(new ProductImageViewModel()
                    {
                        Id = item.Id,
                        ImagePath = item.ImagePath,
                        SortOrder = item.SortOrder,
                        ProductId = item.ProductId,
                    });
                }


                var productViewModel = new ProductViewModel()
                {
                    Id = product.Id,
                    CreatedDate = product.CreatedDate,
                    Description = product != null ? product.Description : null,
                    Name = product != null ? product.Name : null,
                    OriginalPrice = product.OriginalPrice,
                    Price = product.Price,
                    Stock = product.Stock,
                    IsFeatured = product.IsFeatured,
                    ViewCount = product.ViewCount,
                    CategoryId = product.CategoryId,
                    CategoryName = category.Name,
                    ListImage = productImages
                };
                t.Complete();
                return productViewModel;
            }
        }


        public async Task<ApiResult<string>> UpdateAsync(ProductUpdateRequest request)
        {
            using (var t = _unitOfWork.CreateTransactionScope())
            {
                var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = await _userManager.FindByNameAsync(userName);
                var productModel = await _unitOfWork.Products.GetByIdAsync(request.Id);
                if (productModel == null) throw new Exception($"Cannot find a productModel with id: {request.Id}");
                productModel.Name = request.Name;
                productModel.Description = request.Description;
                productModel.IsFeatured = request.IsFeatured;

                await _unitOfWork.Products.UpdateProductAsync(productModel, user.FirstName + " " + user.LastName);
                await _unitOfWork.CompleteAsync();
                t.Complete();
                return new ApiSuccessResult<string>("Update product successful");
            }
        }




        //public async Task UpdatePriceAsync(Guid productId, float newPrice)
        //{
        //    if (await _unitOfWork.Products.UpdatePriceAsync(productId, newPrice))
        //    {
        //        await _unitOfWork.CompleteAsync();
        //    }
        //    else
        //    {
        //        throw new Exception($"Cannot find a product with id: {productId}");
        //    }

        //}

        //public async Task UpdateStockAsync(Guid productId, int addedQuantity)
        //{

        //    if (await _unitOfWork.Products.UpdateStockAsync(productId, addedQuantity))
        //    {
        //        await _unitOfWork.CompleteAsync();
        //    }
        //    else
        //    {
        //        throw new Exception($"Cannot find a product with id: {productId}");
        //    }
        //}
        //public async Task ChangeActiveAsync(Guid productId)
        //{
        //    if (await _unitOfWork.Products.ChangeActiveAsync(productId)) await _unitOfWork.CompleteAsync();
        //    else throw new Exception($"Cannot find a product with id: {productId}");
        //}

        public async Task<PagingResponse<ICollection<ProductViewModel>>> GetAllProductAsync(PagingRequestBase request)
        {
            try
            {
                var query = await _unitOfWork.Products.GetAllProductsAsync();
                var list = query.Select(x => new ProductViewModel()
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
                    ListImage = x.ProductImages.Select(i => new ProductImageViewModel()
                    {
                        Id = i.Id,
                        ImagePath = i.ImagePath,
                        SortOrder = i.SortOrder,
                        ProductId = i.ProductId,
                    }).ToList()
                }).Skip((request._page - 1) * request._limit).Take(request._limit).ToList().ToList();

                int TotalItem = query.Count;
                return new PagingResponse<ICollection<ProductViewModel>>()
                {
                    List = list,
                    TotalItem = TotalItem,
                    CurrentPage = request._page
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public async Task<ApiResult<string>> DeleteAsync(Guid productId)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(userName);
            await _unitOfWork.Products.DeleteProductAsync(productId, user.FirstName + " " + user.LastName);
            await _unitOfWork.CompleteAsync();
            return new ApiSuccessResult<string>("Deleted product successful");
        }



        public async Task<PagingResponse<ICollection<ProductViewModel>>> GetLatestProductsAsync(PagingRequestBase request)
        {
            var products = await _unitOfWork.Products.GetLatestProducts();
            var list = products.Select(x => new ProductViewModel()
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
                ListImage = x.ProductImages.Select(i => new ProductImageViewModel()
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    SortOrder = i.SortOrder,
                    ProductId = i.ProductId,
                }).ToList()
            }).Skip((request._page - 1) * request._limit).Take(request._limit).ToList().ToList();

            int TotalItem = products.Count();
            return new PagingResponse<ICollection<ProductViewModel>>()
            {
                List = list,
                TotalItem = TotalItem,
                CurrentPage = request._page
            };
        }

        public async Task<PagingResponse<ICollection<ProductViewModel>>> GetBestSellerProductsAsync(PagingRequestBase request)
        {
            var products = await _unitOfWork.Products.GetBestSellerProducts();
            var list = products.Select(x => new ProductViewModel()
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
                ListImage = x.ProductImages.Select(i => new ProductImageViewModel()
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    SortOrder = i.SortOrder,
                    ProductId = i.ProductId,
                }).ToList()
            }).Skip((request._page - 1) * request._limit).Take(request._limit).ToList().ToList();
            int TotalItem = products.Count();
            return new PagingResponse<ICollection<ProductViewModel>>()
            {
                List = list,
                TotalItem = TotalItem,
                CurrentPage = request._page
            };
        }

        public async Task<PagingResponse<ICollection<ProductViewModel>>> GetFeaturedProductsAsync(PagingRequestBase request)
        {
            var products = await _unitOfWork.Products.GetFeaturedProducts();
            var list = products.Select(x => new ProductViewModel()
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
                ListImage = x.ProductImages.Select(i => new ProductImageViewModel()
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    SortOrder = i.SortOrder,
                    ProductId = i.ProductId,
                }).ToList()
            }).Skip((request._page - 1) * request._limit).Take(request._limit).ToList().ToList();

            int TotalItem = products.Count();
            return new PagingResponse<ICollection<ProductViewModel>>()
            {
                List = list,
                TotalItem = TotalItem,
                CurrentPage = request._page
            };
        }
    }
}