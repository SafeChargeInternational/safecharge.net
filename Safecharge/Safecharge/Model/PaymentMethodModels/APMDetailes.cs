using System;
using System.Collections.Generic;
using System.Text;

namespace Safecharge.Model.PaymentMethodModels
{
    public class APMDetailes
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Caption { get; set; }
        public List<ListValue> ListValues { get; set; }
    }
}
