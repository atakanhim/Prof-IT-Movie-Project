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
                case "fr-FR":
                    return "Fransızca";
                case "es-ES":
                    return "İspanyolca";
                case "de-DE":
                    return "Almanca";
                case "it-IT":
                    return "İtalyanca";
                case "pt-PT":
                    return "Portekizce";
                case "ar-SA":
                    return "Arapça";
                case "zh-CN":
                    return "Çince";
                case "ja-JP":
                    return "Japonca";
                case "ko-KR":
                    return "Korece";
                case "ru-RU":
                    return "Rusça";
                default:
                    return source;
            }
        }
    }
}
