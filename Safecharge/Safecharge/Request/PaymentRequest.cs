using Safecharge.Model.Common;
using Safecharge.Model.PaymentModels;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Request.Common.Payment;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    /// Uniform request to perform card transactions(credit or debit)
    /// It supports 3D Secure and alternative payment method transactions.
    /// </summary>
    public class PaymentRequest : Authorize3dAndPaymentRequest
    {
        private string orderId;
        private string isPartialApproval;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public PaymentRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="paymentOption">Details about the payment method.</param>
        public PaymentRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string currency,
            string amount,
            PaymentOption paymentOption)
            : base(merchantInfo, ChecksumOrderMapping.ApiGenericChecksumMapping, sessionToken, currency, amount, paymentOption)
        {
            this.RequestUri = this.CreateRequestUri(ApiConstants.PaymentUrl);
        }

        public string OrderId
        {
            get { return this.orderId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringId, nameof(this.OrderId));
                this.orderId = value;
            }
        }

        public string IsMoto { get; set; }

        public SubMethodDetails SubMethodDetails { get; set; }

        /// <summary>
        /// This describes a situation where the deposit was completed and processed with 
        /// an amount lower than the requested amount due to a consumer’s lack of funds within the desired payment method.
        /// <remarks>
        /// </summary>
        /// Partial approval is only supported by Nuvei acquiring.For partial approval to be available for the merchant it should be configured by Nuvei’s Integration Support Team.
        /// Possible values:
        /// <list type="bullet">
        /// <item>1 – allow partial approval</item>
        /// <item>0 – not allow partial approval</item>
        /// </list>
        /// </remarks>
        public string IsPartialApproval
        {
            get { return this.isPartialApproval; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 1, nameof(this.IsPartialApproval));
                this.isPartialApproval = value;
            }
        }

        public CurrencyConversion CurrencyConversion { get; set; }
    }
}
