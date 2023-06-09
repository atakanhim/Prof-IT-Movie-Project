﻿using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class CommentRepository : EntityRepository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> GetCountOfTotalComment()
        {
            return await _context.Comments.CountAsync();
        }
        public async Task<List<Comment>>  GetListWithAppUser(Expression<Func<Comment, bool>>? filter = null)
        {
             return filter == null ? await _context.Comments.Include(y => y.AppUser).ToListAsync() :
                                   await _context.Comments.Include(y => y.AppUser).Where(filter).ToListAsync();
        
        }

        public async Task<List<Comment>> GetListWithAppUserAndMovie(Expression<Func<Comment, bool>>? filter = null)// Admin comment listeleme
        {
            return filter == null ? await _context.Comments.Include(y => y.AppUser).Include(x => x.Movie).ToListAsync() :
                               await _context.Comments.Include(y => y.AppUser).Include(x => x.Movie).Where(filter).ToListAsync(); ;
        
        }
    }
}
