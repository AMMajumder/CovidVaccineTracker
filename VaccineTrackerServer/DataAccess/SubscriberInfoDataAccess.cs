using Microsoft.Azure.Cosmos;
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
        private string ContainerID { get; set; } = "SubscriberInfo";
        private Container Container { get; set; }
        public void Init(string ConnectionString, string DatabaseName)
        {
            this.ConnectionString = ConnectionString;
            this.Database = DatabaseName;
            CosmosClient client = new CosmosClient(this.ConnectionString);
            Database database = client.GetDatabase(this.Database);
            Container = database.GetContainer(ContainerID);
        }
        public async Task<SubscriberInfoModel> AddInfo(SubscriberInfoModel subscriber)
        {
            var result = await Container.CreateItemAsync<SubscriberInfoModel>(subscriber,new PartitionKey(subscriber.SubscriberID));
            return result.Resource;
        }
    }
}
