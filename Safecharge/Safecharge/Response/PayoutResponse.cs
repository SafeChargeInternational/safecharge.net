using Safecharge.Model.Common;
using Safecharge.Response.Common;

namespace Safecharge.Response
{
    /// <summary>
    /// Response received from the SafeCharge's servers to the <see cref="Request.PayoutRequest"/>.
    /// </summary>
    public class PayoutResponse : SafechargeResponse
    {
        public string UserTokenId { get; set; }

        public string TransactionStatus { get; set; }

        public string PaymentMethodErrorCode { get; set; }

        public string PaymentMethodErrorReason { get; set; }

        public int GwErrorCode { get; set; }

        public string GwErrorReason { get; set; }

        public int GwExtendedErrorCode { get; set; }

        public string UserPaymentOptionId { get; set; }

        public string ExternalTransactionId { get; set; }

        public string TransactionId { get; set; }

        public MerchantDetails MerchantDetails { get; set; }
    }
}
