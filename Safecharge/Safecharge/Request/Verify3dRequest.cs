using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels.Verify3d;
using Safecharge.Request.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    /// Call this method if you need to use the SafeCharge MPI service to perform a 3D Secure only request.
    /// </summary>
    /// <remarks>
    /// This method is called after the <see cref="Request.Authorize3dRequest"/> method in case of the Challenge.
    /// This method retrieves the generic 3D Secure result (ECI and CAVV) that you need to send to your PSP or acquirer to benefit
    /// from the 3D Secure liability shift received from the SafeCharge 3D Secure service.
    /// <para>See more <a href="https://www.safecharge.com/docs/api/?json#verify3d">here</a>.</para>
    /// </remarks>
    public class Verify3dRequest : SafechargeRequest
    {
        private string userTokenId;
        private string clientUniqueId;
        private string currency;
        private string amount;
        private string customSiteName;
        private string customData;
        private string relatedTransactionId;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public Verify3dRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verify3dRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="currency">The three character ISO currency code of the transaction.</param>
        /// <param name="amount">The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="paymentOption">Details about the payment method.</param>
        /// <param name="relatedTransactionId">The ID of the original transaction.</param>
        public Verify3dRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string currency,
            string amount,
            Verify3dPaymentOption paymentOption,
            string relatedTransactionId)
            : base(merchantInfo, ChecksumOrderMapping.ApiGenericChecksumMapping, sessionToken)
        {
            this.Currency = currency;
            this.Amount = amount;
            this.PaymentOption = paymentOption;
            this.RelatedTransactionId = relatedTransactionId;
            this.RequestUri = this.CreateRequestUri(ApiConstants.Verify3dUrl);
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

        public string Currency
        {
            get { return this.currency; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.Currency));
                Guard.RequiresMaxLength(value?.Length, 3, nameof(this.Currency));
                this.currency = value;
            }
        }

        public string Amount
        {
            get { return this.amount; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.Amount));
                Guard.RequiresMaxLength(value?.Length, 12, nameof(this.Amount));
                this.amount = value;
            }
        }

        public string CustomSiteName
        {
            get { return this.customSiteName; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 50, nameof(this.CustomSiteName));
                this.customSiteName = value;
            }
        }

        public string CustomData
        {
            get { return this.customData; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomData));
                this.customData = value;
            }
        }

        public string RelatedTransactionId
        {
            get { return this.relatedTransactionId; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.RelatedTransactionId));
                Guard.RequiresMaxLength(value?.Length, 19, nameof(this.RelatedTransactionId));
                this.relatedTransactionId = value;
            }
        }

        public UserAddress BillingAddress { get; set; }

        public MerchantDetails MerchantDetails { get; set; }

        public Verify3dPaymentOption PaymentOption { get; set; }
    }
}
