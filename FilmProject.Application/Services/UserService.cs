
﻿using FilmProject.Application.Contracts.UserRole;
using FilmProject.Application.Interfaces;

using FilmProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }

        public async Task<int> GetUserCountAsync()
        {
            return await _userManager.Users.CountAsync();
        }


        public async Task AddUserByAdmin(RegisterModelDto model) 
        {
            try {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, BirthDate = model.BirthDate, EmailConfirmed = true };
                
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Kullanıcı otomatik olarak "admin" rolüne atanır
                    await _userManager.AddToRoleAsync(user, model.RoleId);
                }
                else
                {
                    throw new InvalidOperationException("Kullanıcı eklenirken bir hata meydana geldi.");
                }
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException(ex.Message);
            }
       

        }

    }
}
