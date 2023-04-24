using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Presentation.Models;

namespace FilmProject.Presentation.Mappings
{
    public class PresentationMovieProfile : Profile
    {
        public PresentationMovieProfile() {
            CreateMap<MovieViewModel, MovieDto>().ReverseMap().ForMember(dest => dest.Ortalama, opt => opt.MapFrom(src => src.MoviePointCounter == 0 ? 1 : Math.Round((float)src.MoviePoint / src.MoviePointCounter, 1)));
            CreateMap<CommentViewModel, CommentDto>().ReverseMap();
            CreateMap<CategoryViewModel, CategoryDto>().ReverseMap();
            CreateMap<FavoriteViewModel, FavoriteDto>().ReverseMap();
        }

    }
}
