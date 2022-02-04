using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VaccineTrackerServer.Models;
using System.Net.Http;
using VaccineTrackerServer.Interfaces;
using System.Net;

namespace VaccineTrackerServer.Backend
{
    public class AddSubscriberInfoFunction
    {
        private readonly string DatabaseName = "CoVTracker";
        private readonly string ConnectionString = "ConnectionString";
        public ISubscriberInfoRepository SubscriberInfoRepository { get; set; }
        public ISubscriberInfoDataAccess SubscriberInfoDataAccess { get; set; }
        public AddSubscriberInfoFunction(ISubscriberInfoRepository SubscriberInfoRepository, ISubscriberInfoDataAccess SubscriberInfoDataAccess)
        {
            this.SubscriberInfoRepository = SubscriberInfoRepository;
            this.SubscriberInfoDataAccess = SubscriberInfoDataAccess;
        }
        [FunctionName("add-subscriber-info-function-post")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "AddSubscriberInfoFunction")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("add-subscriber-info-function-post: Start");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                SubscriberInfoModel info = JsonConvert.DeserializeObject<SubscriberInfoModel>(requestBody);

                info.SubscriptionID = System.DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                info.Identifier = Guid.NewGuid().ToString();

                var DBConnectionString = Environment.GetEnvironmentVariable(ConnectionString);
                SubscriberInfoRepository.Init(DBConnectionString,DatabaseName);
                var createdResource = await SubscriberInfoRepository.AddSubsriberInfo(info);
                if (createdResource.SubscriptionID.Equals(info.SubscriptionID))
                {
                    return new HttpResponseMessage(HttpStatusCode.Created);
                }

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                log.LogInformation($"add-subscriber-info-function-post: failed");
                log.LogInformation($"Message: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            finally
            {
                log.LogInformation("add-subscriber-info-function-post: End");
            }

        }
    }
}
