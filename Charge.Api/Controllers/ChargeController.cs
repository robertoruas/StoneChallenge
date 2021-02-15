using Charge.Api.Models;
using Charge.Repository;
using Common.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Charge.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ChargeController : ControllerBase
    {
        private readonly IChargeRepository _chargeService;

        public ChargeController(IChargeRepository service)
        {
            _chargeService = service;
        }

        /// <summary>
        /// Register a charge to customer
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /Charge
        ///     {
        ///         "CPF":"12345678900",
        ///         "DueDate":"2021-02-07",
        ///         "Value": 1200.00
        ///     }
        /// </remarks>
        /// <response code="200">Charge Registered Successfully</response>
        /// <response code="400">One or more validation errors occurred</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult RegisterCharge(ChargeModel model)
        {
            TryValidateModel(model);

            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Charge charge = _chargeService.GetCharge(model.ParseCPFToInt(), model.DueDate.ToString("yyyy-MM-dd"));

                    if (charge != null)
                        return Conflict($"Já existe uma cobrança registrada para o mês {model.DueDate.Month} e ano {model.DueDate.Year}.");

                    charge = new Domain.Charge(model.ParseCPFToInt(), model.DueDate.Date, model.Value);

                    _chargeService.Create(charge);

                    return Ok(charge);
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


        /// <summary>
        /// Return a charges list by CPF and/or Reference Date
        /// </summary>
        /// <param name="cpf">Customer CPF</param>
        /// <param name="referenceDate">Reference Date (Format yyyy-MM-dd)</param>
        [HttpGet]
        public ActionResult GetCharge(string cpf, string referenceDate)
        {
            try
            {
                List<ChargeModel> result = new List<ChargeModel>();

                if (string.IsNullOrWhiteSpace(cpf) && string.IsNullOrWhiteSpace(referenceDate))
                    return BadRequest("Informe CPF ou Data de Referencia");

                if (!string.IsNullOrWhiteSpace(cpf) && !Validations.ValidateCPF(cpf))
                    return BadRequest("Informe um CPF válido.");

                DateTime validDate;

                if (!string.IsNullOrWhiteSpace(referenceDate) && !DateTime.TryParse(referenceDate, out validDate))
                    return BadRequest("Informe uma data válida (Formato yyyy-MM-dd).");

                long? searchCPF = null;

                if (!string.IsNullOrWhiteSpace(cpf))
                    searchCPF = Convert.ToInt64(Regex.Replace(cpf, "[^0-9]+", ""));

                var charges = _chargeService.GetCharges(searchCPF, referenceDate);

                charges.ForEach(c =>
                {
                    result.Add(new ChargeModel
                    {
                        CPF = c.CPFFormatted(),
                        DueDate = Convert.ToDateTime(c.DueDate),
                        Value = c.ChargeValue
                    });
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
