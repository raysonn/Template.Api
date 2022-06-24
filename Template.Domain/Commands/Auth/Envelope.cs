using System.ComponentModel;
using System.Xml.Serialization;

namespace Template.Domain.Commands.Auth
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Envelope
    {
        public object Header { get; set; }
        public EnvelopeBody Body { get; set; }

        public Envelope()
        {
            Body = new EnvelopeBody()
            {
                MT_AUTENTICACAO_LABS_PARCEIROS_REQUEST = new MT_AUTENTICACAO_LABS_PARCEIROS_REQUEST()
                {
                    limsDSName = "LabWare_QAS",
                    limsServiceName = "LW_WEB",
                    password = "BRF@2022",
                    username = "EXT_LAB_CBO"
                }
            };
        }
    }
}