using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedySpots.Business.Models
{
    public class Talent
    {
        public Guid MPUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Fullname
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        public List<TalentType> TalentTypes { get; set; }
    }
}