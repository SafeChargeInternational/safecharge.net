using Safecharge.Utils;

namespace Safecharge.Model.Common
{
    /// <summary>
    /// Holder for dynamic descriptor information.
    /// </summary>
    public class DynamicDescriptor
    {
        private string merchantName;
        private string merchantPhone;

        /// <summary>
        /// The merchant name, as it's displayed for the transaction on the consumer’s card statement.
        /// </summary>
        public string MerchantName
        {
            get { return this.merchantName; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthMerchantDescriptorName, nameof(this.MerchantName));
                this.merchantName = value;
            }
        }

        /// <summary>
        /// The merchant contact information, as is displayed for the transaction on the consumer’s card statement. 
        /// It can also be an email address.
        /// </summary>
        public string MerchantPhone
        {
            get { return this.merchantPhone; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthMerchantDescriptorPhone, nameof(this.MerchantPhone));
                this.merchantPhone = value;
            }
        }
    }
}
