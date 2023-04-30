using FilmProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Domain.Entities
{
    public class CommentLike:BaseEntity
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public string userId { get; set; }
        public ApplicationUser AppUser { get; set; }
    }
}
