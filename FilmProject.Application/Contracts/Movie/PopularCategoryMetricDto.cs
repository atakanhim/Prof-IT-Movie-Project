using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class PopularCategoryMetricDto
    {
        public string CategoryName { get; set; }
        public int MovieCount { get; set; }
    }
}
