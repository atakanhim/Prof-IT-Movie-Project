using AutoMapper;
using FilmProject.Application.Contracts.Role;
using FilmProject.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private IMapper _mapper;
        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public List<RoleDto> GetAllRoles()
        {
            var rolesList = _roleManager.Roles.ToList();
            List<RoleDto> roles = _mapper.Map<List<IdentityRole>, List<RoleDto>>(rolesList);
            return roles;
        }

        public async Task AddRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded) 
                {
                    throw new InvalidOperationException("Rol eklenirken bir hata meydana geldi.");
                }
            }
            else 
            {
                throw new InvalidOperationException("Bu rol zaten mevcut.");
            }
            
        }
    }
}
