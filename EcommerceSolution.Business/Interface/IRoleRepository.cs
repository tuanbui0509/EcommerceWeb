using EcommerceSolution.Data.Entities;
using EcommerceSolution.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.InterfaceRepository.Interface

{
    public interface IRoleRepository 
    {
        Task<List<RoleViewModel>> GetAll();
    }
}