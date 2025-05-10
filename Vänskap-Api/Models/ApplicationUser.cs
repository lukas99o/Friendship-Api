using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Vänskap_Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Interest>? Interests { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
        public ICollection<EventParticipant> EventParticipations { get; set; } = new List<EventParticipant>();
        public ICollection<Friendship> Friendships { get; set; } = new List<Friendship>();
    }
}
