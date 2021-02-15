using Charge.Repository.DatabaseSettings.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Charge.Repository
{
    public class ChargeRepository : IChargeRepository
    {
        private readonly IMongoCollection<Domain.Charge> _charges;

        public ChargeRepository(IChargeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var databse = client.GetDatabase(settings.DatabaseName);

            _charges = databse.GetCollection<Domain.Charge>(settings.ChargesCollectionName);
        }

        public List<Domain.Charge> GetCharges() => _charges.Find(c => true).ToList();

        public List<Domain.Charge> GetCharges(long? cpf, string dueDate) => _charges.Find(c => 
            (c.CPF == cpf) || 
            (c.DueDate == dueDate)).ToList();

        public Domain.Charge GetCharge(string id) => _charges.Find(c => c.Id == id).FirstOrDefault();

        public Domain.Charge GetCharge(long cpf, string dueDate) => _charges.Find(c => c.CPF == cpf && c.DueDate == dueDate).FirstOrDefault();

        public Domain.Charge Create(Domain.Charge charge)
        {
            try
            {
                _charges.InsertOne(charge);

                return charge;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string id, Domain.Charge Charge) => _charges.ReplaceOne(c => c.Id == id, Charge);

    }
}
