using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Model.PaymentOptionModels.CardModels;
using Safecharge.Request.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    /// Request to execute payout.
    /// </summary>
    /// <remarks>
    ///  This method is intended for merchants implementing payout for Credit cards and APMs(paypal, skrill, neteller and so on).
    ///  Using a native mobile application is not required to use the payout client SDK.
    ///  However, they are able to use this <see cref="PayoutRequest"/> directly.
    ///  Merchants are able to use the payout client SDK, according to the instructions<a href="https://www.safecharge.com/docs/api/?java#payout"> here</a>, 
    ///  which performs credit card tokenization for them.
    ///  See <a href="https://www.safecharge.com/docs/api/?java#payout">documentation</a>.
    /// </remarks>
    public class PayoutRequest : SafechargeRequest
    {
        private string userTokenId;
        private string clientUniqueId;
        private string amount;
        private string currency;
        private string comment;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public PayoutRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayoutRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="userTokenId">ID of the user in merchant system.</param>
        /// <<param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="userPaymentOption">User payment option data.</param>
        public PayoutRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string userTokenId,
            string clientUniqueId,
            string amount,
            string currency,
            UserPaymentOption userPaymentOption)
            : base(merchantInfo, ChecksumOrderMapping.ApiGenericChecksumMapping, sessionToken)
        {
            this.UserTokenId = userTokenId;
            this.ClientUniqueId = clientUniqueId;
            this.Amount = amount;
            this.Currency = currency;
            this.UserPaymentOption = userPaymentOption;
            this.RequestUri = this.CreateRequestUri(ApiConstants.PayoutUrl);
        }

        /// <summary>
        /// ID of the user in merchant system.
        /// </summary>
        public string UserTokenId
        {
            get { return this.userTokenId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.UserTokenId));
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

        public string Amount
        {
            get { return this.amount; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 12, nameof(this.Amount));
                this.amount = value;
            }
        }

        public string Currency
        {
            get { return this.currency; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 3, nameof(this.Currency));
                this.currency = value;
            }
        }

        public UserPaymentOption UserPaymentOption { get; set; }

        /// <summary>
        /// Enables the addition of a free text comment to the request.
        /// </summary>
        public string Comment
        {
            get { return this.comment; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.Comment));
                this.comment = value;
            }
        }

        public DynamicDescriptor DynamicDescriptor { get; set; }

        public MerchantDetails MerchantDetails { get; set; }

        public UrlDetails UrlDetails { get; set; }

        public CardData CardData { get; set; }
    }
}
