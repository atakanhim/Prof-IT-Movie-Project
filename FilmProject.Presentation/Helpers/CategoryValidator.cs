using FilmProject.Presentation.Models;
using FluentValidation;

namespace FilmProject.Presentation.Helpers
{
    public class CategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category Name boş bırakılamaz");
        }
    }
}
