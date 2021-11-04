using System;
using System.Collections.Generic;
using System.Text;

namespace Safecharge.Model.PaymentMethodModels
{
    public class APMDetailes
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<LocalizationMessage> Caption { get; set; }
        public List<ListValue> ListValues { get; set; }
    }
}
