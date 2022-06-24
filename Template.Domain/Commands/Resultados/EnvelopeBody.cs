using System.ComponentModel;
using System.Xml.Serialization;

namespace Template.Domain.Commands.Resultados
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {
        [XmlElement(Namespace = "http://www.brf.com/labs_parceiros/RESULTADOS")]
        public MT_RESULTADOS_LABS_PARCEIROS_REQUEST MT_RESULTADOS_LABS_PARCEIROS_REQUEST { get; set; }
    }
}