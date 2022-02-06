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
using System.Net;
using VaccineTrackerServer.Models;
using VaccineTrackerServer.Interfaces;
using VaccineTrackerServer.Business;

namespace VaccineTrackerServer.Backend
{
    public class SendPeriodicalAlertsToSubscribersFunction
    {
        public ISubscriberInfoRepository SubscriberInfoRepository { get; set; }
        public ISubscriberInfoDataAccess SubscriberInfoDataAccess { get; set; }
        public SendPeriodicalAlertsToSubscribersFunction(ISubscriberInfoRepository SubscriberInfoRepository, ISubscriberInfoDataAccess SubscriberInfoDataAccess)
        {
            this.SubscriberInfoRepository = SubscriberInfoRepository;
            this.SubscriberInfoDataAccess = SubscriberInfoDataAccess;
        }
        [FunctionName("send-periodical-alerts-to-subscribers-function-post")]
        public async Task Run(
        [TimerTrigger("0 28 14 * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                log.LogInformation("send-periodical-alerts-to-subscribers-function-post: Start");

                FindSubscribersAndSendAlertsProcessor processor = new FindSubscribersAndSendAlertsProcessor(SubscriberInfoRepository, SubscriberInfoDataAccess);

                await processor.Process();
                //return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                log.LogInformation($"send-periodical-alerts-to-subscribers-function-post: failed");
                log.LogInformation($"Message: {ex.Message}");
                //return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            finally
            {
                log.LogInformation("send-periodical-alerts-to-subscribers-function-post: End");
            }
        }
    }
}
