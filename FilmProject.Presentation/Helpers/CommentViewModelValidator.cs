using FilmProject.Presentation.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Data;

namespace FilmProject.Presentation.Helpers
{
    public class CommentViewModelValidator: AbstractValidator<CommentCreateViewModel>
    {  public CommentViewModelValidator() 
        
        {

            RuleFor(x => x.Content).NotEmpty().WithMessage("Yorum Boş Bırakılamaz");
            
                
        }   
       

    }
}
