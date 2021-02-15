using System;

namespace UsageCalculation.Domain
{
    public class Customer 
    {   
        public string Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public long CPF { get; set; }

        public string CPFFormatted()
        {
            return CPF.ToString().PadLeft(11, '0');
        }

        public decimal CalculateUsage()
        {
            string value = $"{CPF.ToString().Substring(0, 2)}{CPF.ToString().Substring(CPF.ToString().Length - 2, 2)}";

            return Convert.ToDecimal(value);
        }
    }
}
