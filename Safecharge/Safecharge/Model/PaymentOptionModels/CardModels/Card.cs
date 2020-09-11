using Safecharge.Model.PaymentOptionModels.ThreeDModels;
using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.CardModels
{
    /// <summary>
    /// Represents a credit/debit card.
    /// </summary>
    public class Card : CardData
    {
        private string acquirerId;

        public ExternalToken ExternalToken { get; set; }

        public StoredCredentials StoredCredentials { get; set; }

        public string AcquirerId
        {
            get { return this.acquirerId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.AcquirerId));
                this.acquirerId = value;
            }
        }

        public ThreeD ThreeD { get; set; }
    }
}
