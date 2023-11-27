using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmi.Infrastructure.Data.Models
{
    [Comment("Град")]
    public class Town
    {
        public Town()
        {
            Places = new HashSet<Address>();
        }

        [Key]
        [Comment("Идентификатор на град")]
        public int Id { get; set; }

        [Comment("Име на град")]
        public string Name { get; set; } = null!;

        ICollection<Address> Places { get; set; } = null!;
    }
}
