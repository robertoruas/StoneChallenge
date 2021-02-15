using System;
using System.Linq;
using System.Threading.Tasks;
using UsageCalculation.Domain;
using UsageCalculation.Service.Interface;

namespace UsageCalculation.Service
{
    public class WorkerService : IWorkerService
    {
        private readonly ICustomerHelperService _customerHelperService;
        private readonly IChargeHelperService _chargeHelperService;
        private readonly int _maxThreads;

        public WorkerService(ICustomerHelperService customerHelperService, IChargeHelperService chargeHelperService, IConfigHelper configHelper)
        {
            _customerHelperService = customerHelperService;
            _chargeHelperService = chargeHelperService;
            _maxThreads = configHelper.MaxThreads;
        }

        public void Execute()
        {
            var customers = _customerHelperService.GetCustomers();

            var charges = _chargeHelperService.GetCharges(DateTime.Now.Date);

            Parallel.ForEach(customers, new ParallelOptions { MaxDegreeOfParallelism = _maxThreads },
                customer =>
                {
                    if(!charges.Any(c => c.CPF == customer.CPF))
                    {
                        decimal value = customer.CalculateUsage();

                        Charge charge = new Charge(DateTime.Now.Date, value, customer.CPF);

                        _chargeHelperService.RegisterCharge(charge);                        
                    }
                });           

        }
    }
}
