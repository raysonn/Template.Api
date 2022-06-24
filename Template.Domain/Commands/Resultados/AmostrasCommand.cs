namespace Template.Domain.Commands.Resultados
{
    public partial class AmostrasCommand
    {
        public string AuthToken { get; set; }
        public string AnalysisDateC { get; set; }
        public string AnalysisUserC { get; set; }
        public string SampleNumberC { get; set; }
        public string AnalysisNameC { get; set; }
        public string ResultCodeC { get; set; }
        public string ResultValueC { get; set; }

        public AmostrasCommand(Amostras.AmostrasCommand command, string authToken)
        {
            AuthToken = authToken;
            AnalysisDateC = command.AnalysisDateC;
            AnalysisUserC = command.AnalysisUserC;
            SampleNumberC = command.SampleNumberC;
            AnalysisNameC = command.AnalysisNameC;
            ResultCodeC = command.ResultCodeC;
            ResultValueC = command.ResultValueC;
        }
    }
}