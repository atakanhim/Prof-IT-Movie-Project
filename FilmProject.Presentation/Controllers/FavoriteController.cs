using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public FavoriteController(IFavoriteService favoriteService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
            _userManager = userManager;


        }
        [HttpGet]
        [Route("MyFavorites")]
        [Authorize]
        public async Task<IActionResult> GetMyFavroiteMovies()
        {
            try 
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                var favorites = await _favoriteService.GetMyFavoritesAsync(userId);
                IEnumerable<MovieViewModel> movieViewModel = _mapper.Map<IEnumerable<MovieDto>, IEnumerable<MovieViewModel>>(favorites);

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var favoritesJson = JsonConvert.SerializeObject(movieViewModel, settings);


                return Ok(favoritesJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        // Veya film detayı getirilirken dto içerisinde de tutulabilir

        [HttpGet]
        [Route("IsMyFavorite")]
        public async Task<IActionResult> IsMovieInFavoritesAsync(int movieId)
        {
            try
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (userId == null)
                {
                    return Ok(false);
                }
                var result = await _favoriteService.IsMovieInFavoritesAsync(userId, movieId);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("NumberOfFavorites")]
        public async Task<IActionResult> NumberOfFavoritesAsync(int movieId)
        {
            try
            {
                var count = await _favoriteService.NumberOfFavoritesAsync(movieId);
                var result = new { Count = count };
                return Json(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("ChangeFavorite")]
        [Authorize]
        public async Task<IActionResult> ChangeFavoriteAsync([FromBody] FavoriteViewModel favoriteViewModel)
        {
            try
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                favoriteViewModel.UserId = userId;
                FavoriteDto NewFavorite = _mapper.Map<FavoriteViewModel, FavoriteDto>(favoriteViewModel);
                await _favoriteService.ChangeFavoriteAsync(NewFavorite);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }
    }
}
