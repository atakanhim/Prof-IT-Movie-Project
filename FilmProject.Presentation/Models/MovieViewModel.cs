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
        public int MoviePoint { get; set; }
        public int MoviePointCounter { get; set; } // bir bir arttırıcaz 
        public string PhotoPath { get; set; }
        public bool IsHighLighted { get; set; }
        public string ImdbUrl { get; set; }
        public string MovieLanguage { get; set; }
        public float Ortalama { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public ICollection<CategoryDto> MovieCategories { get; set; }
        public ICollection<FavoriteDto> WhoFavorited { get; set; }
    }
}
