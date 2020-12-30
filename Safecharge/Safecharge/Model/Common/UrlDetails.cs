using Safecharge.Utils;

namespace Safecharge.Model.Common
{
    /// <summary>
    /// Holder for merchant's specific URL to redirect to in case of successful, pending, failed transaction.
    /// It also contains a URL to which to send a Direct Merchant Notification(DMN) with the result of the transaction.
    /// </summary>
    public class UrlDetails
    {
        private string successUrl;
        private string failureUrl;
        private string pendingUrl;
        private string notificationUrl;
        private string backUrl;

        public string SuccessUrl
        {
            get { return this.successUrl; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthUrl, nameof(this.SuccessUrl));
                this.successUrl = value;
            }
        }

        public string FailureUrl
        {
            get { return this.failureUrl; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthUrl, nameof(this.FailureUrl));
                this.failureUrl = value;
            }
        }

        public string PendingUrl
        {
            get { return this.pendingUrl; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthUrl, nameof(this.PendingUrl));
                this.pendingUrl = value;
            }
        }

        public string NotificationUrl
        {
            get { return this.notificationUrl; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthUrl, nameof(this.NotificationUrl));
                this.notificationUrl = value;
            }
        }

        public string BackUrl
        {
            get { return this.backUrl; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthUrl, nameof(this.BackUrl));
                this.backUrl = value;
            }
        }

        public override string ToString()
        {
            return $"{this.successUrl}{this.failureUrl}{this.pendingUrl}{this.notificationUrl}";
        }
    }
}
