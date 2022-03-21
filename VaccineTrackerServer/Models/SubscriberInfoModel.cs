using System;
using System.Collections.Generic;
using System.Text;

namespace VaccineTrackerServer.Models
{
    public class SubscriberInfoModel
    {
        public string Identifier { get; set; }
        public string SubscriberID { get; set; }
        public string Center { get; set; }
        public bool IsAlertEnabled { get; set; }
    }
}
