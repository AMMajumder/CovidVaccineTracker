﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaccineTrackerServer.Models;

namespace VaccineTrackerServer.Interfaces
{
    public interface ISubscriberInfoDataAccess
    {
        public void Init(string ConnectionString, string DatabaseName);
        public Task<SubscriberInfoModel> AddInfo(SubscriberInfoModel subscriber);
    }
}