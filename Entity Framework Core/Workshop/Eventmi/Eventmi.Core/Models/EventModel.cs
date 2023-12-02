using Eventmi.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Eventmi.Core.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Име на събитието")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = UserMessageConstants.StringLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Начало на събитието")]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Край на събитието")]
        public DateTime End { get; set; }

        [Required(ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Място на провеждане")]
        public string StreetAddress { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Град")]
        public int TownId {  get; set; }
    }
}
