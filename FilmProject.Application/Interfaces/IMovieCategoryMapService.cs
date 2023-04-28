using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface IMovieCategoryMapService
    {
        public Task AddMovieToCategory(int MovieId, ICollection<int> IdOfCategories);
    }
}
