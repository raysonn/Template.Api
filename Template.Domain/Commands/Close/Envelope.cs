using System.ComponentModel;
using System.Xml.Serialization;

namespace Template.Domain.Commands.Close
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

        public Envelope(string token)
        {
            Body = new EnvelopeBody()
            {
                MT_CLOSE_LABS_PARCEIROS_REQUEST = new MT_CLOSE_LABS_PARCEIROS_REQUEST()
                {
                    authToken = token,
                }
            };
        }
    }
}