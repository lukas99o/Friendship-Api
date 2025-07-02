namespace Vänskap_Api.Models.Dtos.Event
{
    public class ReadAllPublicEventsDto
    {
        public List<string?>? Interests { get; set; }
        public int? AgeMax { get; set; }
        public int? AgeMin { get; set; }
    }
}
