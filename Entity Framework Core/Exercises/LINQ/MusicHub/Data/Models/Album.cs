using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models
{
    public class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate {  get; set; }

        public decimal Price {  get => Songs.Sum(sp => sp.Price); }

        //•	Price – calculated property(the sum of all song prices in the album)

        public int? ProducerId {  get; set; }
        
        public Producer Producer { get; set; }

        public ICollection<Song> Songs { get; set; }

    }
}