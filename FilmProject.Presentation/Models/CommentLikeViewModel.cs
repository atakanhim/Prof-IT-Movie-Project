using FilmProject.Application.Contracts.Movie;
using FilmProject.Domain.Entities;

namespace FilmProject.Presentation.Models
{
    public class CommentLikeViewModel
    {
        public int CommentId { get; set; }
        public string userId { get; set; }

    }
}
