using Safecharge.Response.Common;

namespace Safecharge.Response
{
    /// <summary>
    /// Response received from the SafeCharge's servers to the <see cref="Request.GetPaymentStatusRequest"/>.
    /// </summary>
    public class GetPaymentStatusResponse : SafechargeResponse
    {
        public string CustomData { get; set; }

        public int GwExtendedErrorCode { get; set; }

        public int GwErrorCode { get; set; }

        public string GwErrorReason { get; set; }

        public int PaymentMethodErrorCode { get; set; }

        public string PaymentMethodErrorReason { get; set; }

        public string AuthCode { get; set; }

        public string TransactionType { get; set; }

        public string TransactionStatus { get; set; }

        public string UserTokenId { get; set; }

        public string TransactionId { get; set; }
    }
}
