using FilmProject.Domain.Entities;

namespace FilmProject.Presentation.Models
{
    public class AdminCommentViewModel:BaseModel
    {
        public string Content { get; set; }
        public bool IsConfirm { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public string userId { get; set; }
        public ApplicationUser AppUser { get; set; }

        public ICollection<CommentLikeViewModel> CommentLikes { get; set; }
    }
}
