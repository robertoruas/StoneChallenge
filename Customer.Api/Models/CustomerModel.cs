using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Customer.Api.Models
{
    public class CustomerModel
    {
        [Required(ErrorMessage = "Informe o nome do cliente.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o estado do cliente.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Informe o CPF do cliente.")]
        [CPFValidation(ErrorMessage = "Informe um CPF válido.")]
        public string CPF { get; set; }

        public long ParseCPFToInt()
        {
            return Convert.ToInt64(Regex.Replace(CPF, "[^0-9]+", ""));
        }
    }
}
