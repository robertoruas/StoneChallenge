using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.RegularExpressions;

namespace Customer.Domain
{
    public class Customer
    {
        public Customer(string name, string state, string cpf)
        {
            Name = name;
            State = state;
            CPF = Convert.ToInt64(Regex.Replace(cpf, "[^0-9]+", ""));
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public string Name { get; private set; }

        public string State { get; private set; }

        public long CPF { get; private set; }

        public string CPFFormatted()
        {
            return CPF.ToString().PadLeft(11, '0');
        }
    }
}
