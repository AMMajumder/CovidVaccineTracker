using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using VaccineTrackerServer.Interfaces;
using VaccineTrackerServer.Models;
using System.Net;

namespace VaccineTrackerServer.Backend
{
    public class UnsubscribeToAlerts
    {
        private readonly string DatabaseName = "CoVTracker";
        private readonly string ConnectionString = "ConnectionString";
        public ISubscriberInfoRepository SubscriberInfoRepository { get; set; }
        public ISubscriberInfoDataAccess SubscriberInfoDataAccess { get; set; }
        public UnsubscribeToAlerts(ISubscriberInfoRepository SubscriberInfoRepository, ISubscriberInfoDataAccess SubscriberInfoDataAccess)
        {
            this.SubscriberInfoRepository = SubscriberInfoRepository;
            this.SubscriberInfoDataAccess = SubscriberInfoDataAccess;
        }
        [FunctionName("un-subscribe-to-alerts-function-post")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("un-subscribe-to-alerts-function-post: Start");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                SubscriberInfoModel info = JsonConvert.DeserializeObject<SubscriberInfoModel>(requestBody);

                var DBConnectionString = Environment.GetEnvironmentVariable(ConnectionString);
                SubscriberInfoRepository.Init(DBConnectionString, DatabaseName);
                if (await SubscriberInfoRepository.UnSubscribeToAlerts(info.SubscriptionID)) 
                { 
                    return new HttpResponseMessage(HttpStatusCode.OK); 
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                log.LogInformation($"un-subscribe-to-alerts-function-post: failed");
                log.LogInformation($"Message: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            finally
            {
                log.LogInformation("un-subscribe-to-alerts-function-post: End");
            }
        }
    }
}
