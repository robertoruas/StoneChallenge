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
    public class ChargeHelperService : IChargeHelperService
    {
        private readonly string _chargeUrl;

        public ChargeHelperService(IConfigHelper configHelper)
        {
            _chargeUrl = configHelper.ChargeUrl;
        }

        public List<Charge> GetCharges(DateTime date)
        {
            try
            {
                var client = new RestClient(_chargeUrl);
                var request = new RestRequest(Method.GET);

                request.AddParameter("referenceDate", date.ToString("yyyy-MM-dd"));

                IRestResponse response = client.Execute(request);

                List<Charge> charges = JsonConvert.DeserializeObject<List<Charge>>(response.Content);

                return charges;

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível carregar a lista de cobranças. ", ex);
            }
        }

        public void RegisterCharge(Charge charge)
        {
            var client = new RestClient(_chargeUrl);
            var request = new RestRequest(Method.POST);

            request.AddJsonBody(new { value = charge.ChargeValue, cpf = charge.CPFFormatted(), dueDate = charge.DueDate});

            IRestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);

        }
    }
}
