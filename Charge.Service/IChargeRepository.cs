using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charge.Repository
{
    public interface IChargeRepository
    {
        List<Domain.Charge> GetCharges();

        List<Domain.Charge> GetCharges(long? cpf, string dueDate);

        Domain.Charge GetCharge(string id);

        Domain.Charge GetCharge(long cpf, string dueDate);

        Domain.Charge Create(Domain.Charge charge);

        void Update(string id, Domain.Charge Charge);

    }
}
