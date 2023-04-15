using FilmProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Domain.Entities
{
    public class Comment:BaseEntity
    {
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public bool IsConfirm { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public string userId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}
