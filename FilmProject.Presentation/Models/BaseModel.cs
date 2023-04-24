namespace FilmProject.Presentation.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
