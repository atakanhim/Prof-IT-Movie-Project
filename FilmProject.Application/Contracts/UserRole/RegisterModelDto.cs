using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FilmProject.Application.Contracts.UserRole
{
    public class RegisterModelDto
    {
        
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string Password { get; set; }
        




    }
}
