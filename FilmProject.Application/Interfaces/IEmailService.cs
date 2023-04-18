using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface IEmailService
    {
        public Task SendNewMovieEmail(List<string> emails, Movie movie);
    }
}
