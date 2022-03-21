using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaccineTrackerServer.Models;

namespace VaccineTrackerServer.Interfaces
{
    public interface ISubscriberInfoRepository
    {
        public Task AddSubsriberInfo(SubscriberInfoModel subscriber);
    }
}
