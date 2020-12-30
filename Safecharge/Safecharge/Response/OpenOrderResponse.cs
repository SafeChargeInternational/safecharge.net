using Safecharge.Response.Common;

namespace Safecharge.Response
{
    /// <summary>
    /// Response received from the SafeCharge's servers to the <see cref="Request.OpenOrderRequest"/>.
    /// </summary>
    public class OpenOrderResponse : SafechargeResponse
    {
        /// <summary>
        /// Merchant Order ID to be used as input parameter in update method and payment methods. The parameter passed to define which merchant order to update.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// ID of the user in merchant system.
        /// </summary>
        public string UserTokenId { get; set; }
    }
}
