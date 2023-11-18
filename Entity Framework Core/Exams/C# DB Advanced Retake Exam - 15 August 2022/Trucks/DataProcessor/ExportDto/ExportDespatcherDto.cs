using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Despatcher")]
    public class ExportDespatcherDto
    {
        [XmlElement("DespatcherName")]
        public string? Name { get; set; }

        [XmlAttribute("TrucksCount")]
        public int TrucksCount {  get; set; }

        [XmlArray("Trucks")]
        public ExportTruckDespatcherDto[] Trucks { get; set; } = null!;
    }
}
