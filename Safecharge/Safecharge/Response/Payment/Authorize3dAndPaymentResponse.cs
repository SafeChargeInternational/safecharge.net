using Safecharge.Model.Common;
using Safecharge.Model.FraudModels;
using Safecharge.Response.Common;

namespace Safecharge.Response.Payment
{
    /// <summary>
    /// Response received from the Safecharge's servers to the <see cref="Request.Common.Payment.Authorize3dAndPaymentRequest"/>
    /// </summary>
    public class Authorize3dAndPaymentResponse : SafechargeResponse
    {
        public string OrderId { get; set; }

        public string UserTokenId { get; set; }

        public PaymentOptionResponse PaymentOption { get; set; }

        public string TransactionStatus { get; set; }

        public MerchantDetails MerchantDetails { get; set; }

        public int GwErrorCode { get; set; }

        public string GwErrorReason { get; set; }

        public int GwExtendedErrorCode { get; set; }

        public string PaymentMethodErrorCode { get; set; }

        public string PaymentMethodErrorReason { get; set; }

        public string TransactionType { get; set; }

        public string TransactionId { get; set; }

        public string ExternalTransactionId { get; set; }

        public string AuthCode { get; set; }

        public string CustomData { get; set; }

        public FraudDetails FraudDetails { get; set; }
    }
}
