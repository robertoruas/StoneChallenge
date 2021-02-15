using Customer.Repository.DatabaseSettings.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Customer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Domain.Customer> _customers;
        public CustomerRepository(ICustomerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _customers = database.GetCollection<Domain.Customer>(settings.CustomersCollectionName);
        }

        public List<Domain.Customer> GetCustomers() => _customers.Find(c => true).ToList();

        public Domain.Customer GetCustomer(string id) => _customers.Find(c => c.Id == id).FirstOrDefault();

        public Domain.Customer GetCustomer(long cpf) => _customers.Find(c => c.CPF == cpf).FirstOrDefault();

        public Domain.Customer Create(Domain.Customer customer)
        {
            try
            {
                _customers.InsertOne(customer);

                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string id, Domain.Customer customer) => _customers.ReplaceOne(c => c.Id == id, customer);

    }
}
