using Charge.Api.Controllers;
using Charge.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Charge.Test
{
    public class ChargeControllerGetCharge
    {
        [Theory(DisplayName = "When CPF parameter valid was send and Reference Date not")]
        [InlineData("39204379855", null )]
        public void WhenCPFParameterValidWasSendAndReferenceDateNot(string cpf, string referenceDate)
        {
            var repository = new Mock<IChargeRepository>();

            repository.Setup(r => r.GetCharges(It.IsAny<long?>(), It.IsAny<string>())).Returns(() => new List<Domain.Charge> { new Domain.Charge(39204379855, new DateTime(2021,02,15), 3955.00m)});

            var controller = new ChargeController(repository.Object);

            var response = controller.GetCharge(cpf, referenceDate);

            Assert.IsType<OkObjectResult>(response);
        }

        [Theory(DisplayName = "When CPF parameter and Reference Date valid was send")]
        [InlineData("39204379855", "2021-02-15")]
        public void WhenCPFParameterAndReferenceDatValidWasSend(string cpf, string referenceDate)
        {
            var repository = new Mock<IChargeRepository>();

            repository.Setup(r => r.GetCharges(It.IsAny<long?>(), It.IsAny<string>())).Returns(() => new List<Domain.Charge> { new Domain.Charge(39204379855, new DateTime(2021, 02, 15), 3955.00m) });

            var controller = new ChargeController(repository.Object);

            var response = controller.GetCharge(cpf, referenceDate);

            Assert.IsType<OkObjectResult>(response);
        }

        [Theory(DisplayName = "When CPF parameter not send and Reference Date valid was send")]
        [InlineData(null, "2021-02-15")]
        public void WhenCPFParameterNotSendAndReferenceDateValidWasSend(string cpf, string referenceDate)
        {
            var repository = new Mock<IChargeRepository>();

            repository.Setup(r => r.GetCharges(It.IsAny<long?>(), It.IsAny<string>())).Returns(() => new List<Domain.Charge> { new Domain.Charge(39204379855, new DateTime(2021, 02, 15), 3955.00m) });

            var controller = new ChargeController(repository.Object);

            var response = controller.GetCharge(cpf, referenceDate);

            Assert.IsType<OkObjectResult>(response);
        }

        [Theory(DisplayName = "When CPF parameter not valid was send and Reference Date not")]
        [InlineData("12345678900", null)]
        public void WhenCPFParameterNotValidWasSendAndReferenceDateNot(string cpf, string referenceDate)
        {
            var repository = new Mock<IChargeRepository>();

            var controller = new ChargeController(repository.Object);

            var response = controller.GetCharge(cpf, referenceDate);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory(DisplayName = "When CPF parameter not send and Reference Date not valid was send")]
        [InlineData(null, "TestDateInValid")]
        public void WhenCPFParameterNotSendAndReferenceDateNotValidWasSend(string cpf, string referenceDate)
        {
            var repository = new Mock<IChargeRepository>();

            var controller = new ChargeController(repository.Object);

            var response = controller.GetCharge(cpf, referenceDate);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory(DisplayName = "When CPF parameter not send and Reference Date ot send ")]
        [InlineData(null, null)]
        public void WhenCPFParameterNotSendAndReferenceDateNotSend(string cpf, string referenceDate)
        {
            var repository = new Mock<IChargeRepository>();

            var controller = new ChargeController(repository.Object);

            var response = controller.GetCharge(cpf, referenceDate);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory(DisplayName ="When execution throw exception")]
        [InlineData("39204379855", "2021-02-15")]

        public void WhenExecutionThrowException(string cpf, string referenceDate)
        {
            var repository = new Mock<IChargeRepository>();

            repository.Setup(r => r.GetCharges(It.IsAny<long>(), It.IsAny<string>())).Throws(new Exception());

            var controller = new ChargeController(repository.Object);

            var response = controller.GetCharge(cpf, referenceDate);

            Assert.IsType<ObjectResult>(response);

            var statusCodeResponse = (response as ObjectResult).StatusCode;

            Assert.Equal(500, statusCodeResponse);
        }
    }
}
