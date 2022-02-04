using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VaccineTrackerServer.DataAccess;
using VaccineTrackerServer.Interfaces;
using VaccineTrackerServer.Repository;
[assembly: FunctionsStartup(typeof(VaccineTrackerServer.Backend.Startup))]
namespace VaccineTrackerServer.Backend
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<ISubscriberInfoDataAccess, SubscriberInfoDataAccess>();
            builder.Services.AddTransient<ISubscriberInfoRepository, SubscriberInfoRepository>();
        }
    } 
}
