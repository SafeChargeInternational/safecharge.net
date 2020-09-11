using Safecharge.Utils;

namespace Safecharge.Model.Common
{
    /// <summary>
    /// Holder for merchant's specific uncategorized data.
    /// </summary>
    /// <remarks>
    /// This allows the merchant to send information with the request to be saved 
    /// in the API level and returned in response. 
    /// It is not passed to the payment gateway and is not used for processing.
    /// </remarks>
    public class MerchantDetails
    {
        private string customField1;
        private string customField2;
        private string customField3;
        private string customField4;
        private string customField5;
        private string customField6;
        private string customField7;
        private string customField8;
        private string customField9;
        private string customField10;
        private string customField11;
        private string customField12;
        private string customField13;
        private string customField14;
        private string customField15;

        public string CustomField1
        {
            get { return this.customField1; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField1));
                this.customField1 = value;
            }
        }

        public string CustomField2
        {
            get { return this.customField2; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField2));
                this.customField2 = value;
            }
        }

        public string CustomField3
        {
            get { return this.customField3; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField3));
                this.customField3 = value;
            }
        }

        public string CustomField4
        {
            get { return this.customField4; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField4));
                this.customField4 = value;
            }
        }

        public string CustomField5
        {
            get { return this.customField5; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField5));
                this.customField5 = value;
            }
        }

        public string CustomField6
        {
            get { return this.customField6; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField6));
                this.customField6 = value;
            }
        }

        public string CustomField7
        {
            get { return this.customField7; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField7));
                this.customField7 = value;
            }
        }

        public string CustomField8
        {
            get { return this.customField8; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField8));
                this.customField8 = value;
            }
        }

        public string CustomField9
        {
            get { return this.customField9; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField9));
                this.customField9 = value;
            }
        }

        public string CustomField10
        {
            get { return this.customField10; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField10));
                this.customField10 = value;
            }
        }

        public string CustomField11
        {
            get { return this.customField11; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField11));
                this.customField11 = value;
            }
        }

        public string CustomField12
        {
            get { return this.customField12; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField12));
                this.customField12 = value;
            }
        }

        public string CustomField13
        {
            get { return this.customField13; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField13));
                this.customField13 = value;
            }
        }

        public string CustomField14
        {
            get { return this.customField14; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField14));
                this.customField14 = value;
            }
        }

        public string CustomField15
        {
            get { return this.customField15; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.CustomField15));
                this.customField15 = value;
            }
        }
    }
}
