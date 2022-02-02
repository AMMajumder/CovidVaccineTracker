using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackerPortal.Model
{
    public class StatesModel
    {
        [JsonProperty("states")]
        public List<States> AllStates { get; set; }
    }
    public class States
    {
        [JsonProperty("state_id")]
        public int StateID { get; set; }
        [JsonProperty("state_name")]
        public string StateName { get; set; }
    }
}
