using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Common.Util.Test
{
    public class CPFValidationAttributeIsValid
    {

        [Theory(DisplayName = "When parameter string is a valid CPF.")]
        [InlineData("39204379855")]
        [InlineData("03398767516")]
        public void WhenStringIsAValidCPF(string cpf)
        {
            bool isValid = new CPFValidationAttribute().IsValid(cpf);

            Assert.True(isValid);
        }

        [Theory(DisplayName ="When parameter string is not valid CPF.")]
        [InlineData("12345678900")]
        [InlineData("123456789")]
        [InlineData("11111111111")]
        [InlineData("")]
        public void WhenStringIsNotValidCPF(string cpf)
        {
            bool isValid = new CPFValidationAttribute().IsValid(cpf);

            Assert.False(isValid);
        }

    }
}
