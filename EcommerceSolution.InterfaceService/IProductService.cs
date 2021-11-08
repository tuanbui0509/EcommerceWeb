using EcommerceSolution.ViewModels.Catolog.Products;
using EcommerceSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.ViewModel.Common;

namespace EcommerceSolution.InterfaceService
{
    public interface IProductService
    {
        Task<ProductViewModel> CreateAsync(ProductCreateRequest request);
        Task UpdateAsync(ProductUpdateRequest request);
        Task<ProductViewModel> GetByIdAsync(Guid productId);
        Task DeleteAsync(Guid productId);

        Task<PagingResponse<ICollection<ProductViewModel>>> GetAllProductAsync(PagingRequestBase request);


        //product feature
        Task<PagingResponse<ICollection<ProductViewModel>>> GetFeaturedProductsAsync(PagingRequestBase request);

        Task<PagingResponse<ICollection<ProductViewModel>>> GetLatestProductsAsync(PagingRequestBase request);

        Task<PagingResponse<ICollection<ProductViewModel>>> GetBestSellerProductsAsync(PagingRequestBase request);
        #region Api Other
        //Task UpdatePriceAsync(Guid productId, float newPrice);

        //Task UpdateStockAsync(Guid productId, int addedQuantity);

        //Task ChangeActiveAsync(Guid productId);

        //Task AddViewCountAsync(Guid productId);
        //review
        //Task<ReviewViewModel> GetReviewByIdAsync(int reviewId);

        //Task<int> CreateReviewAsync(ReviewCreateRequest request);

        //Task<PagingResponseModel<ReviewViewModel>> GetAllReviewsAsync(ReviewsPagingRequest request);
        #endregion Api Other
    }
}
