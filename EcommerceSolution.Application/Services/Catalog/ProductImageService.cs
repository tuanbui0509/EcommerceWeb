using EcommerceSolution.InterfaceService;
using EcommerceSolution.ViewModels.Catalog.ProductImage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EcommerceSolution.Application.Common;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.Data.Model;
using EcommerceSolution.InterfaceRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EcommerceSolution.Application.Services.Catalog
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobService _blobService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string ContainerName = "images";
        public ProductImageService(IUnitOfWork unitOfWork, IBlobService blobService
        , IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _blobService = blobService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ProductImageViewModel> AddImageAsync(ProductImageCreateRequest request, string userName)
        {
            using (var t = _unitOfWork.CreateTransaction())
            {
                var productImage = new ProductImageModel()
                {
                    SortOrder = request.SortOrder,
                    ProductId = request.ProductId,
                };
                //Save image
                if (request.ImageFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ImageFile.FileName.Replace(" ", ""));
                    await _blobService.UploadFileBlobAsync(fileName, request.ImageFile, ContainerName);
                    productImage.ImagePath = fileName;
                }
                await _unitOfWork.ProductImages.AddAsync(productImage, userName);
                await _unitOfWork.CompleteAsync();
                t.Commit();
                return new ProductImageViewModel()
                {
                    Id = productImage.Id,
                    ImagePath = productImage.ImagePath,
                    SortOrder = productImage.SortOrder,
                    ProductId = productImage.ProductId
                };
            }
        }
        public async Task DeleteImageAsync(Guid imageId, string userName)
        {
            var image = await _unitOfWork.ProductImages.GetByIdAsync(imageId);
            await _blobService.DeleteBlobAsync(image.ImagePath, "images");
            await _unitOfWork.ProductImages.DeleteAsync(imageId, userName);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ProductImageViewModel> GetImageByIdAsync(Guid imageId)
        {
            var image = await _unitOfWork.ProductImages.GetByIdAsync(imageId);
            var viewModel = new ProductImageViewModel()
            {
                Id = image.Id,
                ImagePath = image.ImagePath,
                SortOrder = image.SortOrder,
                ProductId = image.ProductId,
            };
            return viewModel;
        }

        public ICollection<ProductImageViewModel> GetListImagesAsync(Guid productId)
        {
            return _unitOfWork.ProductImages.GetAllByProductIdAsync(productId).Result.Select(i => new ProductImageViewModel()
            {
                Id = i.Id,
                ImagePath = i.ImagePath,
                SortOrder = i.SortOrder,
                ProductId = i.ProductId,
            }).ToList();
        }

        public async Task UpdateImageAsync(ProductImageUpdateRequest request, string userName)
        {
            using (var t = _unitOfWork.CreateTransaction())
            {
                var productImage = await _unitOfWork.ProductImages.GetByIdAsync(request.Id);
                if (productImage == null) throw new Exception($"Cannot find a productImage with id: {request.Id}");
                //Save image
                if (request.ImageFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ImageFile.FileName.Replace(" ", ""));
                    await _blobService.UploadFileBlobAsync(fileName, request.ImageFile, ContainerName);
                    productImage.ImagePath = fileName;
                }

                productImage.SortOrder = request.SortOrder;
                await _unitOfWork.ProductImages.UpdateAsync(productImage, userName);
                await _unitOfWork.CompleteAsync();
                t.Commit();
            }
        }
    }
}
