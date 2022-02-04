﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaccineTrackerServer.Interfaces;

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
        } 
    }
}
