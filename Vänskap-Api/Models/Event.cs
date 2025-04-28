using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vänskap_Api.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public required string Location { get; set; }
        public int AgeRangeMin { get; set; }
        public int AgeRangeMax { get; set; }
        public ICollection<Interest>? Interests { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey("CreatedByUser")]
        public required string CreatedByUserId { get; set; }
        public required ApplicationUser CreatedByUser { get; set; }

        [Required]
        public required ICollection<EventParticipant> EventParticipants { get; set; }  
    }
}
