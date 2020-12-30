using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels.OpenOrder;
using Safecharge.Request.Common.OpenOrder;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    /// Request to create an order in the SafeCharge's system.
    /// </summary>
    /// <remarks>
    /// This request represents the state of the order when it is created, it can be changed at later time.
    /// Note that no payment request is send, it is used mainly to store the order details at the time of creation.
    /// </remarks>
    public class OpenOrderRequest : OrderRequestWithDetails
    {
        private string productId;
        private string isRebilling;
        private string preventOverride;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public OpenOrderRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenOrderRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        public OpenOrderRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string currency,
            string amount)
            : base(merchantInfo, ChecksumOrderMapping.ApiGenericChecksumMapping, sessionToken, currency, amount)
        {
            this.RequestUri = this.CreateRequestUri(ApiConstants.OpenOrderUrl);
        }

        public string CustomSiteName { get; set; }

        public string ProductId
        {
            get { return this.productId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 50, nameof(this.ProductId));
                this.productId = value;
            }
        }

        public OpenOrderPaymentOption PaymentOption { get; set; }

        public string TransactionType { get; set; }

        public string IsRebilling
        {
            get { return this.isRebilling; }
            set
            {
                if (value != null)
                {
                    Guard.RequiresBool(value, nameof(this.IsRebilling));
                }

                this.isRebilling = value;
            }
        }

        public string PreventOverride
        {
            get { return this.preventOverride; }
            set
            {
                if (value != null)
                {
                    Guard.RequiresBool(value, nameof(this.PreventOverride));
                }

                this.preventOverride = value;
            }
        }
    }
}
