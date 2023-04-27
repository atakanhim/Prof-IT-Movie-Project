﻿using FilmProject.Application.Contracts.Movie;
using FilmProject.Domain.Enums;

namespace FilmProject.Presentation.Models
{
    public class PostMovieViewModel:BaseModel
    {
        public string MovieName { get; set; }
        public string MovieSummary { get; set; }
        public string DirectorName { get; set; }
        public MovieRatings RatingAge { get; set; }
        public DateTime PublishYear { get; set; }
        public int MoviePoint { get; set; } = 0;
        public int MoviePointCounter { get; set; } = 0;
        public IFormFile Photo{ get; set; }
        public string PhotoPath { get; set; }
        public bool IsHighLighted { get; set; } = false;
        public string ImdbUrl { get; set; }
        public string MovieLanguage { get; set; }
        
        public ICollection<int> CategoriesId { get; set; }
    }
}
