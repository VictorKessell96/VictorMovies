using System.ComponentModel.DataAnnotations;

namespace VictorMovies.Components.Domain
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [MaxLength(50)]
        public string GenreName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
