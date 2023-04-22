using FilmProject.Application.Contracts.Movie;


namespace FilmProject.Presentation.Models
{
    public class RenderMoviesViewModel
    {
        public IEnumerable<MovieDto> Movies { get; set; }

        public List<float> OrtalamaList { get; set; }
    }
}
