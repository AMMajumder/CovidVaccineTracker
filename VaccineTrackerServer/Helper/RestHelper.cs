using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VaccineTrackerServer.Models;

namespace VaccineTrackerServer.Helper
{
    public static class RestHelper
    {
        private const string urlBase = "https://cdn-api.co-vin.in/api";
        private const string FetchCenterInfoURL = "/v2/appointment/sessions/public/calendarByCenter";
        public static async Task<List<Sessions>> PopulateSessions(int centerID, DateTime appointmentDate)
        {
            HttpClient httpClient = null;
            var centersList = new CenterInfoModel();
            string contentString = null;

            try
            {
                string chosenAppointmentDate = appointmentDate.ToString("dd-MM-yyyy");
                var requestURL = $"{urlBase}{FetchCenterInfoURL}?center_id={centerID}&date={chosenAppointmentDate}";

                using (httpClient = new HttpClient())
                {
                    HttpResponseMessage result = new HttpResponseMessage();

                    result = await httpClient.GetAsync(requestURL);

                    contentString = await result.Content.ReadAsStringAsync();

                    centersList = JsonConvert.DeserializeObject<CenterInfoModel>(contentString);

                }
            }
            catch (Exception ex)
            {
                centersList = new CenterInfoModel() { Slots = new Slots() { Sessions = new List<Sessions>() } };
            }
            finally
            {
                if (httpClient != null) httpClient = null;
            }
            return centersList.Slots.Sessions;
        }
    }
}
