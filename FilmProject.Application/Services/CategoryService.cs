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

        public async Task<bool> AddCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<CategoryDto, Category>(categoryDto);

            // validation kontrolleri yapılacak.

            // Veritabanında bu isimle bir kategori var mı kontrolü yapıldı.
            if (await _categoryRepository.isExistAsync(category.CategoryName))
            {
                throw new InvalidOperationException("Bu kategori zaten mevcut.");
            }
            else 
            {
               return await _categoryRepository.AddAsync(category);
            }
 
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            Category NewCategory = _mapper.Map<CategoryDto, Category>(categoryDto);
            // Bu id değerine sahip kategori kontrolü yapıldı.
            Category oldCategory = await _categoryRepository.GetAsync(x => x.Id == NewCategory.Id);

            bool exists = await _categoryRepository.isExistAsync(categoryDto.CategoryName);
            if (oldCategory == null)
            {
                // Kategori bulunmuyorsa hata mesajı dönüldü
                throw new InvalidOperationException("Kategori Bulunamadı.");
            }
            else if (categoryDto.CategoryName == oldCategory.CategoryName)
            {
                throw new InvalidOperationException("Degisiklik Yapılmadı.");
            }
            else if (exists)
            {
                throw new InvalidOperationException("Aynı isimde category eklenemez.");
            }
            else 
            {
                // Kategori varsa ismi değiştirilip güncellendi. (Bu şekilde yapılarak created değeri korunmuş oldu.)
                oldCategory.CategoryName = NewCategory.CategoryName;
                _categoryRepository.Update(oldCategory);
            }
            
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(x=>x.Id==id);

            // validation kontrolleri yapılacak.

            // Veritabanında bu isimle bir kategori var mı kontrolü yapıldı.
            if (category==null)
            {
                throw new InvalidOperationException("Bu kategori mevcut degil.");
            }
            else
            {
               
                _categoryRepository.Delete(category);
            }
        }
    }
}
