using Medicines.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Medicine")]
    public class ImportMedicineDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [XmlElement("Price")]
        [Required]
        [Range(0.01, 1000.00)]
        public decimal Price { get; set; }

        [XmlElement("ProductionDate")]
        [Required]
        public string ProductionDate { get; set; } = null!;

        [XmlElement("ExpiryDate")]
        [Required]
        public string ExpiryDate { get; set; } = null!;

        [XmlAttribute("category")]
        [Required]
        public int Category { get; set; }

        [XmlElement("Producer")]
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Producer { get; set; } = null!;
    }
}
