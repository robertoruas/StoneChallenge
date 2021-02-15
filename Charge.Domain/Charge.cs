using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.RegularExpressions;

namespace Charge.Domain
{
    public class Charge
    {
        public Charge(long cpf, DateTime dueDate, decimal chargeValue)
        {
            CPF = cpf;
            DueDate = dueDate.ToString("yyyy-MM-dd");
            ChargeValue = chargeValue;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; private set; }

        public string DueDate { get; private set; }

        public decimal ChargeValue { get; private set; }

        public long CPF { get; private set; }

        public string CPFFormatted()
        {
            return CPF.ToString().PadLeft(11, '0');
        }
    }
}
