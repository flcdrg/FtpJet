using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NodaTime;

namespace FtpJet.Controllers
{
    public class FlightDto
    {
        public string Code { get; set; }
        public ZonedDateTime Start { get; set; }
        public ZonedDateTime Finish { get; set; }
        public Duration Duration { get; set; }
    }

    public class FlightsController : ApiController
    {
        // GET: api/Flights
        public IEnumerable<FlightDto> Get()
        {
            var adelaideTz = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Australia/Adelaide");
            var sydneyTz = DateTimeZoneProviders.Tzdb.GetZoneOrNull("Australia/Sydney");

            var start = adelaideTz.AtLeniently(LocalDateTime.FromDateTime(new DateTime(2015, 10, 3, 22, 0, 0)));
            var finish = sydneyTz.AtLeniently(LocalDateTime.FromDateTime(new DateTime(2015, 10, 4, 9, 30, 0)));

            return new FlightDto[] { new FlightDto() { Code = "QF0", Start = start, Finish = finish, Duration = (finish.ToInstant() - start.ToInstant()) } };
        }

        // GET: api/Flights/5
        public string Get(int id)
        {
            return "value";
        }

    }
}
