using Safecharge.Model.Common;
using Safecharge.Response.Common;
using Safecharge.Response.Payment;

namespace Safecharge.Response
{
    /// <summary>
    /// Response received from the SafeCharge's servers to the <see cref="Request.Verify3dRequest"/>.
    /// </summary>
    public class Verify3dResponse : SafechargeResponse
    {
        public string OrderId { get; set; }

        public string UserTokenId { get; set; }

        public string TransactionStatus { get; set; }

        public string TransactionType { get; set; }

        public string TransactionId { get; set; }

        public string CustomData { get; set; }

        public MerchantDetails MerchantDetails { get; set; }

        public PaymentOptionResponse PaymentOption { get; set; }

        public int GwErrorCode { get; set; }

        public string GwErrorReason { get; set; }

        public int GwExtendedErrorCode { get; set; }
    }
}
