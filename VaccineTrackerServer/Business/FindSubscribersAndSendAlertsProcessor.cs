using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineTrackerServer.Interfaces;
using VaccineTrackerServer.Models;

namespace VaccineTrackerServer.Business
{
    public class FindSubscribersAndSendAlertsProcessor
    {
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

            Parallel.ForEach(activeSubscribers, subscriber =>
            {
                var subscriptionsCurrentUser = activeSubscriptions.Where(x => x.SubscriberID.Equals(subscriber));

                string EmailReceiver = subscriber;
                List<int> subscribedCenters = new List<int>();
                foreach (var subscription in subscriptionsCurrentUser)
                {
                    subscribedCenters.Add(subscription.CenterID);

                    //call cowin api for each center and check available slots
                    //store the available slots in list<int>
                }
                //call email function.


            });

        } 
    }
}
