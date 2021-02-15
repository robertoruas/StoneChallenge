using Customer.Repository.DatabaseSettings.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Repository.DatabaseSettings
{
    public class CustomerDatabaseSettings : ICustomerDatabaseSettings
    {
        public string CustomersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
