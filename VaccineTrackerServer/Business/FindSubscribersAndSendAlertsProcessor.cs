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
        private readonly string EmailSubject = "CoV Tracker Alerts";
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
            if(activeSubscriptions!=null || activeSubscriptions.Count > 0)
            {
                var activeSubscribers = activeSubscriptions.GroupBy(x => x.SubscriberID).Select(x => x.Key);

                Parallel.ForEach(activeSubscribers, async subscriber =>
                {
                    string EmailBody = $"<!DOCTYPE html><html><head><style>table, th, td {{border: 1px solid #000000;border-collapse: collapse;}} </style></head><body><div>Dear {subscriber.Split('@')[0]},</div><h4>Please find below vaccine availability information for your subscribed centers</h4>";
                    var subscriptionsCurrentUser = activeSubscriptions.Where(x => x.SubscriberID.Equals(subscriber));

                    string EmailReceiver = subscriber;
                    List<int> subscribedCenters = new List<int>();
                    foreach (var subscription in subscriptionsCurrentUser)
                    {
                        subscribedCenters.Add(subscription.CenterID);

                        var sessions = await RestHelper.PopulateSessions(subscription.CenterID, DateTime.Now.AddDays(1));
                        if(sessions!=null || sessions.Count > 0)
                        {
                            sessions = sessions.Take(5).ToList();

                            EmailBody += ($"<br/><div>Center - {subscription.CenterName}</div>");
                            EmailBody += ($"<table><tr><th>Date</th><th>Vaccine</th><th>Dose 1</th><th>Dose 2</th><th>Min. Age</th></tr>");

                            foreach (var session in sessions)
                            {
                                EmailBody += ("<tr>");
                                EmailBody += ($"<td>{session.Date}</td><td>{session.Vaccine}</td><td>{session.AvailableCapacityDose1}</td><td>{session.AvailableCapacityDose2}</td><td>{session.MinAgeLimit}</td>");
                                EmailBody += ("</tr>");
                            }
                            EmailBody += ("</table>");
                            EmailBody += ("<br/>").ToString();
                        }
                    }
                    EmailBody += ("<br/><span>For more info </span><a href=\"https://www.cowin.gov.in/\">click here</a>");
                    EmailBody += ("<br/><br/><div>Regards,</div><div>CoVTracker Team</div></body></html>");
                    SMTPHelper helper = new SMTPHelper();
                    helper.SendMail(null, EmailReceiver, EmailSubject, EmailBody);

                });
            }
        } 
    }
}
