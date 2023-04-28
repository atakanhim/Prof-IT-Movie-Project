﻿using FilmProject.Application.Contracts.Movie;
using FilmProject.Domain.Entities;

namespace FilmProject.Presentation.Models
{
    public class CommentViewModel:BaseModel
    {
        public string Content { get; set; }
        public int LikeCount { get; set; } = 0;
        public bool IsConfirm { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int MovieId { get; set; }
     
        public string userId { get; set; }

    }
}
