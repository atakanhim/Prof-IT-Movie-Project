using FilmProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Domain.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string DirectorName { get; set; }
        public string MovieDescription { get; set; }
        public DateTime PublishYear { get; set; }
        public MovieRatings Rating { get; set; }
        public string PhotoPath { get; set; }
        public int CategoryId { get; set; }
        public Category MovieCategory { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
