using FilmProject.Application.Contracts.Movie;
using FilmProject.Domain.Entities;
using FilmProject.Domain.Entities.Common;

namespace FilmProject.Presentation.Models
{
    public class MovieLikeViewModel:BaseEntity
    {
        public int Point { get; set; }
        public string userId { get; set; }
        public int MovieId { get; set; }
    }
}
