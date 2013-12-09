using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedySpots.Business.Models
{
    internal class SmtpSettings
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}