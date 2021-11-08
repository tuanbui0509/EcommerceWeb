using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceRepository.Interface;
using EcommerceSolution.ViewModels.Catalog.Orders;
using EcommerceSolution.ViewModels.Common;
using EcommerceSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
