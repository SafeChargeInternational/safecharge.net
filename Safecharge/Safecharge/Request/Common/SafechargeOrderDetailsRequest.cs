using System.Collections.Generic;
using Safecharge.Model.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request.Common
{
    /// <summary>
    /// Abstract class to be used as a base for order related and payment requests.
    /// </summary>
    public abstract class SafechargeOrderDetailsRequest : SafechargeRequest
    {
        private string userTokenId;
        private string clientUniqueId;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public SafechargeOrderDetailsRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafechargeOrderDetailsRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="checksumOrderMapping">Type of checksum.</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        public SafechargeOrderDetailsRequest(
            MerchantInfo merchantInfo,
            ChecksumOrderMapping checksumOrderMapping,
            string sessionToken,
            string currency,
            string amount)
            : base(merchantInfo, checksumOrderMapping, sessionToken)
        {
            this.Currency = currency;
            this.Amount = amount;
        }

        /// <summary>
        /// The three character ISO currency code.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The transaction amount.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// List of items that will be purchased.
        /// </summary>
        public List<Item> Items { get; set; } = new List<Item>();

        /// <summary>
        /// Details about the user which include the user's name, email, address, etc.
        /// </summary>
        public CashierUserDetails UserDetails { get; set; }

        /// <summary>
        /// Shipping address related to a user's order.
        /// </summary>
        public UserAddress ShippingAddress { get; set; }

        /// <summary>
        /// Billing address related to a user payment option. Since order can contain only one payment option billing address is part of the order parameters.
        /// </summary>
        public UserAddress BillingAddress { get; set; }

        /// <summary>
        /// Merchant descriptor - this is the message(Merchant's name and phone) that the user will see in his payment bank report
        /// </summary>
        public DynamicDescriptor DynamicDescriptor { get; set; }

        /// <summary>
        /// Optional custom fields.
        /// </summary>
        public MerchantDetails MerchantDetails { get; set; }

        /// <summary>
        /// Although DMN response can be configured per merchant site, it will allow to dynamically return the DMN to the provided address per request.
        /// </summary>
        public UrlDetails UrlDetails { get; set; }

        /// <summary>
        /// ID of the user in merchant system.
        /// </summary>
        public string UserTokenId
        {
            get { return this.userTokenId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringId, nameof(this.UserTokenId));
                this.userTokenId = value;
            }
        }

        /// <summary>
        /// ID of the transaction in merchant system.
        /// </summary>
        public string ClientUniqueId
        {
            get { return this.clientUniqueId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringId, nameof(this.ClientUniqueId));
                this.clientUniqueId = value;
            }
        }

        public AmountDetails AmountDetails { get; set; }
    }
}
