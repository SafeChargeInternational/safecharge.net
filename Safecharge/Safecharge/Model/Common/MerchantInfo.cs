using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Model.Common
{
    /// <summary>
    /// Holder for the required for a payment merchant's data such as 
    /// secret key, the merchant id, the merchant site id, etc.
    /// </summary>
    public class MerchantInfo
    {
        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public MerchantInfo()
        {
        }

        /// <summary>
        /// Creates an object with data from the params.
        /// </summary>
        /// <param name="merchantKey">The secret merchant key obtained by the Merchant during integration process with Safecharge</param>
        /// <param name="merchantId">Merchant id in the Safecharge's system</param>
        /// <param name="merchantSiteId">Merchant site id in the Safecharge's system</param>
        /// <param name="serverHost">The Safecharge's server address to send the request to</param>
        /// <param name="hashAlgorithm">The hashing algorithm used to generate the checksum</param>
        public MerchantInfo(string merchantKey, string merchantId, string merchantSiteId, string serverHost, HashAlgorithmType hashAlgorithm)
        {
            Guard.RequiresNotNull(merchantKey, nameof(merchantKey));
            Guard.RequiresNotNull(merchantId, nameof(merchantId));
            Guard.RequiresNotNull(merchantSiteId, nameof(merchantSiteId));
            Guard.RequiresNotNull(serverHost, nameof(serverHost));

            this.MerchantKey = merchantKey;
            this.MerchantId = merchantId;
            this.MerchantSiteId = merchantSiteId;
            this.ServerHost = serverHost;
            this.HashAlgorithm = hashAlgorithm;
        }

        public string MerchantKey { get; set; }

        public string MerchantId { get; set; }

        public string MerchantSiteId { get; set; }

        public string ServerHost { get; set; }

        public HashAlgorithmType HashAlgorithm { get; set; }
    }
}
