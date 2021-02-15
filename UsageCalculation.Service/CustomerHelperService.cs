using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsageCalculation.Domain;
using UsageCalculation.Service.Interface;

namespace UsageCalculation.Service
{
    public class CustomerHelperService : ICustomerHelperService
    {

        private readonly string _customerUrl;

        public CustomerHelperService(IConfigHelper helper)
        {
            _customerUrl = helper.CustomerUrl;
        }

        public List<Customer> GetCustomers()
        {
            try
            {
                var client = new RestClient($"{_customerUrl}/GetCustomers");
                var request = new RestRequest(Method.GET);

                IRestResponse response = client.Execute(request);

                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(response.Content);

                return customers;

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível carregar a lista de clientes. ", ex);
            }
        }
    }
}
