using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.Data.Model;

namespace EcommerceSolution.InterfaceRepository.Interface
{
    public interface IProductRepository
    {
        IQueryable<Product> Entities { get; }
        Task AddProductAsync(ProductModel product, string userName);
        Task UpdateProductAsync(ProductModel product, string userName);
        Task<ProductModel> GetByIdAsync(Guid productId);
        Task DeleteProductAsync(Guid productId, string userName);
        Task<ICollection<ProductModel>> GetAllProductsAsync();
        Task<ICollection<ProductModel>> GetFeaturedProducts();

        Task<ICollection<ProductModel>> GetLatestProducts();

        Task<ICollection<ProductModel>> GetBestSellerProducts();
        Task<bool> UpdateOrderQuantity(Guid productId, int addedQuantity, string userName);
        //Task<bool> UpdateStockAsync(Guid productId, int addedQuantity);
        //Task<bool> UpdatePriceAsync(Guid productId, float newPrice);
        //Task<bool> ChangeActiveAsync(Guid productId);

        //review
        //Task<int> CreateReviewAsync(ReviewCreateRequest request);

        //Task<PagingResponseModel<ReviewViewModel>> GetAllReviewsAsync(ReviewsPagingRequest request);

    }
}