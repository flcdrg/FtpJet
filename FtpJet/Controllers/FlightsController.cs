using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FtpJet.Controllers
{
    public class FlightsController : ApiController
    {
        // GET: api/Flights
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Flights/5
        public string Get(int id)
        {
            return "value";
        }

    }
}
