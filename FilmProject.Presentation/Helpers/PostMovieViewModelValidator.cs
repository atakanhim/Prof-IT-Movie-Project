using FilmProject.Presentation.Models;
using FluentValidation;

namespace FilmProject.Presentation.Helpers
{
    public class PostMovieViewModelValidator : AbstractValidator<PostMovieViewModel>
    {
        public PostMovieViewModelValidator() 
        {
            RuleFor(x => x.MovieName).NotEmpty().WithMessage("Film adı zorunludur.");
            RuleFor(x => x.MovieSummary).NotEmpty().WithMessage("Film özeti zorunludur.");
            RuleFor(x => x.DirectorName).NotEmpty().WithMessage("Yönetmen adı zorunludur.");
            RuleFor(x => x.RatingAge).NotNull().WithMessage("Yaş sınırı zorunludur.");
            RuleFor(x => x.PublishYear).NotNull().WithMessage("Yayın yılı zorunludur.");
            RuleFor(x => x.Photo).NotNull().WithMessage("Fotoğraf yüklemeniz gerekmektedir.");
            RuleFor(x => x.ImdbUrl).NotNull().WithMessage("Geçerli bir IMDB URL'si giriniz.");
            RuleFor(x => x.MovieLanguage).NotEmpty().WithMessage("Film dili zorunludur.");
            RuleFor(x => x.CategoriesId).NotEmpty().WithMessage("En az bir kategori seçmelisiniz.");

        }
    }
}
