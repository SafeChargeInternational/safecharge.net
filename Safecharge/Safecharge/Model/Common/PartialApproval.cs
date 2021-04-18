using System;
using System.Collections.Generic;
using System.Text;

namespace Safecharge.Model.Common
{
    public class PartialApproval
    {
        public string RequestedAmount { get; set; }
        public string RequestedCurrency { get; set; }
        public string ProcessedAmount { get; set; }
        public string ProcessedCurrency { get; set; }
    }
}
