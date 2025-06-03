using System.ComponentModel.DataAnnotations;

namespace VictorMovies.Components.Domain
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [MaxLength(100)]
        public string MovieName { get; set; } = String.Empty;
        public int YearReleased { get; set; }
        [MaxLength(10)]
        public string AgeRestriction { get; set; } = string.Empty;
        public int Rating { get; set; } = 0;
        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
