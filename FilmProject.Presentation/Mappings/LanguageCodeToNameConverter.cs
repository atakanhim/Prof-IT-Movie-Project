using AutoMapper;

namespace FilmProject.Presentation.Mappings
{
    public class LanguageCodeToNameConverter : IValueConverter<string, string>
    {
        public string Convert(string source, ResolutionContext context)
        {
            switch (source)
            {
                case "tr-TR":
                    return "Türkçe";
                case "en-US":
                    return "İngilizce";
                default:
                    return source;
            }
        }
    }
}
