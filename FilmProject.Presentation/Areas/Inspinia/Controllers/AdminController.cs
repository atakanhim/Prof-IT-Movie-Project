using AutoMapper;
using FilmProject.Application.Contracts.Role;
using FilmProject.Application.Contracts.UserRole;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Presentation.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FilmProject.Presentation.Areas.Inspinia.Controllers
{
    [Area("Inspinia")]
    //[Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IValidator<RegisterByAdminViewModel> _validator;
        private IMapper _mapper;
        public AdminController(IRoleService roleService, IMapper mapper, IUserService userService, IValidator<RegisterByAdminViewModel> validator)
        {
            _roleService = roleService;
            _userService = userService;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<IActionResult> AddUserForm()
        {


            return View();
        }

        [HttpPost]
        //[Authorize("Admin")]
        public async Task<IActionResult> AddUserByAdmin([FromBody] RegisterByAdminViewModel RegisterViewModel)
        {
            try
            {
                ValidationResult result = await _validator.ValidateAsync(RegisterViewModel);
                if (result.IsValid)
                {
                    RegisterModelDto NewUser = _mapper.Map<RegisterByAdminViewModel, RegisterModelDto>(RegisterViewModel);
                    await _userService.AddUserByAdmin(NewUser);
                    return Ok();
                }
                else
                {
                    Console.WriteLine(result.Errors.ToArray().ToString());
                    throw new InvalidOperationException(result.Errors.ToArray().ToString());
                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }

        public IActionResult GetRoles()
        {
            try
            {
                var RoleList = _roleService.GetAllRoles();
                List<RoleViewModel> roles = _mapper.Map<List<RoleDto>, List<RoleViewModel>>(RoleList);
                return Ok(RoleList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> GetUsers()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsersList()
        {
            try
            {
                var userListDto = await _userService.GetAllUsers();
                List<ApplicationUserViewModel> userViewModelList = _mapper.Map<List<ApplicationUserDto>, List<ApplicationUserViewModel>>(userListDto);
                return Ok(userViewModelList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet]
        public async Task<IActionResult> GetUserCountInAdminRole()
        {
            try
            {
                int count = await _userService.GetUserCountInRole("Admin");
                return Json(new { adminCount = count });
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesViewModel UpdateRoleInfo)
        {
            try
            {
                var updateRoles = _mapper.Map<UpdateUserRolesViewModel, UpdateUserRolesDto>(UpdateRoleInfo);
                await _userService.UpdateUserRolesAsync(updateRoles);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
