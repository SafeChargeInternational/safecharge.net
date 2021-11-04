using System;
using System.Collections.Generic;
using System.Text;

namespace Safecharge.Model.PaymentMethodModels
{
    public class ListValue
    {
        public string Code { get; set; }
        public string Caption { get; set; }
        public List<string> MandatoryFields { get; set; }
    }
}
