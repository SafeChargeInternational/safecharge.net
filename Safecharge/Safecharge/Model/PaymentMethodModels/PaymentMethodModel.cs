using System.Collections.Generic;

namespace Safecharge.Model.PaymentMethodModels
{
    /// <summary>
    /// Holder for Payment method(PM) data on Checkout Page.
    /// Along with PM's specific info it contains a list of allowed countries and currencies ISO codes for use with the PM.
    /// </summary>
    public class PaymentMethodModel
    {
        public string PaymentMethod { get; set; }

        public string IsDirect { get; set; }

        public List<string> Countries { get; set; }

        public List<string> Currencies { get; set; }

        public string LogoURL { get; set; }

        public List<LocalizationMessage> PaymentMethodDisplayName { get; set; }

        public List<Field> Fields { get; set; }
    }
}
