using System;
using Xunit;

namespace Common.Util.Test
{
    public class ValidationsValidateCPF
    {
        [Theory(DisplayName = "CPF is valid")]
        [InlineData("39204379855")]
        [InlineData("03398767516")]
        public void WhenCPFIsValid(string cpf)
        {
            var isvalid = Validations.ValidateCPF(cpf);

            Assert.True(isvalid);
        }

        [Theory(DisplayName = "CPF is not valid")]
        [InlineData("12345678900")]
        [InlineData("123456789")]
        [InlineData("11111111111")]
        public void WhenCPFIsNotValid(string cpf)
        {
            var isvalid = Validations.ValidateCPF(cpf);

            Assert.False(isvalid);
        }
    }
}
