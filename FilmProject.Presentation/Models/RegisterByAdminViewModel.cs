using FilmProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FilmProject.Presentation.Models
{
    public class RegisterByAdminViewModel
    {
        
        public string UserName { get; set; }

        
        public DateTime BirthDate { get; set; }

        
        public string Email { get; set; }

        
        public string RoleId { get; set; }

       
        public string Password { get; set; }

        
       
        public string ConfirmPassword { get; set; }

       
    }
}
