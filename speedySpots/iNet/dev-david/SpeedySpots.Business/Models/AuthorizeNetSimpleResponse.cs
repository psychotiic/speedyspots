using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedySpots.Business.Models
{
    public class AuthorizeNetSimpleResponse
    {
        public int ResponseCode { get; set; }
        public int ResponseReasonCode { get; set; }
        public string ResponseReasonText { get; set; }
        public string TransactionId { get; set; }
    }
}
