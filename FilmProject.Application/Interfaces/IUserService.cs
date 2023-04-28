
﻿using FilmProject.Application.Contracts.UserRole;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface IUserService
    {
        Task<int> GetUserCountAsync();

        public Task AddUserByAdmin(RegisterModelDto model);

    }
}
