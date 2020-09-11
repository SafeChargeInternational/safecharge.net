using System.Collections.Generic;
using Safecharge.Model.PaymentOptionModels.CardModels;

namespace Safecharge.Model.PaymentOptionModels
{
    /// <summary>
    /// Holdeer for payment option data.
    /// </summary>
    public class PaymentOption : BasePaymentOption
    {
        public Card Card { get; set; }

        public Dictionary<string, string> AlternativePaymentMethod { get; set; }

        public SubMethod Submethod { get; set; }
    }
}
