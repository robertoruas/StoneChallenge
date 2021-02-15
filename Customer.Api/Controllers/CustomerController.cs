using Common.Util;
using Customer.Api.Models;
using Customer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Customer.Api.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerService;

        public CustomerController(ICustomerRepository service)
        {
            _customerService = service;
        }

        /// <summary>
        /// Get a Customer by CPF
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET /Customer?cpf=12345678900
        /// 
        /// </remarks>
        /// <param name="cpf">Customer CPF</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get([FromQuery] string cpf)
        {
            try
            {
                if (Validations.ValidateCPF(cpf))
                {
                    long searchValue = Convert.ToInt64(Regex.Replace(cpf, "[^0-9]+", ""));

                    Domain.Customer customer = _customerService.GetCustomer(searchValue);

                    if (customer == null)
                        return NotFound($"Não foi possivel encontrar o cliente com o CPF {cpf}.");

                    return Ok(customer);
                }
                else
                    return BadRequest("Informe um CPF válido para buscar o cliente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Return all customers registered
        /// </summary>
        /// <returns>Customer list registered in database</returns>
        [HttpGet]
        [Route("GetCustomers")]
        public ActionResult GetCustomers()
        {
            try
            {
                return Ok(_customerService.GetCustomers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Create a new Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Customer
        ///     {
        ///         "Name": "João Silva",
        ///         "State": "SP",
        ///         "CPF": "123.456.789-00"
        ///     }
        ///     
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Created Customer</returns>
        /// <response code="200">Customer Created Successfully</response>
        /// <response code="400">One or more validation errors occurred</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post(CustomerModel model)
        {
            TryValidateModel(model);

            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Customer customer = _customerService.GetCustomer(model.ParseCPFToInt());
                    if (customer == null)
                    {
                        customer = new Domain.Customer(model.Name, model.State, model.CPF);

                        _customerService.Create(customer);

                        return Ok(customer);
                    }
                    else
                        return Conflict(customer);

                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
