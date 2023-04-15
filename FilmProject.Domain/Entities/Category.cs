using FilmProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Domain.Entities
{
    public class Category:BaseEntity
    {
   
        public string CategoryName { get; set; }

        public ICollection<MovieCategoryMap> MovieCategories { get; set; }
    }
}
