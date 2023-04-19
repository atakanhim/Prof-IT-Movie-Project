using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync(Expression<Func<Movie, bool>>? filter = null)
        {
            return await _commentRepository.GetListAsync();
        }

        public async Task<int> GetCountOfTotalComment()
        {
            return await _commentRepository.GetCountOfTotalComment();
        }
    }
}
