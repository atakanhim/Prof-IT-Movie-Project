using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FilmProject.Application.Interfaces;
using FilmProject.Infrastructure.Repository.Abstract;

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

        public async Task<bool> IsCommentLike(string userId, int CommentId)
        {
            return await _commentLikeRepository.IsCommentLikedAsync(userId, CommentId);
        }
    }
}
