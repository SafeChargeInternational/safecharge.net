using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Request.Common.Payment;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>This request is used when wanting to perform a 3d secure only request. </summary>
    /// <remarks>
    /// <para>
    /// It is used after an <see cref="InitPaymentRequest"/> to provide to the merchant information whether a Challenge is needed or if they receive a frictionless response.
    /// </para>
    /// <para>
    /// Note that Authorize3d is virtually the same as the Payment request(has the same fields) but is executed against a different REST endpoint.
    /// </para>
    /// </remarks>
    public class Authorize3dRequest : Authorize3dAndPaymentRequest
    {
        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public Authorize3dRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authorize3dRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="paymentOption">Details about the payment method.</param>
        /// <param name="relatedTransactionId">The ID of the original transaction.</param>
        public Authorize3dRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string currency,
            string amount,
            PaymentOption paymentOption,
            string relatedTransactionId)
            : base(merchantInfo, ChecksumOrderMapping.ApiGenericChecksumMapping, sessionToken, currency, amount, paymentOption)
        {
            this.RelatedTransactionId = relatedTransactionId;
            this.RequestUri = this.CreateRequestUri(ApiConstants.Authorize3dUrl);
        }
    }
}
