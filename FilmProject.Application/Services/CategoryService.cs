using AutoMapper;
using FilmProject.Application.Contracts.Movie;
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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository ,IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public Task<List<CategoryDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void AddCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<CategoryDto, Category>(categoryDto);

            // validation kontrolleri yapılacak.

            // Veritabanında bu isimle bir kategori var mı kontrolü yapıldı.
            if (_categoryRepository.isExist(category.CategoryName))
            {
                throw new InvalidOperationException("This category is already exists.");
            }
            else 
            {
                _categoryRepository.Add(category);
            }
 
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            Category NewCategory = _mapper.Map<CategoryDto, Category>(categoryDto);
            // Bu id değerine sahip kategori kontrolü yapıldı.
            Category oldCategory = await _categoryRepository.GetAsync(x => x.Id == NewCategory.Id);

            bool exists = _categoryRepository.isExist(categoryDto.CategoryName);
            if (oldCategory == null || exists)
            {
                // Kategori bulunmuyorsa hata mesajı dönüldü
                throw new InvalidOperationException("There is no category with this id or there is a category with the same name.");
            }
            else 
            {
                // Kategori varsa ismi değiştirilip güncellendi. (Bu şekilde yapılarak created değeri korunmuş oldu.)
                oldCategory.CategoryName = NewCategory.CategoryName;
                _categoryRepository.Update(oldCategory);
            }
            
        }
    }
}
