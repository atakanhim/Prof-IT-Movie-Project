using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Presentation.Models;

namespace FilmProject.Presentation.Mappings
{
    public class PresentationMovieProfile : Profile
    {
        public PresentationMovieProfile() {
            CreateMap<MovieViewModel, MovieDto>().ReverseMap();
        }

    }
}
