using Customer.Api.Controllers;
using Customer.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace Customer.Test
{
    public class CustomerControllerGet
    {
        [Theory(DisplayName = "When parameter CPF is found")]
        [InlineData("39204379855")]
        [InlineData("03398767516")]
        public void WhenParameterCPFIsFound(string cpf)
        {
            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.GetCustomer(It.IsAny<long>())).Returns(() => new Domain.Customer("Integrated Test", "SP", cpf));

            var controller = new CustomerController(repository.Object);

            var response = controller.Get(cpf);

            Assert.IsType<OkObjectResult>(response);

        }

        [Theory(DisplayName = "When parameter CPF is not found")]
        [InlineData("89017479019")]
        public void WhenParameterCPFIsNotFound(string cpf)
        {
            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.GetCustomer(It.IsAny<long>())).Returns((Domain.Customer)null);

            var controller = new CustomerController(repository.Object);

            var response = controller.Get(cpf);

            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Theory(DisplayName = "When parameter CPF is not valid")]
        [InlineData("12345678900")]
        [InlineData("")]
        [InlineData("32276019000151")]
        public void WhenParameterCPFIsNotValid(string cpf)
        {
            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.GetCustomer(It.IsAny<long>())).Returns(() => new Domain.Customer("Integrated Test", "SP", cpf));

            var controller = new CustomerController(repository.Object);

            var response = controller.Get(cpf);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory(DisplayName = "When occurre any problem on execution")]
        [InlineData("39204379855")]
        public void WhenOccurreAnyProblemOnExecution(string cpf)
        {
            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.GetCustomer(It.IsAny<long>())).Throws(new Exception());

            var controller = new CustomerController(repository.Object);

            var response = controller.Get(cpf);

            Assert.IsType<ObjectResult>(response);

            var statusCodeResponse = (response as ObjectResult).StatusCode;

            Assert.Equal(500, statusCodeResponse);
        }
    }
}
