using System.ComponentModel;
using System.Xml.Serialization;

namespace Template.Domain.Commands.Resultados
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Envelope
    {
        public object Header { get; set; }
        public EnvelopeBody Body { get; set; }

        public Envelope() { }

        public Envelope(AmostrasCommand command)
        {
            Body = new EnvelopeBody()
            {
                MT_RESULTADOS_LABS_PARCEIROS_REQUEST = new MT_RESULTADOS_LABS_PARCEIROS_REQUEST()
                {
                    authToken = command.AuthToken,
                    analysisDateC = command.AnalysisDateC,
                    analysisUserC = command.AnalysisUserC,
                    sampleNumberC = command.SampleNumberC,
                    analysisNameC = command.AnalysisNameC,
                    resultCodeC = command.ResultCodeC,
                    resultValueC = command.ResultValueC,
                }
            };
        }
    }
}