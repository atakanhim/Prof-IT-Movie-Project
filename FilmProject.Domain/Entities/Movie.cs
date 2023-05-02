using FilmProject.Domain.Entities.Common;
using FilmProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Domain.Entities
{
    public class Movie:BaseEntity
    {
        public string MovieName { get; set; }
        public string MovieSummary { get; set; }
        public string DirectorName { get; set; }
        public MovieRatings RatingAge { get; set; }
        public DateTime PublishYear { get; set; }
        public string PhotoPath { get; set; }
        public bool IsHighLighted { get; set; }
        public string ImdbUrl { get; set; }
        public string MovieLanguage { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<MovieLike> MovieLikes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<MovieCategoryMap> MovieCategories { get; set; }
        public ICollection<Favorite> WhoFavorited { get; set; }
    }
}
