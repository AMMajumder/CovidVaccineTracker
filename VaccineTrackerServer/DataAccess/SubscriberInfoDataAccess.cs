using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<SubscriberInfoModel>> GetActiveSubsriptions(string SubscriberID=null)
        {
            List<SubscriberInfoModel> result;
            if(!string.IsNullOrEmpty(SubscriberID))
            {
                result = Container.GetItemLinqQueryable<SubscriberInfoModel>(true).Where(x => x.SubscriberID == SubscriberID).Where(x => x.IsAlertEnabled == true).AsEnumerable().ToList();
            }
            else
            {
                result = Container.GetItemLinqQueryable<SubscriberInfoModel>(true).Where(x => x.IsAlertEnabled == true).AsEnumerable().ToList();
            }
            return result;
        }
        public async Task<SubscriberInfoModel> UnSubscribeToAlerts(string SubscriptionID)
        {
            var item = Container.GetItemLinqQueryable<SubscriberInfoModel>(true).Single(x => x.SubscriptionID == SubscriptionID);
            var result = await Container.DeleteItemAsync<SubscriberInfoModel>(item.Identifier, new PartitionKey(item.SubscriberID));
            return result;
        }
    }
}
