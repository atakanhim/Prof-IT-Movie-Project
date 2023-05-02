using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private IMapper _mapper;


        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
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
                return await _categoryRepository.AddAsync(category);
            
            

        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
           
            Category NewCategory = _mapper.Map<CategoryDto, Category>(categoryDto);

            // Bu id değerine sahip kategori kontrolü yapıldı.
            Category oldCategory = await _categoryRepository.GetAsync(x => x.Id == NewCategory.Id && x.isDeleted==false);

            // kategoriler sadece false ise gez


            bool exists = await _categoryRepository.isExistAsync(categoryDto.CategoryName);
            if (oldCategory == null)
            {
                throw new InvalidOperationException("Kategori Bulunamadı.");
            }
            else if (categoryDto.CategoryName == oldCategory.CategoryName)
            {
                // aynı isim varsa hic error dondurmuyoruz ama update de yapmamıza gereken yok
            }
            else if (exists)
            {          
               throw new InvalidOperationException("Bu isimde category var.");
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
            var category = await _categoryRepository.GetAsync(x => x.Id == id);

            // validation kontrolleri yapılacak.

            // Veritabanında bu isimle bir kategori var mı kontrolü yapıldı.
            if (category == null)
            {
                throw new InvalidOperationException("Bu kategori mevcut degil.");
            }
            else
            {
                if (!category.isDeleted)
                {
                    category.isDeleted = true;
                    _categoryRepository.Update(category);
                }

            }
        }

        public async Task<int> CountAsync()
        {
            return await _categoryRepository.CountAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(Expression<Func<Category, bool>>? filter = null)
        {
            var categories = await _categoryRepository.GetListAsync(filter);
            List<CategoryDto> categoryDto = _mapper.Map<List<Category>, List<CategoryDto>>(categories);

            return categoryDto;
        }
    }
}
