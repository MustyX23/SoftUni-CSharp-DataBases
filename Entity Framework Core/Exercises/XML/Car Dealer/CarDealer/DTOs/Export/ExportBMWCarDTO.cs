using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("car")]
    public class ExportBMWCarDTO
    {
        [XmlAttribute("id")]
        public int CarId {  get; set; }

        [XmlAttribute("model")]
        public string Model {  get; set; }

        [XmlAttribute("traveled-distance")]
        public long TraveledDistance {  get; set; }
    }
}
