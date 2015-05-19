using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
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
        public IEnumerable<FlightDto> Get([ModelBinder(typeof(LocalDateModelBinder))] LocalDate startDate, int duration)
        {
            var adelaideTz = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Australia/Adelaide");

            using (var db = new MyDbContext())
            {
                var q = from r in db.Routes
                        where r.Airport_SourceAirport.FaaIata == "ADL"
                        select new
                        {
                            r.Airport_DestinationAirport.Name,
                            r.Airport_DestinationAirport.Tzdb,
                            r.Airport_DestinationAirport.FaaIata,
                            r.Id
                        };

                var results = q.ToList();

                int hours = 0;

                foreach (var r in results)
                {
                    var destTz = DateTimeZoneProviders.Tzdb.GetZoneOrNull(r.Tzdb);

                    var localStart = startDate.At(new LocalTime(0, 0)).PlusHours(hours++);
                    //var localFinish = localStart.PlusHours(4);

                    var zonedStart = adelaideTz.AtLeniently(localStart);
                    var inst = zonedStart.ToInstant().Plus(Duration.FromHours(duration));
                    var zonedFinish = inst.InZone(destTz);

                    yield return new FlightDto()
                    {
                        Code = r.Id.ToString(),
                        Start = zonedStart,
                        Finish = zonedFinish,
                        Duration = (zonedFinish.ToInstant() - zonedStart.ToInstant()),
                        Source = "Adelaide",
                        Destination = r.Name
                    };
                }
            }
        }
    }
}
