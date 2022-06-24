using System.ComponentModel;
using System.Xml.Serialization;

namespace Template.Domain.Commands.Close
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.brf.com/labs_parceiros/CLOSE")]
    [XmlRoot(Namespace = "http://www.brf.com/labs_parceiros/CLOSE", IsNullable = false)]
    public partial class MT_CLOSE_LABS_PARCEIROS_REQUEST
    {
        [XmlElement(Namespace = "")]
        public string authToken { get; set; }
    }
}