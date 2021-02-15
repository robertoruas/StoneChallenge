using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Customer.Util
{
    public static class Validations
    {
        public static bool ValidateCPF(string cpf)
        {
            cpf = cpf.Trim();
            cpf = Regex.Replace(cpf, "[^0-9]+", "");

            if (cpf == "00000000000" ||
                cpf == "11111111111" ||
                cpf == "22222222222" ||
                cpf == "33333333333" ||
                cpf == "44444444444" ||
                cpf == "55555555555" ||
                cpf == "66666666666" ||
                cpf == "77777777777" ||
                cpf == "88888888888" ||
                cpf == "99999999999")
                return false;

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            
            string temp;
            string digit;
            
            int sum;
            int rest;
            
            if (cpf.Length != 11) 
                return false;
            
            temp = cpf.Substring(0, 9); sum = 0;
            
            for (int i = 0; i < 9; i++)
                sum += int.Parse(temp[i].ToString()) * multiplier1[i]; 
            
            rest = sum % 11;
            
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            
            digit = rest.ToString();
            
            temp = temp + digit;
            
            sum = 0;
            
            for (int i = 0; i < 10; i++)
                sum += int.Parse(temp[i].ToString()) * multiplier2[i];
            
            rest = sum % 11;
            
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            
            digit = digit + rest.ToString();
            
            return cpf.EndsWith(digit);
        }
    }
}
