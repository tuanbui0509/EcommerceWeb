using EcommerceSolution.Data.EF;
using EcommerceSolution.InterfaceRepository.Interface;

namespace EcommerceSolution.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        EcommerceDBContext _context;

        public UserRepository(EcommerceDBContext context)
        {
            _context = context;
        }

    }
}
