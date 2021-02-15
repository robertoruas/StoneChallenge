using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageCalculation.Service.Interface
{
    public interface IConfigHelper
    {
        string CustomerUrl { get; set; }

        string ChargeUrl { get; set; }

        int MaxThreads { get; set; }
    }
}
