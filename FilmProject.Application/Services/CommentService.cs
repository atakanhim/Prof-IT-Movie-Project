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

        public void Add(Comment comment)
        {
             _commentRepository.Add(comment);
        }
        public void Update(Comment comment)
        {
            _commentRepository.Update(comment);
        }
        public async Task<IEnumerable<Comment>> GetAllAsync(Expression<Func<Comment, bool>>? filter = null)
        {
            return await _commentRepository.GetListAsync();
        }

        public async Task<Comment?> GetAsync(Expression<Func<Comment, bool>> filter)
        {
            return await _commentRepository.GetAsync(filter);
        }

        public async Task<int> GetCountOfTotalComment()
        {
            return await _commentRepository.GetCountOfTotalComment();
        }

 
    }
}
