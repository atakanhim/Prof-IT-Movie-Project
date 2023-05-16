using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.UserRole
{
    public class UpdateUserRolesDto
    {
        public string userId { get; set; }
        public List<string> roles { get; set; }
    }
}
