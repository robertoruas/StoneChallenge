using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Charge.Api.Models
{
    public class ChargeModel
    {
        [Required(ErrorMessage = "Informe o CPF do cliente")]
        [CPFValidation(ErrorMessage = "Informe um CPF válido.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe o valor para cobrança")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Informe a data de vencimento da cobrança")]
        public DateTime DueDate { get; set; }

        public long ParseCPFToInt()
        {
            return Convert.ToInt64(Regex.Replace(CPF, "[^0-9]+", ""));
        }
    }
}
