using FilmProject.Application.Contracts.Movie;
using FilmProject.Domain.Entities;
using FilmProject.Domain.Enums;

namespace FilmProject.Presentation.Models
{
    public class MovieViewModel:BaseModel
    {
        public string MovieName { get; set; }
        public string MovieSummary { get; set; }
        public string DirectorName { get; set; }
        public MovieRatings RatingAge { get; set; }
        public DateTime PublishYear { get; set; }
        public string PhotoPath { get; set; }
        public bool IsHighLighted { get; set; } = false;
        public string ImdbUrl { get; set; }
        public string MovieLanguage { get; set; }
        public bool isDeleted { get; set; } = false;
        public float Ortalama { get; set; }

        public ICollection<MovieLikeViewModel> MovieLikes { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
        public ICollection<CategoryDto> MovieCategories { get; set; }
        public ICollection<FavoriteDto> WhoFavorited { get; set; }
    }
}
