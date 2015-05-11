using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FtpJet.Models
{
    public class HomeModel
    {
        public IDictionary<string, string> Locations { get; set; }
        public string Origin
        {
            get; set;   
        }
    }
}