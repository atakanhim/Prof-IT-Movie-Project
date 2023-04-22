using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Domain.Entities;
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

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Favorite, FavoriteDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<MovieCategoryMap, MovieCategoryMapDto>().ReverseMap();
        }
    }
}
