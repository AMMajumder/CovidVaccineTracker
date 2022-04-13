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
        public async Task<SubscriberInfoModel> AddSubsriberInfo(SubscriberInfoModel subscriber)
        {
            var result = await SubscriberInfoDataAccess.AddInfo(subscriber);
            return result;
        }
        public async Task<List<SubscriberInfoModel>> GetActiveSubsriptions(string SubscriberID=null)
        {
            var result = await SubscriberInfoDataAccess.GetActiveSubsriptions(SubscriberID);
            return result;
        }
    }
}
