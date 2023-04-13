using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Domain.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public string userId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}
