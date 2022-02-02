using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackerPortal.Model
{
    public class DistrictsModel
    {
        [JsonProperty("districts")]
        public List<Districts> AllDistricts { get; set; }
    }
    public class Districts
    {
        [JsonProperty("district_id")]
        public int DistrictID { get; set; }
        [JsonProperty("district_name")]
        public string DistrictName { get; set; }
    }
}
