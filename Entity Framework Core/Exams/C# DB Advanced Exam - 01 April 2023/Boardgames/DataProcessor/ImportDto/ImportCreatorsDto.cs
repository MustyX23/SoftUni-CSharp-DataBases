﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Creator")]
    public class ImportCreatorsDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(7)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(2)]
        [MaxLength(7)]
        public string LastName { get; set; } = null!;

        public ImportBoardGameDto[] Boardgames { get; set; } = null!;
    }
}
