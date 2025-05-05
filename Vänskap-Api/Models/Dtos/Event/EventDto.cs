using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models.Dtos.Event
{
    public class EventDto
    {
        [Required]
        public required string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Title { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime? StartTime { get; set; } 
        public DateTime? EndTime { get; set; } 
        public string? Location { get; set; }
        public int? AgeRangeMin { get; set; }
        public int? AgeRangeMax { get; set; }
        public List<string>? Interests { get; set; }


    }
}
