using Customer.Api.Controllers;
using Customer.Api.Models;
using Customer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Customer.Test
{
    public class CustomerControllerPost
    {
        // Requisição Valida
        [Fact(DisplayName = "When customer json object is valid")]        
        public void WhenCustomerJsonObjectIsValid()
        {
            var jsonRequest = new CustomerModel() { Name = "Integration Test", CPF = "39204379855", State = "SP" };

            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.GetCustomer(It.IsAny<long>())).Returns((Domain.Customer)null);
            
            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));


            var controller = new CustomerController(repository.Object);
            
            controller.ObjectValidator = objectValidator.Object;

            var response = controller.Post(jsonRequest);

            Assert.IsType<OkObjectResult>(response);
        }

        // Requisição com Cliente já cadastrado
        [Fact(DisplayName = "When customer send already registered")]
        public void WhenCustomerSendAlreadyRegistered()
        {
            var jsonRequest = new CustomerModel() { Name = "Integration Test", CPF = "39204379855", State = "SP" };

            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.GetCustomer(It.IsAny<long>())).Returns(() => new Domain.Customer("Integration Test", "SP", "39204379855"));


            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));


            var controller = new CustomerController(repository.Object);

            controller.ObjectValidator = objectValidator.Object;

            var response = controller.Post(jsonRequest);

            Assert.IsType<ConflictObjectResult>(response);
        }

        // Requisição com exceção
        [Fact(DisplayName = "When customer json object throw exception")]
        public void WhenCustomerJsonObjectThrowExceoption()
        {
            var jsonRequest = new CustomerModel() { Name = "Integration Test", CPF = "39204379855", State = "SP" };

            var repository = new Mock<ICustomerRepository>();

            repository.Setup(r => r.Create(It.IsAny<Domain.Customer>())).Throws(new Exception());

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));


            var controller = new CustomerController(repository.Object);

            controller.ObjectValidator = objectValidator.Object;

            var response = controller.Post(jsonRequest);

            Assert.IsType<ObjectResult>(response);

            var statusCodeResponse = (response as ObjectResult).StatusCode;

            Assert.Equal(500, statusCodeResponse);
        }
    }
}
