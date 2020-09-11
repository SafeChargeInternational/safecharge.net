using Safecharge.Model.Common;
using Safecharge.Request.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request
{
    /// <summary>
    /// This method can be used by the merchant to retrieve details about a card, such as card type and brand. 
    /// </summary>
    /// <remarks>
    /// In addition, the methods also returns the currency balance for the account,
    /// which might be important if you would like to perform currency conversion for this card.
    /// For a postman script that simulates this method, 
    /// please click <a href="https://docs.safecharge.com/wp-content/uploads/2020/06/Get-Card-details.postman_collection.json">here</a>, 
    /// or refer to our <a href="https://docs.safecharge.com/documentation/guides/testing/testing-apis-with-postman/">guide</a> for testing with Postman.
    /// </remarks>
    public class GetCardDetailsRequest : SafechargeRequest
    {
        private string clientUniqueId;
        private string cardNumber;

        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public GetCardDetailsRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCardDetailsRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        /// <param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="cardNumber">CardNumber or bin to be populated</param>
        public GetCardDetailsRequest(
            MerchantInfo merchantInfo,
            string sessionToken,
            string clientUniqueId,
            string cardNumber)
            : base(merchantInfo, ChecksumOrderMapping.NoChecksumMapping, sessionToken)
        {
            this.ClientUniqueId = clientUniqueId;
            this.CardNumber = cardNumber;
            this.RequestUri = this.CreateRequestUri(ApiConstants.GetCardDetailsUrl);
        }

        /// <summary>
        /// ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: 
        /// reconciliation, identifying the transaction in the event of any issues, etc.
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

        /// <summary>
        /// CardNumber or bin must be populated
        /// </summary>
        public string CardNumber
        {
            get { return this.cardNumber; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 20, nameof(this.CardNumber));
                this.cardNumber = value;
            }
        }
    }
}
