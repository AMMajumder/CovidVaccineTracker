using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VaccineTrackerServer.Models
{
    public class CenterInfoModel
    {
        [JsonProperty("centers")]
        public Slots Slots { get; set; }
    }

    public class Slots
    {
        [JsonProperty("sessions")]
        public List<Sessions> Sessions { get; set; }
    }
    public class Sessions
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("available_capacity_dose1")]
        public string AvailableCapacityDose1 { get; set; }
        [JsonProperty("available_capacity_dose2")]
        public string AvailableCapacityDose2 { get; set; }
        [JsonProperty("min_age_limit")]
        public string MinAgeLimit { get; set; }
        [JsonProperty("vaccine")]
        public string Vaccine { get; set; }
    }

}