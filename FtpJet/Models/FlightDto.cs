using NodaTime;

namespace FtpJet.Models
{
    public class FlightDto
    {
        public string Code { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public ZonedDateTime Start { get; set; }
        public ZonedDateTime Finish { get; set; }
        public Duration Duration { get; set; }
    }
}