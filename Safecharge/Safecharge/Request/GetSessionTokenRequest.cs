using Safecharge.Model.Common;
using Safecharge.Request.Common;
using Safecharge.Utils;

namespace Safecharge.Request
{
    /// <summary>
    /// Request to obtain a session token.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This request returns a unique session token. Almost all Safecharge's requests require a valid session token.
    /// The unique token is created upon the initial successful authorization and represents the client session.
    /// It also contains an expiration date, as well as information about user granted privileges.
    /// For subsequent calls to the session, the token must be provided for validation to ensure that it is still active and valid.
    /// </para>
    /// <para>Note that most of the payment requests consume the token, so it can be used for one payment only.</para>
    /// </remarks>
    public class GetSessionTokenRequest : SafechargeRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSessionTokenRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        public GetSessionTokenRequest(MerchantInfo merchantInfo)
            : base(merchantInfo, Utils.Enum.ChecksumOrderMapping.ApiBasicChecksumMapping)
        {
            this.RequestUri = this.CreateRequestUri(ApiConstants.GetSessionTokenUrl);
        }
    }
}
