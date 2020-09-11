using Safecharge.Model.Common;
using Safecharge.Request.Common.Transaction;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    /// Request to refund a transaction.
    /// </summary>
    /// <remarks>
    /// This request can be used to refund a previously settled transaction. Full or partial refunds are supported.
    /// When partial refunds are issued, multiple refund requests can be performed for up to the entire amount
    /// of the original settled transaction.
    /// </remarks>
    public class RefundTransactionRequest : SafechargeTransactionRequest
    {
        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public RefundTransactionRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RefundTransactionRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="relatedTransactionId">The ID of the original transaction.</param>
        public RefundTransactionRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string currency,
            string amount,
            string relatedTransactionId)
            : base(merchantInfo, ChecksumOrderMapping.RefundGwTransactionChecksumMapping, sessionToken, currency, amount, relatedTransactionId)
        {
            this.RequestUri = this.CreateRequestUri(ApiConstants.RefundTransactionUrl);
        }
    }
}
