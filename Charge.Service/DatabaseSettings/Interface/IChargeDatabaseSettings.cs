using System;
using System.Collections.Generic;
using System.Text;

namespace Charge.Repository.DatabaseSettings.Interface
{
    public interface IChargeDatabaseSettings
    {
        string ChargesCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
