using Eventmi.Infrastructure.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventmi.Infrastructure.Data.Models
{
    [Comment("Събитие")]
    public class Event : IDeletable
    {
        [Key]
        [Comment("Идентификатор на събитието")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Comment("Име на събитието")]
        public string Name { get; set; } = null!;        

        [Required]
        [Comment("Начало на събитието")]
        public DateTime Start { get; set; }

        [Required]
        [Comment("Край на събитието")]
        public DateTime End { get; set; }

        [Required]
        [Comment("Активност на събитието")]
        public bool IsActive { get; set; } = true;

        [Comment("Дата на изтриване")]
        public DateTime? DeletedOn { get; set; }

        [Required]
        [Comment("Идентификатор на място на провеждане")]
        [ForeignKey(nameof(Place))]
        public int PlaceId { get; set; }


        [Required]
        [Comment("Място на провеждане")]
        public Address Place { get; set; } = null!;
        
    }
}
