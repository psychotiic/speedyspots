using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedySpots.Business.Models
{
    public class StatusOption
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SystemName
        {
            get
            {
                return Name.Replace(" ", "").Replace("-", "");
            }
        }
    }
}