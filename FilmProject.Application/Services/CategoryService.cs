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
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetListAsync();
            List<CategoryDto> categoryDto = _mapper.Map<List<Category>, List<CategoryDto>>(categories);

            return categoryDto;
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
            if (oldCategory == null)
            {
                // Kategori bulunmuyorsa hata mesajı dönüldü
                throw new InvalidOperationException("There is no category with this id");
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
