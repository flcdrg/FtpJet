using System;
using System.Collections.Generic;
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
        public IEnumerable<FlightDto> Get([ModelBinder(typeof(LocalDateModelBinder))] LocalDate startDate)
        {
            var adelaideTz = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Australia/Adelaide");

            using (var db = new MyDbContext())
            {
                var q = from r in db.Routes
                        where r.Airport_SourceAirport.FaaIata == "ADL"
                        select new { r.Airport_DestinationAirport.Name, r.Airport_DestinationAirport.Tzdb, r.Airport_DestinationAirport.FaaIata, r.Id };

                var results = q.ToList();

                foreach (var r in results)
                {
                    var destTz = DateTimeZoneProviders.Tzdb.GetZoneOrNull(r.Tzdb);

                    var start = adelaideTz.AtLeniently(startDate.At(new LocalTime(22, 0))); // LocalDateTime.FromDateTime(new DateTime(2015, 10, 3, 22, 0, 0)));
                    var finish = destTz.AtLeniently(startDate.PlusDays(1).At(new LocalTime(9, 30))); // LocalDateTime.FromDateTime(new DateTime(2015, 10, 4, 9, 30, 0)));

                    yield return new FlightDto() { Code = r.Id.ToString(), Start = start, Finish = finish, Duration = (finish.ToInstant() - start.ToInstant()), Source = "Adelaide", Destination = r.Name };
                }
            }
        }
    }
}
