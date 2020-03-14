using System.Xml.Serialization;

namespace Cw2.Models
{
    public class ActiveStudies
    {
        [XmlArray("activeStudies"), XmlArrayItem ("studies")]
        public Studies[] Studia { get; set; }
        public ActiveStudies() { }
    }
}