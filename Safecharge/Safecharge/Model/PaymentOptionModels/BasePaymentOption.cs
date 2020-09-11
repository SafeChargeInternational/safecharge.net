using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels
{
    /// <summary>
    /// Base class for PaymentOption.
    /// </summary>
    public class BasePaymentOption
    {
        private string userPaymentOptionId;

        public string UserPaymentOptionId
        {
            get { return this.userPaymentOptionId; }
            set
            {
                Guard.RequiresNotNull(value, nameof(UserPaymentOptionId));
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringId, nameof(this.UserPaymentOptionId));
                this.userPaymentOptionId = value;
            }
        }
    }
}
