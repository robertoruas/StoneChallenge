using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsageCalculation.Service.Interface;

namespace UsageCalculation.Service
{
    public class ConfigHelper : IConfigHelper
    {
        public string CustomerUrl { get; set; }
        public string ChargeUrl { get; set; }
        public int MaxThreads { get; set; }
    }
}
