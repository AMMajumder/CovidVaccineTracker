using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaccineTrackerServer.Interfaces;
using VaccineTrackerServer.Models;

namespace VaccineTrackerServer.Repository
{
    public class SubscriberInfoRepository : ISubscriberInfoRepository
    {
        public ISubscriberInfoDataAccess SubscriberInfoDataAccess { get; set; }
        public SubscriberInfoRepository(ISubscriberInfoDataAccess SubscriberInfoDataAccess)
        {
            this.SubscriberInfoDataAccess = SubscriberInfoDataAccess;
        }

        public void Init(string ConnectionString, string DatabaseName)
        {
            SubscriberInfoDataAccess.Init(ConnectionString,DatabaseName);
        }
        public Task AddSubsriberInfo(SubscriberInfoModel subscriber)
        {
            throw new NotImplementedException();
        }
    }
}
