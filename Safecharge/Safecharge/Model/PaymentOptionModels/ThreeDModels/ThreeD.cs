using Safecharge.Model.Payment;
using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.ThreeDModels
{
    /// <summary>
    /// Holder for 3D information payment option.
    /// </summary>
    public class ThreeD
    {
        private string isDynamic3D;
        private string methodCompletionInd;
        private string notificationURL;
        private string merchantURL;
        private string platformType;
        private string version;

        public string IsDynamic3D
        {
            get { return this.isDynamic3D; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.isDynamic3D));
                Guard.RequiresMaxLength(value.Length, Constants.MinLengthStringDefault, nameof(this.IsDynamic3D));
                this.isDynamic3D = value;
            }
        }

        public string Dynamic3DMode { get; set; }

        public string ConvertNonEnrolled { get; set; }

        public ExternalMpi ExternalMpi { get; set; }

        public string PaResponse { get; set; }

        public string MethodCompletionInd
        {
            get { return this.methodCompletionInd; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.methodCompletionInd));
                Guard.RequiresMaxLength(value.Length, Constants.MinLengthStringDefault, nameof(this.MethodCompletionInd));
                this.methodCompletionInd = value;
            }
        }

        public string NotificationURL
        {
            get { return this.notificationURL; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 256, nameof(this.NotificationURL));
                this.notificationURL = value;
            }
        }

        public string MerchantURL
        {
            get { return this.merchantURL; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2048, nameof(this.MerchantURL));
                this.merchantURL = value;
            }
        }

        public string PlatformType
        {
            get { return this.platformType; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.PlatformType));
                this.platformType = value;
            }
        }

        public string Version
        {
            get { return this.version; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 8, nameof(this.Version));
                this.version = value;
            }
        }

        public BrowserDetails BrowserDetails { get; set; }

        public Sdk Sdk { get; set; }

        public Acquirer Acquirer { get; set; }

        public Account Account { get; set; }

        public V2AdditionalParams V2AdditionalParams { get; set; }
    }
}
