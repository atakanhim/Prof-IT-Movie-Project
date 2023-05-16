using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Contracts.Role;
using FilmProject.Application.Contracts.UserRole;
using FilmProject.Presentation.Models;

namespace FilmProject.Presentation.Mappings
{
    public class PresentationMovieProfile : Profile
    {
        public PresentationMovieProfile()
        {
            CreateMap<MovieDto, MovieViewModel>()
               .ForMember(dest => dest.Ortalama,
                 opt => opt.MapFrom(src => src.MovieLikes.Count > 0
                                         ? Math.Round(src.MovieLikes.Average(l => l.Point), 1)
                                         : 0))
               .ForMember(dest => dest.MovieLanguage, opt => opt.ConvertUsing<LanguageCodeToNameConverter, string>())
              .ReverseMap();



            CreateMap<CommentViewModel, CommentDto>().ReverseMap();
            CreateMap<AdminCommentViewModel, AdminCommentDto>().ReverseMap();
            CreateMap<CategoryViewModel, CategoryDto>().ReverseMap();
            CreateMap<FavoriteViewModel, FavoriteDto>().ReverseMap();
            CreateMap<PostMovieViewModel, MovieDto>().ReverseMap();
            CreateMap<RoleViewModel, RoleDto>().ReverseMap();
            CreateMap<RegisterByAdminViewModel, RegisterModelDto>().ReverseMap();
            CreateMap<UpdateUserRolesDto, UpdateUserRolesViewModel>().ReverseMap();

            CreateMap<MovieLikeDto, MovieLikeViewModel>().ReverseMap();
            CreateMap<CommentLikeDto, CommentLikeViewModel>().ReverseMap();
            CreateMap<ApplicationUserDto, ApplicationUserViewModel>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToShortDateString()))
                .ReverseMap();
        }

    }
}
