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
        public string SubscriberID{ get; set; }
        [JsonProperty("subscriptionid")]
        public string SubscriptionID { get; set; }
        [JsonProperty("center")]
        public string Center { get; set; }
        [JsonProperty("isalertenabled")]
        public bool IsAlertEnabled { get; set; }
    }
}
