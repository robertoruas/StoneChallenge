using Customer.Api.Controllers;
using Customer.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Customer.Test
{
    public class CustomerControllerGetCustomers
    {
        [Fact(DisplayName = "When execution return a list")]        
        public void WhenExecutionReturnAList()
        {
            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.GetCustomers()).Returns(() => new List<Domain.Customer>() { new Domain.Customer("Integrated Test", "SP", "39204379855") });

            var controller = new CustomerController(repository.Object);

            var response = controller.GetCustomers();

            Assert.IsType<OkObjectResult>(response);

        }

        [Fact(DisplayName = "When execution return a empty list")]
        public void WhenExecutionReturnAEmptyList()
        {
            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.GetCustomers()).Returns(() => new List<Domain.Customer>());

            var controller = new CustomerController(repository.Object);

            var response = controller.GetCustomers();
            
            Assert.IsType<OkObjectResult>(response);

            var okResult = response as OkObjectResult;

            var modelResponse = okResult.Value as List<Domain.Customer>;

            Assert.True(modelResponse.Count == 0);
        }

        [Fact(DisplayName = "When occurre any problem on execution")]
        public void WhenOccurreAnyProblemOnExecution()
        {
            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.GetCustomers()).Throws(new Exception());

            var controller = new CustomerController(repository.Object);

            var response = controller.GetCustomers();

            Assert.IsType<ObjectResult>(response);

            var statusCodeResponse = (response as ObjectResult).StatusCode;
            
            Assert.Equal(500, statusCodeResponse);
        }
    }
}
