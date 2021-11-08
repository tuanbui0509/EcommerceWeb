using EcommerceSolution.InterfaceRepository.Interface;
using EcommerceSolution.Data.EF;
using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceSolution.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EcommerceDBContext _context;
        public UnitOfWork(EcommerceDBContext context, IUserRepository userRepository, ICategoryRepository categoryRepository
            , IProductRepository productRepository, IProductImageRepository productImageRepository, IOrderDetailRepository orderDetailRepository
            , IOrderRepository orderRepository)
        {
            _context = context;
            Users = userRepository;
            Categories = categoryRepository;
            Products = productRepository;
            ProductImages = productImageRepository;
            OrderDetails = orderDetailRepository;
            Orders = orderRepository;
        }
        public IUserRepository Users { get; private set; }

        public IRoleRepository Roles { get; private set; }

        public IProductRepository Products { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public IProductImageRepository ProductImages { get; private set; }

        public IOrderDetailRepository OrderDetails { get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IDbContextTransaction CreateTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public TransactionScope CreateTransactionScope(IsolationLevel level = IsolationLevel.ReadUncommitted)
        {
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
            {
                IsolationLevel = level,
                Timeout = TimeSpan.FromMinutes(30)
            }, TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
