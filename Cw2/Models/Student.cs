using System;
using System.Xml.Serialization;

namespace Cw2.Models
{
    [XmlRoot("student")]
    public class Student
    {
        [XmlAttribute(attributeName: "indexNumber")]
        public string Indeks { get; set; }
        
        [XmlElement(elementName: "fname")]
        public string Imie { get; set; } 
        
        [XmlElement(elementName: "lname")]
        public string Nazwisko { get; set; }
        
        [XmlElement(elementName: "birthdate")]
        public string DataUrodz { get; set; }
     
        [XmlElement(elementName:"email")]
        public string Email { get; set; }
        
        [XmlElement(elementName: "mothersName")]
        public string ImieMatki { get; set; }
        
        [XmlElement(elementName: "fathersName")]
        public string ImieOjca { get; set; }
        
        public Studies studies { get; set; }
    
        public Student(){}
    }
}