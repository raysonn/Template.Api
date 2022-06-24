using System.ComponentModel;
using System.Xml.Serialization;

namespace Template.Domain.Commands.Resultados
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.brf.com/labs_parceiros/RESULTADOS")]
    [XmlRoot(Namespace = "http://www.brf.com/labs_parceiros/RESULTADOS", IsNullable = false)]
    public partial class MT_RESULTADOS_LABS_PARCEIROS_REQUEST
    {
        [XmlElement(Namespace = "")]
        public string authToken { get; set; }

        [XmlElement(Namespace = "")]
        public string analysisDateC { get; set; }

        [XmlElement(Namespace = "")]
        public string analysisUserC { get; set; }

        [XmlElement(Namespace = "")]
        public string sampleNumberC { get; set; }

        [XmlElement(Namespace = "")]
        public string analysisNameC { get; set; }

        [XmlElement(Namespace = "")]
        public string resultCodeC { get; set; }

        [XmlElement(Namespace = "")]
        public string resultValueC { get; set; }
    }
}