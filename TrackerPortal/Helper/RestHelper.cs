using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrackerPortal.Model;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TrackerPortal.Helper
{
    public static class RestHelper
    {
        private const string urlBase = "https://cdn-api.co-vin.in/api";
        private const string FetchStatesURL = "/v2/admin/location/states";
        private const string FetchDistrictsURL = "/v2/admin/location/districts/";
        private const string FetchCentersURL = "/v2/appointment/sessions/public/findByDistrict";

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
        public static async Task<List<Districts>> PopulateDistricts(int stateID)
        {
            HttpClient httpClient = null;
            var districtsList = new DistrictsModel();
            string contentString = null;

            try
            {

                var requestURL = $"{urlBase}{FetchDistrictsURL}{stateID}";

                using (httpClient = new HttpClient())
                {
                    HttpResponseMessage result = new HttpResponseMessage();

                    result = await httpClient.GetAsync(requestURL);

                    contentString = await result.Content.ReadAsStringAsync();

                    districtsList = JsonConvert.DeserializeObject<DistrictsModel>(contentString);

                }
            }
            catch (Exception ex)
            {
                districtsList = new DistrictsModel();
            }
            finally
            {
                if (httpClient != null) httpClient = null;
            }
            return districtsList.AllDistricts;
        }
        public static async Task<List<Centers>> PopulateCenters(int districtID, DateTime appointmentDate)
        {
            HttpClient httpClient = null;
            var centersList = new CentersModel();
            string contentString = null;

            try
            {
                string chosenAppointmentDate = appointmentDate.ToString("dd-MM-yyyy");
                var requestURL = $"{urlBase}{FetchCentersURL}?district_id={districtID}&date={chosenAppointmentDate}";

                using (httpClient = new HttpClient())
                {
                    HttpResponseMessage result = new HttpResponseMessage();

                    result = await httpClient.GetAsync(requestURL);

                    contentString = await result.Content.ReadAsStringAsync();

                    centersList = JsonConvert.DeserializeObject<CentersModel>(contentString);

                }
            }
            catch (Exception ex)
            {
                centersList = new CentersModel() { AllCenters = new List<Centers>() };
            }
            finally
            {
                if (httpClient != null) httpClient = null;
            }
            return centersList.AllCenters;
        }
        public static async Task<bool> SendEmail(EmailModel mailInfo)
        {
            HttpClient httpClient = null;

            try
            {
                var requestURL = AppData.configuration.GetValue<string>("Email-api-url");

                using (httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(mailInfo), Encoding.UTF8, "application/json");

                    HttpResponseMessage result = new HttpResponseMessage();

                    result = await httpClient.PostAsync(requestURL, content);

                    if (result.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (httpClient != null) httpClient = null;
            }
        }
        public static async Task<bool> SaveSubscription(SubscriberInfoModel model)
        {
            HttpClient httpClient = null;

            try
            {
                var requestURL = AppData.configuration.GetValue<string>("Save-subscription-api-url");

                using (httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    HttpResponseMessage result = new HttpResponseMessage();

                    result = await httpClient.PostAsync(requestURL, content);

                    if (result.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (httpClient != null) httpClient = null;
            }
        }
        public static async Task<List<SubscriberInfoModel>> GetSubscription(SubscriberInfoModel model)
        {
            HttpClient httpClient = null;
            List<SubscriberInfoModel> subscriptions = new List<SubscriberInfoModel>();
            try
            {
                var requestURL = AppData.configuration.GetValue<string>("Get-subscription-api-url");

                using (httpClient = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    HttpResponseMessage result = new HttpResponseMessage();

                    result = await httpClient.PostAsync(requestURL, content);

                    var contentString = await result.Content.ReadAsStringAsync();

                    subscriptions = JsonConvert.DeserializeObject<List<SubscriberInfoModel>>(contentString);

                }
            }
            catch (Exception ex)
            {
                subscriptions = new List<SubscriberInfoModel>();
            }
            finally
            {
                if (httpClient != null) httpClient = null;
            }
            return subscriptions;
        }
    }
}
