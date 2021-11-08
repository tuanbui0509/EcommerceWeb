using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.ViewModels.System.Roles;

namespace EcommerceSolution.InterfaceService
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAll();

    }
}
