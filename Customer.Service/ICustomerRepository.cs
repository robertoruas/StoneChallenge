using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Repository
{
    public interface ICustomerRepository
    {
        List<Domain.Customer> GetCustomers();

        Domain.Customer GetCustomer(string id);

        Domain.Customer GetCustomer(long cpf);

        Domain.Customer Create(Domain.Customer customer);

        void Update(string id, Domain.Customer customer);
    }
}
