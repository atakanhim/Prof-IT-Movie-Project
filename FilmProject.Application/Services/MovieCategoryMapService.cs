using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Services
{
    public class MovieCategoryMapService : IMovieCategoryMapService
    {
        private readonly IMovieCategoryMapRepository _repository;
        private readonly IMovieRepository _movieRepository;
        private readonly ICategoryRepository _categoryRepository;
        public MovieCategoryMapService(IMovieCategoryMapRepository repository, IMovieRepository movieRepository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _movieRepository = movieRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task AddMovieToCategory(int MovieId, ICollection<int> IdOfCategories)
        {
            var movie = _movieRepository.isExistById(MovieId);
            if (!movie)
            {
                throw new InvalidOperationException("There is no movie with this id.");
            }
            foreach (var category in IdOfCategories)
            {
                if (!_categoryRepository.isExistById(category))
                {
                    throw new InvalidOperationException("There is no category with this id : " + category);
                }
                else {
                    var NewMap = new MovieCategoryMap();
                    NewMap.MovieId = MovieId;
                    NewMap.CategoryId = category;
                    _repository.Add(NewMap);
                }
            }
            
        }
    }
}
