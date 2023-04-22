using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class MovieCategoryMapDto
    {

        public int MovieId { get; set; }
        public MovieDto Movie { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
