using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models.Dtos.Event
{
    public class EventDto
    {
        [Required]
        [StringLength(50)]
        public required string Title { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now.AddDays(1);
        public string? Location { get; set; }
        public int? AgeRangeMin { get; set; }
        public int? AgeRangeMax { get; set; }
        public List<string>? Interests { get; set; }


    }
}
