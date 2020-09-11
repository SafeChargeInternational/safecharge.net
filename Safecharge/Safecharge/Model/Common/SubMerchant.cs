using Safecharge.Utils;

namespace Safecharge.Model.Common
{
    /// <summary>
    /// Holder for submerchant information.
    /// </summary>
    public class SubMerchant
    {
        private string id;
        private string countryCode;
        private string city;

        /// <summary>
        /// This field represents the ID of internal merchants and will be forwarded to Mastercard as “SubMerchantId”.
        /// </summary>
        public string Id
        {
            get { return this.id; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 15, nameof(this.Id));
                this.id = value;
            }
        }

        /// <summary>
        /// The payment facilitator’s sub-merchant’s two-letter ISO country code.
        /// </summary>
        public string CountryCode
        {
            get { return this.countryCode; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.CountryCode));
                this.countryCode = value;
            }
        }

        /// <summary>
        /// The payment facilitator’s sub-merchant’s city name.
        /// </summary>
        public string City
        {
            get { return this.city; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 20, nameof(this.City));
                this.city = value;
            }
        }
    }
}
