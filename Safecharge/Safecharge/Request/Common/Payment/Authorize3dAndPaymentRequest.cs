using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Utils.Enum;

namespace Safecharge.Request.Common.Payment
{
    /// <summary>
    /// Abstract class to be used as a base for payment and authorize 3d requests.
    /// </summary>
    public abstract class Authorize3dAndPaymentRequest : SafechargePaymentRequest
    {
        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public Authorize3dAndPaymentRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authorize3dAndPaymentRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="checksumOrderMapping">Type of checksum.</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="paymentOption">Details about the payment method.</param>
        public Authorize3dAndPaymentRequest(
            MerchantInfo merchantInfo,
            ChecksumOrderMapping checksumOrderMapping,
            string sessionToken,
            string currency,
            string amount,
            PaymentOption paymentOption)
            : base(merchantInfo, checksumOrderMapping, sessionToken, currency, amount)
        {
            this.PaymentOption = paymentOption;
        }

        public PaymentOption PaymentOption { get; set; }

        public int IsRebilling { get; set; }

        public bool AutoPayment3D { get; set; }
    }
}
