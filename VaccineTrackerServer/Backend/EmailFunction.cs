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
using VaccineTrackerServer.Helper;
using VaccineTrackerServer.Models;

namespace VaccineTrackerServer.Backend
{
    public class EmailFunction
    {
        [FunctionName("send-mail-function-post")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "SendMailFunction")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("send-mail-function-post: Start");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Mail info = JsonConvert.DeserializeObject<Mail>(requestBody);

                SMTPHelper helper = new SMTPHelper();
                helper.SendMail(info.From, info.To, info.Subject, info.Body);

                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                log.LogInformation($"send-mail-function-post: failed");
                log.LogInformation($"Message: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            finally
            {
                log.LogInformation("send-mail-function-post: End");
            }

        }
    }
}
