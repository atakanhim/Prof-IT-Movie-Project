﻿namespace FilmProject.Presentation.Models
{
    public class CategoryViewModel:BaseModel
    {
        public string CategoryName { get; set; }
        public bool isDeleted { get; set; } = false;

    }
}
