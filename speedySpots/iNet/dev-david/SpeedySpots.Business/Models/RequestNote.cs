using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedySpots.Business.Models
{
    public class RequestNote
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Note { get; set; }
    }
}