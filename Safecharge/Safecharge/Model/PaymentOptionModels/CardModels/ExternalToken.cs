using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.CardModels
{
    /// <summary>
    /// Holder for external token data.
    /// </summary>
    /// <remarks>
    /// This class is set under the card class and is to be used if you wish to submit a payment with an external token provider as the card input.
    /// </remarks>
    public class ExternalToken
    {
        private string externalTokenProvider;
        private string mobileToken;

        public string ExternalTokenProvider
        {
            get { return this.externalTokenProvider; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.ExternalTokenProvider));
                this.externalTokenProvider = value;
            }
        }

        public string MobileToken
        {
            get { return this.mobileToken; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 5000, nameof(this.MobileToken));
                this.mobileToken = value;
            }
        }
    }
}
