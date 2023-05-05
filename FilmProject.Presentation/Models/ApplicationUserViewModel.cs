namespace FilmProject.Presentation.Models
{
    public class ApplicationUserViewModel
    {
        public string BirthDate { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
    }
}
