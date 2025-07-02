namespace Vänskap_Api.Models
{
    public class EventInterest
    {
        public int EventId { get; set; }
        public Event? Event { get; set; }

        public int InterestId { get; set; }
        public Interest? Interest { get; set; }
    }
}
