using System.Dynamic;
using System.Xml.Serialization;

namespace Cw2.Models
{
    [XmlRoot("studies")]
    public class Studies
    {
        [XmlElement(elementName: "name")]
        public string Nazwa { get; set; }
        [XmlElement(elementName: "mode")]
        public string Tryb { get; set; }
        [XmlAttribute("name")]
        public string ActiveStudies { get; set; }
        [XmlAttribute("numberOfStudents")]
        public string Number { get; set; }
        public Studies() { }
    }
}