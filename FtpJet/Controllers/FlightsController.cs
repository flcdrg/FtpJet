using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using FtpJet.Models;
using NodaTime;

namespace FtpJet.Controllers
{
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
                    var zonedStart = adelaideTz.AtLeniently(localStart);

                    // add duration to Instant
                    var inst = zonedStart.ToInstant().Plus(Duration.FromHours(duration));

                    // translate instant back to destination time zone
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
