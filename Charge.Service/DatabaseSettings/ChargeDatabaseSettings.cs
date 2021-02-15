using Charge.Repository.DatabaseSettings.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Charge.Repository.DatabaseSettings
{
    public class ChargeDatabaseSettings : IChargeDatabaseSettings
    {
        public string ChargesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
