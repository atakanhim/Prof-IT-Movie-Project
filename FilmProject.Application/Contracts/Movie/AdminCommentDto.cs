using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class AdminCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsConfirm { get; set; }
        public string MovieName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }

    }
}
