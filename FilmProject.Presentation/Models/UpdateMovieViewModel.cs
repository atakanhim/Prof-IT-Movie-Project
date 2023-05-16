using FilmProject.Domain.Enums;

namespace FilmProject.Presentation.Models
{
    public class UpdateMovieViewModel: BaseModel
    {
        public string MovieName { get; set; }
        public string MovieSummary { get; set; }
        public string DirectorName { get; set; }
        public MovieRatings RatingAge { get; set; }
        public DateTime PublishYear { get; set; }
        public IFormFile? Photo { get; set; }
        public string PhotoPath { get; set; }
        public bool IsHighLighted { get; set; } = false;
        public bool isDeleted { get; set; } 
        public string ImdbUrl { get; set; }
        public string MovieLanguage { get; set; }

        public ICollection<int> CategoriesId { get; set; }
    }
}
