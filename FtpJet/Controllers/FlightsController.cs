using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NodaTime;

namespace FtpJet.Controllers
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

    public class FlightsController : ApiController
    {
        // GET: api/Flights
        public IEnumerable<FlightDto> Get(string startDate)
        {
            var adelaideTz = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Australia/Adelaide");

            using (var db = new MyDbContext())
            {
                var q = from r in db.Routes
                        where r.Airport_SourceAirport.FaaIata == "ADL"
                        select new { r.Airport_DestinationAirport.Name, r.Airport_DestinationAirport.Tzdb, r.Airport_DestinationAirport.FaaIata };

                var results = q.ToList();

                foreach (var r in results)
                {
                    var destTz = DateTimeZoneProviders.Tzdb.GetZoneOrNull(r.Tzdb);

                    var start = adelaideTz.AtLeniently(LocalDateTime.FromDateTime(new DateTime(2015, 10, 3, 22, 0, 0)));
                    var finish = destTz.AtLeniently(LocalDateTime.FromDateTime(new DateTime(2015, 10, 4, 9, 30, 0)));

                    yield return new FlightDto() { Code = "QF0", Start = start, Finish = finish, Duration = (finish.ToInstant() - start.ToInstant()), Source = "Adelaide", Destination = r.Name };
                }
            }
        }
    }
}
