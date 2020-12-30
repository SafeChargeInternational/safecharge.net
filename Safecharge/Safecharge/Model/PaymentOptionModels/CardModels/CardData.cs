using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.CardModels
{
    /// <summary>
    /// Holder for credit/debit/prepaid card data.
    /// </summary>
    public class CardData
    {
        private string cardNumber;
        private string cardHolderName;
        private string expirationMonth;
        private string expirationYear;
        private string ccTempToken;
        private string cVV;

        public string CardNumber
        {
            get { return this.cardNumber; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthCardNumber, nameof(this.CardNumber));
                this.cardNumber = value;
            }
        }

        public string CardHolderName
        {
            get { return this.cardHolderName; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthCardHolderName, nameof(this.CardHolderName));
                this.cardHolderName = value;
            }
        }

        public string ExpirationMonth
        {
            get { return this.expirationMonth; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.ExpirationMonth));
                this.expirationMonth = value;
            }
        }

        public string ExpirationYear
        {
            get { return this.expirationYear; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.ExpirationYear));
                this.expirationYear = value;
            }
        }

        public string CcTempToken
        {
            get { return this.ccTempToken; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringId, nameof(this.CcTempToken));
                this.ccTempToken = value;
            }
        }

        public string CVV
        {
            get { return this.cVV; }
            set
            {
                Guard.RequiresLengthBetween(value?.Length, 3, 4, nameof(this.CVV));
                this.cVV = value;
            }
        }
    }
}
