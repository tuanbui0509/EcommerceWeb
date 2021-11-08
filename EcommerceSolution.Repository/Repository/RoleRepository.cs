using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceRepository.Interface;
using EcommerceSolution.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.Repository.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public RoleRepository()
        {
        }
        public Task<List<RoleViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
