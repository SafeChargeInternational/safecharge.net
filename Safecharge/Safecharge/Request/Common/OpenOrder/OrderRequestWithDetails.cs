using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Utils.Enum;

namespace Safecharge.Request.Common.OpenOrder
{
    /// <summary>
    /// Abstract class with basic fields used with requests to create an order in the SafeCharge's system.
    /// </summary>
    /// <remarks>
    /// This request represents the state of the order when it is created, it can be changed at later time.
    /// Note that no payment request is send, it is used mainly to store the order details at the time of creation.
    /// </remarks>
    public abstract class OrderRequestWithDetails : SafechargeOrderDetailsRequest
    {
        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public OrderRequestWithDetails()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRequestWithDetails"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="checksumOrderMapping">Type of checksum.</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        public OrderRequestWithDetails(
            MerchantInfo merchantInfo,
            ChecksumOrderMapping checksumOrderMapping,
            string sessionToken,
            string currency,
            string amount)
            : base(merchantInfo, checksumOrderMapping, sessionToken, currency, amount)
        {
        }

        public string PaymentMethod { get; set; }

        public UserPaymentOption UserPaymentOption { get; set; }

        public string CustomData { get; set; }

        public string IsMoto { get; set; }
    }
}
