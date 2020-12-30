using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.CardModels
{
    /// <summary>
    /// Holder for stored credenials data.
    /// </summary>
    public class StoredCredentials
    {
        private string storedCredentialsMode;

        /// <summary>
        /// This parameter shows whether or not stored tokenized card data is sent to execute the transaction. 
        /// </summary>
        public string StoredCredentialsMode
        {
            get { return this.storedCredentialsMode; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 1, nameof(this.StoredCredentialsMode));
                this.storedCredentialsMode = value;
            }
        }
    }
}
