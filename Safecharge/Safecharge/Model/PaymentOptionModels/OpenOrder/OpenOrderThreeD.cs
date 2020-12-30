using Safecharge.Model.PaymentOptionModels.ThreeDModels;
using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.OpenOrder
{
    /// <summary>
    /// Holder for ThreeD info in PaymentOption's card data in OpenOrder.
    /// </summary>
    public class OpenOrderThreeD
    {
        private string merchantURL;
        private string challengePreference;
        private string externalRiskScore;

        public string IsDynamic3D { get; set; }

        public string Dynamic3DMode { get; set; }

        public string ConvertNonEnrolled { get; set; }

        public OpenOrderThreeDV2AdditionalParams V2AdditionalParams { get; set; }

        public Account Account { get; set; }

        public Acquirer Acquirer { get; set; }

        public ExternalMpi ExternalMpi { get; set; }

        public string MerchantURL
        {
            get { return this.merchantURL; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2048, nameof(this.MerchantURL));
                this.merchantURL = value;
            }
        }

        public string ChallengePreference
        {
            get { return this.challengePreference; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.ChallengePreference));
                this.challengePreference = value;
            }
        }

        public string ExternalRiskScore
        {
            get { return this.externalRiskScore; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 3, nameof(this.ExternalRiskScore));
                this.externalRiskScore = value;
            }
        }
    }
}
