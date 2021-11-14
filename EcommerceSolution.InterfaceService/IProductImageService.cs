using EcommerceSolution.ViewModels.Catalog.ProductImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.InterfaceService
{
    public interface IProductImageService
    {
        Task<ProductImageViewModel> AddImageAsync(ProductImageCreateRequest request, string userName);

        Task UpdateImageAsync(ProductImageUpdateRequest request, string userName);
        Task DeleteImageAsync(Guid imageId, string userName);
        Task<ProductImageViewModel> GetImageByIdAsync(Guid imageId);
        ICollection<ProductImageViewModel> GetListImagesAsync(Guid productId);
    }
}
