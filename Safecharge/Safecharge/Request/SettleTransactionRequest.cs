using Safecharge.Model.Common;
using Safecharge.Request.Common.Transaction;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    ///  Request to settle a transaction.
    /// </summary>
    /// <remarks>
    /// This request is used for settling an authorization transaction that was previously performed, with a two-phase
    ///  auth-settle process, for either a full or partial settles.When partial settles are issued – multiple settle requests
    ///  can be performed for up to the entire amount of the original authorized transaction.
    ///  * Payment request before this request should be called with TransactionType "Auth".
    /// </remarks>
    public class SettleTransactionRequest : SafechargeTransactionRequest
    {
        private string descriptorMerchantName;
        private string descriptorMerchantPhone;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public SettleTransactionRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettleTransactionRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="relatedTransactionId">The ID of the original transaction.</param>
        public SettleTransactionRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string currency,
            string amount,
            string relatedTransactionId)
            : base(merchantInfo, ChecksumOrderMapping.SettleGwTransactionChecksumMapping, sessionToken, currency, amount, relatedTransactionId)
        {
            this.RequestUri = this.CreateRequestUri(ApiConstants.SettleTransactionUrl);
        }

        /// <summary>
        /// The name that will appear in the payment statement
        /// </summary>
        public string DescriptorMerchantName
        {
            get { return this.descriptorMerchantName; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 25, nameof(this.DescriptorMerchantName));
                this.descriptorMerchantName = value;
            }
        }

        /// <summary>
        /// The phone that will appear in the payment statement
        /// </summary>
        public string DescriptorMerchantPhone
        {
            get { return this.descriptorMerchantPhone; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 13, nameof(this.DescriptorMerchantPhone));
                this.descriptorMerchantPhone = value;
            }
        }
    }
}
