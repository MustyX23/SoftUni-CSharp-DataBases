using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.DataProcessor.ImportDto
{
    public class ImportTeamsDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(@"^[a-zA-Z0-9 .-]+$")]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Nationality { get; set; } = null!;

        [Required]
        public int Trophies { get; set; }

        public int[] Footballers { get; set; } = null!;

    }
}
