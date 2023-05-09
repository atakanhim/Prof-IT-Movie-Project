using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Contracts.Role;
using FilmProject.Application.Contracts.UserRole;
using FilmProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.MovieCategories, opt => opt.MapFrom(src => src.MovieCategories.Select(x => x.Category)))
            .ForMember(dest => dest.WhoFavorited, opt => opt.MapFrom(src => src.WhoFavorited));

            CreateMap<MovieDto, Movie>();
            CreateMap<Comment, AdminCommentDto>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.MovieName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AppUser.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName));
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<MovieLike, MovieLikeDto>().ReverseMap();
            CreateMap<CommentLike, CommentLikeDto>().ReverseMap();
            CreateMap<Favorite, FavoriteDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<MovieCategoryMap, MovieCategoryMapDto>().ReverseMap();
            CreateMap<RoleDto, IdentityRole>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDto>()
            .ForMember(dest => dest.IsTwoFactorEnabled, opt => opt.MapFrom(src => src.TwoFactorEnabled));
            CreateMap<Category, PopularCategoryMetricDto>()
    .ForMember(dest => dest.MovieCount, opt => opt.MapFrom(src => src.MovieCategories.Count));

        }
    }
}
