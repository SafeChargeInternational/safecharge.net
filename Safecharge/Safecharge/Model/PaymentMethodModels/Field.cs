using System.Collections.Generic;

namespace Safecharge.Model.PaymentMethodModels
{
    /// <summary>
    /// Holder for merchant's specific info. For example: order id, account name, etc. in the Merchant's ERP system.
    /// The data in the field can be validated by regular expression.
    /// </summary>
    public class Field
    {
        public string Name { get; set; }

        public string Regex { get; set; }

        public string Type { get; set; }

        public List<LocalizationMessage> ValidationMessage { get; set; }

        public List<LocalizationMessage> Caption { get; set; }

        public List<APMDetailes> ListValues { get; set; }
    }
}
