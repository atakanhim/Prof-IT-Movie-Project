using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class FavoriteDto
    {
        public int MovieId { get; set; }
        public MovieDto Movie { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
