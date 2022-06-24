using System.ComponentModel;
using System.Xml.Serialization;

namespace Template.Domain.Commands.Auth
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.brf.com/labs_parceiros/AUTENTICACAO")]
    [XmlRoot(Namespace = "http://www.brf.com/labs_parceiros/AUTENTICACAO", IsNullable = false)]
    public partial class MT_AUTENTICACAO_LABS_PARCEIROS_REQUEST
    {
        [XmlElement(Namespace = "")]
        public string username { get; set; }

        [XmlElement(Namespace = "")]
        public string password { get; set; }

        [XmlElement(Namespace = "")]
        public string limsDSName { get; set; }

        [XmlElement(Namespace = "")]
        public string limsServiceName { get; set; }
    }
}