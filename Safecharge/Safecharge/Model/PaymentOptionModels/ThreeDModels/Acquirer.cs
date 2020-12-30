using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.ThreeDModels
{
    /// <summary>
    /// Holder for the acquirer information, with which the transaction is eventually processed.
    /// </summary>
    public class Acquirer
    {
        private string bin;
        private string merchantId;
        private string merchantName;

        public string Bin
        {
            get { return this.bin; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 6, nameof(this.Bin));
                this.bin = value;
            }
        }

        public string MerchantId
        {
            get { return this.merchantId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 20, nameof(this.MerchantId));
                this.merchantId = value;
            }
        }

        public string MerchantName
        {
            get { return this.merchantName; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 25, nameof(this.MerchantName));
                this.merchantName = value;
            }
        }
    }
}
