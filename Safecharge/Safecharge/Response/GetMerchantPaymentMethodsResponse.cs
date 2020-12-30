using System.Collections.Generic;
using Safecharge.Model.PaymentMethodModels;
using Safecharge.Response.Common;

namespace Safecharge.Response
{
    /// <summary>
    /// Response received from the SafeCharge's servers to the <see cref="Request.GetMerchantPaymentMethodsRequest"/>.
    /// </summary>
    public class GetMerchantPaymentMethodsResponse : SafechargeResponse
    {
        /// <summary>
        /// List of {@link PaymentMethod} objects containing the available payment options per the GetMerchantPaymentMethodsRequest
        /// </summary>
        public List<PaymentMethodModel> PaymentMethods { get; set; }
    }
}
