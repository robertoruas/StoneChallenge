using System;
using System.Collections.Generic;
using System.Text;

namespace UsageCalculation.Domain
{
    public class Charge
    {
        public Charge(DateTime dueDate, decimal value, long cpf)
        {
            DueDate = dueDate;
            ChargeValue = value;
            CPF = cpf;
        }

        public string Id { get; private set; }

        public DateTime DueDate { get; private set; }

        public decimal ChargeValue { get; private set; }

        public long CPF { get; private set; }

        public string CPFFormatted()
        {
            return CPF.ToString().PadLeft(11, '0');
        }
    }
}
