using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaccineTrackerServer.Models
{
    public class SubscriberInfoModel
    {
        [JsonProperty("id")]
        public string Identifier { get; set; }
        [JsonProperty("subscriberid")]
        public string SubscriberID { get; set; }
        [JsonProperty("subscriptionid")]
        public string SubscriptionID { get; set; }
        [JsonProperty("centername")]
        public string CenterName { get; set; }
        [JsonProperty("centerid")]
        public int CenterID { get; set; }
        [JsonProperty("isalertenabled")]
        public bool IsAlertEnabled { get; set; }
    }
}
