using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cw2.Models
{
    [XmlRoot("uczelnia")]
    public class Uczelnia
    {
        [XmlAttribute(attributeName: "createdAt")]
        public string CreatedAt { get; set; }
        [XmlAttribute(attributeName: "author")]
        public string Author { get; set; }
        public List<Student> Students { get; set; }
        public Uczelnia() {}
    }
}