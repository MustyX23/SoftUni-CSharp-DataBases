using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("supplier")]
    public class ExportLocalSuppliersDTO
    {
        [XmlAttribute("id")]
        public int Id {  get; set; }

        [XmlAttribute("name")]
        public string Name { get; set;}

        [XmlAttribute("parts-count")]
        public int PartsCount {  get; set; }

        //<supplier id = "4" name="Airgas, Inc." parts-count="2" />
    }
}
