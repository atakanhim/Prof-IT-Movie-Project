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
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task<List<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void AddCategory(Category Category)
        {
            // validation kontrolleri yapılacak.

            // Veritabanında bu isimle bir kategori var mı kontrolü yapıldı.
            if (_categoryRepository.isExist(Category.CategoryName))
            {
                throw new InvalidOperationException("This category is already exists.");
            }
            else 
            {
                _categoryRepository.Add(Category);
            }
 
        }

        public async Task UpdateCategoryAsync(Category NewCategory)
        {
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
