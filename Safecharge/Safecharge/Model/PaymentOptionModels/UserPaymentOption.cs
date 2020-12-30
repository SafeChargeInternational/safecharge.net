using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels
{
    /// <summary>
    /// Holder for User Payment Option(UPO) data.
    /// </summary>
    public class UserPaymentOption : BasePaymentOption
    {
        private string cvv;

        public string CVV
        {
            get { return this.cvv; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 4, nameof(this.CVV));
                this.cvv = value;
            }
        }
    }
}
