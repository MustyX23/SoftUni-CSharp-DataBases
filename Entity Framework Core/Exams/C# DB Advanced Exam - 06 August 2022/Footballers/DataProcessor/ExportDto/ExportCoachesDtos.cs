using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ExportDto
{
    [XmlType("Coach")]
    public class ExportCoachesDtos
    {
        [XmlAttribute("FootballersCount")]
        public int FootballersCount {  get; set; }

        [XmlElement("CoachName")]
        public string CoachName {  get; set; } = null!;

        [XmlArray("Footballers")]
        public ExportFootballerDto[] Footballers { get; set;} = null!;
    }
}
