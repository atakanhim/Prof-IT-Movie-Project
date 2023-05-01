using AutoMapper;
using FilmProject.Application.Contracts.Movie;
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
        private IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }


        public void Add(CommentDto commentDto)
        {
            // Veritabanında bu isimle bir film var mı kontrolü yapıldı.
            Comment comment = _mapper.Map<CommentDto, Comment>(commentDto);

            _commentRepository.Add(comment);

        }
        public void Update(CommentDto commentDto)
        {
            Comment comment = _mapper.Map<CommentDto, Comment>(commentDto);

            _commentRepository.Update(comment);
        }
        public async Task<IEnumerable<CommentDto>> GetAllAsync(Expression<Func<Comment, bool>>? filter = null)
        {

            var comments = await _commentRepository.GetListAsync();
            List<CommentDto> commentsDto = _mapper.Map<List<Comment>, List<CommentDto>>(comments);

            return commentsDto;

        }

        public async Task<CommentDto?> GetAsync(Expression<Func<Comment, bool>> filter)
        {
            var comment = await _commentRepository.GetAsync(filter);
            CommentDto commentsDto = _mapper.Map<Comment, CommentDto>(comment);

            return commentsDto;
        }

        public async Task<int> GetCountOfTotalComment()
        {
            return await _commentRepository.GetCountOfTotalComment();
        }

        public async Task<IEnumerable<CommentDto>> GetListWithAppUser(Expression<Func<Comment, bool>>? filter = null)
        {
            List<Comment> comment = await _commentRepository.GetListWithAppUser(filter);

            List<CommentDto> commentsDto = _mapper.Map<List<Comment>, List<CommentDto>>(comment);

            return commentsDto;


        }

        public async Task<IEnumerable<CommentDto>> GetListWithAppUserAndMovie(Expression<Func<Comment, bool>>? filter = null)
        {
            List<Comment> comment = await _commentRepository.GetListWithAppUserAndMovie(filter);
            List<CommentDto> commentsDto = _mapper.Map<List<Comment>, List<CommentDto>>(comment);
            return commentsDto;
        }
    }
}
