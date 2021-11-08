using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.Data.Model;

namespace EcommerceSolution.InterfaceRepository.Interface
{
    public interface IProductImageRepository
    {
        IQueryable<ProductImage> Entities { get; }
        Task<ICollection<ProductImageModel>> GetAllByProductIdAsync(Guid productId);
        Task<ProductImageModel> GetByIdAsync(Guid id);
        Task AddAsync(ProductImageModel productImageModel, string userName);
        Task UpdateAsync(ProductImageModel productImageModel, string userName);
        Task DeleteAsync(Guid id, string userName);
    }
}
