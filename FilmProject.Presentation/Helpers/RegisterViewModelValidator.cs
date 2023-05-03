using FilmProject.Presentation.Models;
using FluentValidation;

namespace FilmProject.Presentation.Helpers
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterByAdminViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
                .Length(4, 50).WithMessage("Kullanıcı adı en az 4, en fazla 50 karakter olmalıdır.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Doğum tarihi zorunludur.");
                

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta zorunludur.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Rol zorunludur.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola zorunludur.")
                .MinimumLength(6).WithMessage("Parola en az 6 karakter uzunluğunda olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Parola ve parola tekrarı eşleşmiyor.");

        }

        
    }
}
