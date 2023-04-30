using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class CommentLikeDto:BaseDto
    {
        public int CommentId { get; set; }
        public CommentDto Comment { get; set; }

        public string userId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}
