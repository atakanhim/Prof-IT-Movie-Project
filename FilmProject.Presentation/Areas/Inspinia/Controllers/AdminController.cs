using AutoMapper;
using FilmProject.Application.Contracts.Role;
using FilmProject.Application.Contracts.UserRole;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Areas.Inspinia.Controllers
{
    [Area("Inspinia")]
    //[Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private IMapper _mapper;
        public AdminController(IRoleService roleService, IMapper mapper, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
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
                RegisterModelDto NewUser = _mapper.Map<RegisterByAdminViewModel, RegisterModelDto>(RegisterViewModel);
                await _userService.AddUserByAdmin(NewUser);
                return Ok();
            } catch (Exception ex)
            { 
                return BadRequest(ex);
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
    }
}
