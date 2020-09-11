using Safecharge.Model.Common;
using Safecharge.Model.Common.Addendum;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request.Common
{
    /// <summary>
    /// Abstract class to be used as a base for all of the requests to SafeCharge's servers.
    /// </summary>
    public abstract class SafechargeRequest : SafechargeBaseRequest
    {
        private string userId;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public SafechargeRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafechargeRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="checksumOrderMapping">Type of checksum.</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        public SafechargeRequest(
            MerchantInfo merchantInfo,
            ChecksumOrderMapping checksumOrderMapping,
            string sessionToken = null)
            : base(merchantInfo, checksumOrderMapping, sessionToken)
        {
            this.MerchantId = merchantInfo.MerchantId;
            this.MerchantSiteId = merchantInfo.MerchantSiteId;
        }

        public string MerchantId { get; set; }

        public string MerchantSiteId { get; set; }

        public string SourceApplication { get { return ApiConstants.SourceApplication;  } }

        public string RebillingType { get; set; }

        public string AuthenticationTypeOnly { get; set; }

        public SubMerchant SubMerchant { get; set; }

        /// <summary>
        /// This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.
        /// </summary>
        public Addendums Addendums { get; set; }

        public string UserId
        {
            get { return this.userId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.UserId));
                this.userId = value;
            }
        }

        /// <summary>
        /// The details for the device from which the transaction will be made.
        /// </summary>
        public DeviceDetails DeviceDetails { get; set; }
    }
}
