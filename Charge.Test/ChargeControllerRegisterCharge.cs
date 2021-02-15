using Charge.Api.Controllers;
using Charge.Api.Models;
using Charge.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Charge.Test
{
    public class ChargeControllerRegisterCharge
    {

        [Fact(DisplayName = "When charge json object is valid")]
        public void WhenChargeJsonObjectIsValid()
        {
            var jsonRequest = new ChargeModel() { CPF = "39204379855", DueDate = new DateTime(2021, 02, 15), Value = 3955.00m };

            var repository = new Mock<IChargeRepository>();

            repository.Setup(r => r.GetCharge(It.IsAny<long>(), It.IsAny<string>())).Returns((Domain.Charge) null);

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));

            var controller = new ChargeController(repository.Object);

            controller.ObjectValidator = objectValidator.Object;

            var response = controller.RegisterCharge(jsonRequest);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact(DisplayName = "When charge json object is valid and charge already exists")]
        public void WhenChargeJsonObjectIsValidAndChargeAlreadyExists()
        {
            var jsonRequest = new ChargeModel() { CPF = "39204379855", DueDate = new DateTime(2021, 02, 15), Value = 3955.00m };

            var repository = new Mock<IChargeRepository>();

            repository.Setup(r => r.GetCharge(It.IsAny<long>(), It.IsAny<string>())).Returns(() => new Domain.Charge(39204379855, new DateTime(2021, 02, 15), 3955.00m));

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));

            var controller = new ChargeController(repository.Object);

            controller.ObjectValidator = objectValidator.Object;

            var response = controller.RegisterCharge(jsonRequest);

            Assert.IsType<ConflictObjectResult>(response);
        }

        [Fact(DisplayName = "When charge json object throw exception")]
        public void WhenChargeJsonObjectThrowException()
        {
            var jsonRequest = new ChargeModel() { CPF = "39204379855", DueDate = new DateTime(2021, 02, 15), Value = 3955.00m };

            var repository = new Mock<IChargeRepository>();

            repository.Setup(r => r.GetCharge(It.IsAny<long>(), It.IsAny<string>())).Returns((Domain.Charge)null);

            repository.Setup(r => r.Create(It.IsAny<Domain.Charge>())).Throws(new Exception());

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));

            var controller = new ChargeController(repository.Object);

            controller.ObjectValidator = objectValidator.Object;

            var response = controller.RegisterCharge(jsonRequest);

            Assert.IsType<ObjectResult>(response);

            var statusCodeResponse = (response as ObjectResult).StatusCode;

            Assert.Equal(500, statusCodeResponse);

        }
    }
}
