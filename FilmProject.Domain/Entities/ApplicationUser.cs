using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public ICollection<Favorite> Favorite { get; set; }
    }
}
