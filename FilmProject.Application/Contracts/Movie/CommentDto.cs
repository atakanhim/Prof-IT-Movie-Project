using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class CommentDto:BaseDto
    {
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public bool IsConfirm { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int MovieId { get; set; }
        public MovieDto Movie { get; set; }

        public string userId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}
