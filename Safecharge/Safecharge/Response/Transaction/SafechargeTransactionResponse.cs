using Safecharge.Response.Common;

namespace Safecharge.Response.Transaction
{
    /// <summary>
    /// This is the unified response for the Settle, Void and Refund transaction requests.
    /// </summary>
    public class SafechargeTransactionResponse : SafechargeResponse
    {
        /// <summary>
        /// The transaction ID of the transaction for future actions.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// The transaction ID of the transaction in the event that an external service is used.
        /// </summary>
        public string ExternalTransactionId { get; set; }

        /// <summary>
        /// The UPO ID used for the transaction.
        /// </summary>
        public long UserPaymentOptionId { get; set; }

        /// <summary>
        /// If an error occurred on the APM side, an error code is returned in this parameter.
        /// </summary>
        public int PaymentMethodErrorCode { get; set; }

        /// <summary>
        /// If an error occurred on the APM side, an error reason is returned in this parameter.
        /// </summary>
        public string PaymentMethodErrorReason { get; set; }

        /// <summary>
        /// If an error occurred in the Gateway, then an error code is returned in this parameter.
        /// </summary>
        public int GwErrorCode { get; set; }

        /// <summary>
        /// If an error occurred in the gateway, then an error reason is returned in this parameter.
        /// (E.g. failure in checksum validation, timeout from processing engine, etc.)
        /// </summary>
        public string GwErrorReason { get; set; }

        /// <summary>
        /// Error code if error occurred on the bank’s side.
        /// When a transaction is successful, this field is 0.
        /// When a transaction is not successful, the parameter is the code of the generic error.
        /// </summary>
        public int GwExtendedErrorCode { get; set; }

        /// <summary>
        /// The gateway transaction status. Possible values:
        /// <list type="bullet">
        /// <item>APPROVED</item>
        /// <item>DECLINED</item>
        /// <item>ERROR</item>
        /// </list>
        /// </summary>
        public string TransactionStatus { get; set; }

        /// <summary>
        /// The authorization code of the related auth transaction, to be compared to the original one.
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// The 3D secure request data for the card issuer/bank.
        /// </summary>
        public string PaRequest { get; set; }

        /// <summary>
        /// The URL used by the merchant to redirect consumers to the payment method for authentication and authorization of the transaction.
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The Electronic Commerce Indicator (ECI) is returned from banks and indicates whether the attempted transaction
        /// passed as full 3D or failed.
        /// </summary>
        public string Eci { get; set; }

        /// <summary>
        /// If the attempt for 3D transaction failed - this parameter is returned by the banks to indicate the reason why
        /// the transaction was not passed as full 3D.
        /// </summary>
        public string ThreeDReason { get; set; }
    }
}
