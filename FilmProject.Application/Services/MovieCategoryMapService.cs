using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                throw new InvalidOperationException("Bu film bulunamadı");
            }
            foreach (var category in IdOfCategories)
            {
                if (!_categoryRepository.isExistById(category))
                {
                    throw new InvalidOperationException("Bu id de bir film bulunamadı : " + category);
                }
                else {
                    var NewMap = new MovieCategoryMap();
                    NewMap.MovieId = MovieId;
                    NewMap.CategoryId = category;
                    _repository.Add(NewMap);
                }
            }
            
        }
        public async Task UpdateMovieToCategoryAsync(int MovieId, ICollection<int> IdOfCategories)
        {
            var movie = _movieRepository.isExistById(MovieId);
            if (!movie)
            {
                throw new InvalidOperationException("Bu film bulunamadı");
            }
            else
            {

                foreach (var category in IdOfCategories)
                {
                    if (!_categoryRepository.isExistById(category))
                    {
                        throw new InvalidOperationException("Bu id de bir category bulunamadı : " + category);
                    }
                    else
                    {
                        var result = await _repository.isExistCategoryMap(MovieId, category);
                        if (result == false)
                        {
                            var NewMap = new MovieCategoryMap();
                            NewMap.MovieId = MovieId;
                            NewMap.CategoryId = category;
                            _repository.Add(NewMap);
                        }

                    }
                }

                // verileri ekledigimiz zaman bu film id si ile bu categori id ler hariç hepsi silinecek
               
              
            }
             int[] categories = IdOfCategories.ToArray();
             await _repository.DeleteArray(MovieId,categories);

        }
        public async Task<bool?> AnyMoviesInThisCategory(Expression<Func<MovieCategoryMap, bool>> filter)
        {
            var model = await _repository.GetListAsync(filter);
            if(model == null || model.Count==0)
                return false;

            return true;
        }

        public async Task<int[]> GetCategoriesWithMovieIdAsync(int id)
        {
            return await _repository.GetCategoriesWithMovieIdAsync(id);
        }
    }
}
