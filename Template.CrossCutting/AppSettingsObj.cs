namespace Uninter.Template.CrossCutting
{
    public class AppSettingsObj
    {
        public Api TokenApi { get; set; }
        public Api TemplateApi { get; set; }
        public Api LabWareApi { get; set; }
    }

    public class Api
    {
        public string Url { get; set; }
        public string UrlToken { get; set; }
    }

    public class Certificado
    {
        public string Path { get; set; }
        public string Password { get; set; }
    }
}