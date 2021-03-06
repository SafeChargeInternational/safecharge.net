﻿using Safecharge.Model.Common;
using Safecharge.Request.Common.Transaction;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    /// Request to void a transaction.
    /// </summary>
    /// <remarks>
    /// This request is used for voiding a previously performed authorization, within a two-phase auth-settle process.
    /// Please note that a void action is not always supported by the payment method or the card issuer.
    /// </remarks>
    public class VoidTransactionRequest : SafechargeTransactionRequest
    {
        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public VoidTransactionRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoidTransactionRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="relatedTransactionId">The ID of the original transaction.</param>
        public VoidTransactionRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string currency,
            string amount,
            string relatedTransactionId)
            : base(merchantInfo, ChecksumOrderMapping.VoidGwTransactionChecksumMapping, sessionToken, currency, amount, relatedTransactionId)
        {
            this.RequestUri = this.CreateRequestUri(ApiConstants.VoidTransactionUrl);
        }
    }
}
