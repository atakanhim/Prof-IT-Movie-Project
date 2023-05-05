using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.UserRole
{
    public class ApplicationUserDto
    {
        public DateTime BirthDate { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
    }
}
