using Footballers.Data.Models.Enums;
using Footballers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Footballer")]
    public class ImportFootballerDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; } = null!;

        [XmlElement("ContractStartDate")]
        [Required]
        public string? ContractStartDate { get; set; }

        [XmlElement("ContractEndDate")]
        [Required]
        public string? ContractEndDate { get; set; }

        [XmlElement("BestSkillType")]
        [Required]
        public int BestSkillType { get; set; }

        [XmlElement("PositionType")]
        [Required]
        public int PositionType { get; set; }

    }
}
