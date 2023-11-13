using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("customer")]
    public class ExportCustmerDTO
    {
        [XmlAttribute("full-name")]
        public string Name {  get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars {  get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney {  get; set; }

        [XmlIgnore]
        public bool IsYoungDriver { get; set; }

    }
}
