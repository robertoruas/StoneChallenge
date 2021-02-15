using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsageCalculation.Domain;

namespace UsageCalculation.Service.Interface
{
    public interface ICustomerHelperService
    {
        List<Customer> GetCustomers();
    }
}
