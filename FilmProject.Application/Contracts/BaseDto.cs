using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts
{
    public class BaseDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } 
    }
}
