using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class MovieLikeDto:BaseDto
    {
        public int Point { get; set; }
        public int MovieId { get; set; }
        public MovieDto Movie { get; set; }

        public string userId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}
