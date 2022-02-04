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

namespace VaccineTrackerServer.Backend
{
    public class AddSubscriberInfoFunction
    {
        public ISubscriberInfoRepository SubscriberInfoRepository { get; set; }
        public ISubscriberInfoDataAccess SubscriberInfoDataAccess { get; set; }
        public AddSubscriberInfoFunction(ISubscriberInfoRepository SubscriberInfoRepository, ISubscriberInfoDataAccess SubscriberInfoDataAccess)
        {
            this.SubscriberInfoRepository = SubscriberInfoRepository;
            this.SubscriberInfoDataAccess = SubscriberInfoDataAccess;
        }
        [FunctionName("add-subscriber-info-function-post")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "AddSubscriberInfoFunction")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("add-subscriber-info-function-post: Start");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                SubscriberInfoModel info = JsonConvert.DeserializeObject<SubscriberInfoModel>(requestBody);


                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                log.LogInformation($"add-subscriber-info-function-post: failed");
                log.LogInformation($"Message: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            finally
            {
                log.LogInformation("add-subscriber-info-function-post: End");
            }

        }
    }
}
