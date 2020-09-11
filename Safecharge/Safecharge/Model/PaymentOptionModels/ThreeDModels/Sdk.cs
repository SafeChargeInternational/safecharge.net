using Safecharge.Utils;

namespace Safecharge.Model.Payment
{
    /// <summary>
    /// Holder for SDK information.
    /// </summary>
    /// <remarks>This class is mandatory for 3D Secure 2.0 for transactions originating from a mobile device.</remarks>
    public class Sdk
    {
        private string appSdkInterface;
        private string appSdkUIType;
        private string appId;
        private string encData;
        private string ephemPubKey;
        private string maxTimeout;
        private string referenceNumber;
        private string transId;

        public string AppSdkInterface
        {
            get { return this.appSdkInterface; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.AppSdkInterface));
                this.appSdkInterface = value;
            }
        }

        public string AppSdkUIType
        {
            get { return this.appSdkUIType; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.AppSdkUIType));
                this.appSdkUIType = value;
            }
        }

        public string AppId
        {
            get { return this.appId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 36, nameof(this.AppId));
                this.appId = value;
            }
        }

        public string EncData
        {
            get { return this.encData; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 64000, nameof(this.EncData));
                this.encData = value;
            }
        }

        public string EphemPubKey
        {
            get { return this.ephemPubKey; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 256, nameof(this.EphemPubKey));
                this.ephemPubKey = value;
            }
        }

        public string MaxTimeout
        {
            get { return this.maxTimeout; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.MaxTimeout));
                this.maxTimeout = value;
            }
        }

        public string ReferenceNumber
        {
            get { return this.referenceNumber; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.ReferenceNumber));
                this.referenceNumber = value;
            }
        }

        public string TransId
        {
            get { return this.transId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 36, nameof(this.TransId));
                this.transId = value;
            }
        }
    }
}
