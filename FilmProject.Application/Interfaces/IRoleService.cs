using FilmProject.Application.Contracts.Role;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface IRoleService
    {
        List<RoleDto> GetAllRoles();
        public Task AddRole(string roleName);

    }
}
