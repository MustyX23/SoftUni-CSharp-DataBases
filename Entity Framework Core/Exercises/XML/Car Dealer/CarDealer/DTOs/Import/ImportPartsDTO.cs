using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Part")]
    public class ImportPartsDTO
    {
        [XmlElement("name")]
        public string Name {  get; set; }

        [XmlElement("price")]
        public decimal Price {  get; set; }

        [XmlElement("quantity")]
        public int Quantity {  get; set; }

        [XmlElement("supplierId")]
        public int? SupplierId {  get; set; }
        //<name>Unexposed bumper</name>
        //<price>1003.34</price>
        //<quantity>10</quantity>
        //<supplierId>12</supplierId>
    }
}
