using FilmProject.Domain.Entities;

namespace FilmProject.Presentation.Models
{
    public class RenderMoviesViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }

        public List<float> OrtalamaList { get; set; }
    }
}
