using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class SignalNewCommentDto
    {
        public string UserName { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public int LikeCount { get; set; }
    }
}
