using Safecharge.Model.Common;
using Safecharge.Request.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    /// Request to get payment's status.
    /// </summary>
    public class GetPaymentStatusRequest : SafechargeRequest
    {
        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public GetPaymentStatusRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentStatusRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        public GetPaymentStatusRequest(
            MerchantInfo merchantInfo,
            string sessionToken)
            : base(merchantInfo, ChecksumOrderMapping.NoChecksumMapping, sessionToken)
        {
            this.RequestUri = this.CreateRequestUri(ApiConstants.GetPaymentStatusUrl);
        }
    }
}
