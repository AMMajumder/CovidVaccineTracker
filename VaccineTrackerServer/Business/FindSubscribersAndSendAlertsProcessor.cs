using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineTrackerServer.Helper;
using VaccineTrackerServer.Interfaces;
using VaccineTrackerServer.Models;

namespace VaccineTrackerServer.Business
{
    public class FindSubscribersAndSendAlertsProcessor
    {
        private readonly string EmailSubject = "CoVTracker Alerts";
        private readonly string DatabaseName = "CoVTracker";
        private readonly string ConnectionString = "ConnectionString";
        public ISubscriberInfoRepository SubscriberInfoRepository { get; set; }
        public ISubscriberInfoDataAccess SubscriberInfoDataAccess { get; set; }
        public FindSubscribersAndSendAlertsProcessor(ISubscriberInfoRepository SubscriberInfoRepository, ISubscriberInfoDataAccess SubscriberInfoDataAccess)
        {
            this.SubscriberInfoRepository = SubscriberInfoRepository;
            this.SubscriberInfoDataAccess = SubscriberInfoDataAccess;
        }
        public async Task Process()
        {
            var DBConnectionString = Environment.GetEnvironmentVariable(ConnectionString);
            SubscriberInfoRepository.Init(DBConnectionString, DatabaseName);
            var activeSubscriptions = await SubscriberInfoRepository.GetActiveSubsriptions();

            var activeSubscribers = activeSubscriptions.GroupBy(x => x.SubscriberID).Select(x => x.Key);

            Parallel.ForEach(activeSubscribers, async subscriber =>
            {
                string EmailBody = $"<div>Dear {subscriber},</div><h4>Please find below vaccine availability information for your subscribed centers</h4>";
                var subscriptionsCurrentUser = activeSubscriptions.Where(x => x.SubscriberID.Equals(subscriber));

                string EmailReceiver = subscriber.Split('@')[0];
                List<int> subscribedCenters = new List<int>();
                foreach (var subscription in subscriptionsCurrentUser)
                {
                    subscribedCenters.Add(subscription.CenterID);

                    var sessions = await RestHelper.PopulateSessions(subscription.CenterID, DateTime.Now);

                    sessions = sessions.Take(5).ToList();

                    EmailBody+=($"<br/><div>Center - {subscription.CenterName}</div><div>Available capacity:</div>").ToString();

                    foreach(var session in sessions)
                    {
                        EmailBody+=($"<div>{session.Date}: Vaccine - {session.Vaccine}, Dose 1 - {session.AvailableCapacityDose1}, Dose 2 - {session.AvailableCapacityDose2}, Min. Age - {session.MinAgeLimit}</div>").ToString();
                    }
                    EmailBody+=("<br/>").ToString();

                }
                EmailBody+=("<br/><br/><div>Regards,</div><div>CoVTracker Team</div>").ToString();
                SMTPHelper helper = new SMTPHelper();
                helper.SendMail(null, EmailReceiver, EmailSubject, EmailBody);

            });

        } 
    }
}
