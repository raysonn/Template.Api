using System.ComponentModel;
using System.Xml.Serialization;

namespace Template.Domain.ViewModels.Close
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Envelope
    {
        public object Header { get; set; }

        public EnvelopeBody Body { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {
        [XmlElement(Namespace = "labware_weblims_close")]
        public closeResponse closeResponse { get; set; }
    }
    
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "labware_weblims_close")]
    [XmlRoot(Namespace = "labware_weblims_close", IsNullable = false)]
    public partial class closeResponse
    {
        public bool @return { get; set; }

        [XmlAttribute()]
        public string xmlsoapenv { get; set; }
    }
}