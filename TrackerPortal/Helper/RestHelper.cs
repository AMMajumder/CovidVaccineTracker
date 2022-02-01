using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TrackerPortal.Model;

namespace TrackerPortal.Helper
{
    public static class RestHelper
    {
        private const string urlBase = "https://cdn-api.co-vin.in/api";
        private const string FetchStatesURL = "/v2/admin/location/states";

        public static async Task<List<States>> PopulateStates()
        {
            HttpClient httpClient = null;
            var statesList = new StatesModel();
            string contentString = null;

            try
            {

                var requestURL = $"{urlBase}{FetchStatesURL}";

                using (httpClient = new HttpClient())
                {
                    HttpResponseMessage result = new HttpResponseMessage();

                    result = await httpClient.GetAsync(requestURL);

                    contentString = await result.Content.ReadAsStringAsync();

                    statesList = JsonConvert.DeserializeObject<StatesModel>(contentString);

                }
            }
            catch (Exception ex)
            {
                statesList = new StatesModel();
            }
            finally
            {
                if (httpClient != null) httpClient = null;
            }
            return statesList.AllStates;
        }
        
    }
}
