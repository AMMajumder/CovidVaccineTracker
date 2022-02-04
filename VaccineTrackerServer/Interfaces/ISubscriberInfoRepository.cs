using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaccineTrackerServer.Models;

namespace VaccineTrackerServer.Interfaces
{
    public interface ISubscriberInfoRepository
    {
        public void Init(string ConnectionString, string DatabaseName);
        public Task<SubscriberInfoModel> AddSubsriberInfo(SubscriberInfoModel subscriber);
        public Task<List<SubscriberInfoModel>> GetActiveSubsriptions();
    }
}
