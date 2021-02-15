using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Repository.DatabaseSettings.Interface
{
    public interface ICustomerDatabaseSettings
    {
        string CustomersCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
