using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.Verify3d
{
    /// <summary>
    /// Holder for ThreeD info in PaymentOption's card data in Verify3d.
    /// </summary>
    public class Verify3dThreeD
    {
        private string mpiChallengePreference;
        private string externalRiskScore;

        public string PaResponse { get; set; }

        public string MpiChallengePreference
        {
            get { return this.mpiChallengePreference; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 1, nameof(this.MpiChallengePreference));
                this.mpiChallengePreference = value;
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
