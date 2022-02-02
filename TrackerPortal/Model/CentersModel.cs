using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackerPortal.Model
{
    public class CentersModel
    {
        [JsonProperty("sessions")]
        public List<Centers> AllCenters { get; set; }
    }
    public class Centers
    {
        [JsonProperty("center_id")]
        public int CenterID { get; set; }
        [JsonProperty("name")]
        public string CenterName { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("block_name")]
        public string BlockName { get; set; }
        [JsonProperty("pincode")]
        public string PinCode { get; set; }
        [JsonProperty("fee_type")]
        public string FeeType { get; set; }
        [JsonProperty("available_capacity")]
        public string AvailableCapacity { get; set; }
        [JsonProperty("available_capacity_dose1")]
        public string AvailableCapacityDose1 { get; set; }
        [JsonProperty("available_capacity_dose2")]
        public string AvailableCapacityDose2 { get; set; }
        [JsonProperty("fee")]
        public string Fee { get; set; }
        [JsonProperty("min_age_limit")]
        public string MinAgeLimit { get; set; }
        [JsonProperty("max_age_limit")]
        public string MaxAgeLimit { get; set; }
        [JsonProperty("vaccine")]
        public string Vaccine { get; set; }
        [JsonProperty("slots")]
        public List<string> Slots { get; set; }
    }
}
