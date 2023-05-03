using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using FilmProject.Infrastructure.Repository.Concrete;

namespace FilmProject.Application.Services
{
    public class CommentLikeService: ICommentLikeService
    {
        private readonly ICommentLikeRepository _commentLikeRepository;
        private IMapper _mapper;

        public CommentLikeService(ICommentLikeRepository commentLikeRepository, IMapper mapper)
        {
            _commentLikeRepository = commentLikeRepository;
            _mapper = mapper;
        }

        public async Task ChangeCommentLikeStatue(CommentLikeDto commentLike)
        {
            CommentLike newCommentLike = _mapper.Map<CommentLikeDto,CommentLike>(commentLike);
            await _commentLikeRepository.ChangeCommentLikeStatueAsync(newCommentLike);
        }

        public async Task<CommentLikeDto> GetAsync(Expression<Func<CommentLike, bool>> filter)
        {
            var model = await _commentLikeRepository.GetAsync(filter);
            CommentLikeDto newCommentLike = _mapper.Map< CommentLike, CommentLikeDto>(model);
            return newCommentLike;
        }

        public async Task<bool> IsCommentLike(string userId, int CommentId)
        {
            var model = await _commentLikeRepository.IsCommentLikedAsync(userId, CommentId);
            if(model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public async Task<int> NumberofCommentLike(int CommentId)
        {
            return await _commentLikeRepository.NumberOfCommentLikeAsync(CommentId);
        }
    }
}
