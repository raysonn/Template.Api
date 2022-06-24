using System.ComponentModel;
using System.Xml.Serialization;

namespace Template.Domain.Commands.Close
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {
        [XmlElement(Namespace = "http://www.brf.com/labs_parceiros/CLOSE")]
        public MT_CLOSE_LABS_PARCEIROS_REQUEST MT_CLOSE_LABS_PARCEIROS_REQUEST { get; set; }
    }
}