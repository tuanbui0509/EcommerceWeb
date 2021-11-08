using EcommerceSolution.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceSolution.InterfaceRepository.Interface
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IProductRepository Products { get; }
        IProductImageRepository ProductImages { get; }
        IOrderRepository Orders { get; }
        IOrderDetailRepository OrderDetails { get; }
        ICategoryRepository Categories { get; }

        TransactionScope CreateTransactionScope(IsolationLevel level = IsolationLevel.ReadUncommitted);
        IDbContextTransaction CreateTransaction();
        Task CompleteAsync();
        void Dispose();
    }
}
