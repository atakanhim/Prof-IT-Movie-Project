﻿using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Abstract
{
    public interface IMovieRepository : IEntityRepository<Movie>
    {
        bool ChangeOneCikar(int id);
    }
}