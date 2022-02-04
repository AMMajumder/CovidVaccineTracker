using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaccineTrackerServer.Interfaces;
using VaccineTrackerServer.Models;

namespace VaccineTrackerServer.DataAccess
{
    public class SubscriberInfoDataAccess : ISubscriberInfoDataAccess
    {
        private string ConnectionString { get; set; }
        private string Database { get; set; }
        private string Container { get; set; } = "SubscriberInfo";
        public void Init(string ConnectionString, string DatabaseName)
        {
            this.ConnectionString = ConnectionString;
            this.Database = DatabaseName;
        }
        public Task AddInfo(SubscriberInfoModel subscriber)
        {
            throw new NotImplementedException();
        }
    }
}
