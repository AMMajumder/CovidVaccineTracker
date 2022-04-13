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
using VaccineTrackerServer.Models;
using VaccineTrackerServer.Interfaces;
using System.Net;

namespace VaccineTrackerServer.Backend
{
    public class GetSubscriptionsForSubscriber
    {
        private readonly string DatabaseName = "CoVTracker";
        private readonly string ConnectionString = "ConnectionString";
        public ISubscriberInfoRepository SubscriberInfoRepository { get; set; }
        public ISubscriberInfoDataAccess SubscriberInfoDataAccess { get; set; }
        public GetSubscriptionsForSubscriber(ISubscriberInfoRepository SubscriberInfoRepository, ISubscriberInfoDataAccess SubscriberInfoDataAccess)
        {
            this.SubscriberInfoRepository = SubscriberInfoRepository;
            this.SubscriberInfoDataAccess = SubscriberInfoDataAccess;
        }
        [FunctionName("get-subscriber-info-function-post")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "GetSubscriberInfoFunction")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("get-subscriber-info-function-post: Start");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                SubscriberInfoModel info = JsonConvert.DeserializeObject<SubscriberInfoModel>(requestBody);

                var DBConnectionString = Environment.GetEnvironmentVariable(ConnectionString);
                SubscriberInfoRepository.Init(DBConnectionString, DatabaseName);

                var subscriberDetails = await SubscriberInfoRepository.GetActiveSubsriptions(info.SubscriberID);

                return new OkObjectResult(subscriberDetails);
            }
            catch (Exception ex)
            {
                log.LogInformation($"get-subscriber-info-function-post: failed");
                log.LogInformation($"Message: {ex.Message}");
                return new BadRequestObjectResult(null);
            }
            finally
            {
                log.LogInformation("get-subscriber-info-function-post: End");
            }
        }
    }
}
