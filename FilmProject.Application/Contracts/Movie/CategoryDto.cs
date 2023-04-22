using FilmProject.Domain.Entities;
using FilmProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class CategoryDto:BaseEntity
    {
        public string CategoryName { get; set; }
  
    }
}
