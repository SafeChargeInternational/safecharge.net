using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.ThreeDModels
{
    /// <summary>
    /// Holder for V2AdditionalParams.
    /// </summary>
    public class V2AdditionalParams
    {
        private string deliveryEmail;
        private string deliveryTimeFrame;
        private string giftCardAmount;
        private string giftCardCount;
        private string giftCardCurrency;
        private string preOrderDate;
        private string preOrderPurchaseInd;
        private string reorderItemsInd;
        private string shipIndicator;
        private string rebillExpiry;
        private string rebillFrequency;
        private string challengeWindowSize;
        private string challengePreference;

        public string DeliveryEmail
        {
            get { return this.deliveryEmail; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 254, nameof(this.DeliveryEmail));
                this.deliveryEmail = value;
            }
        }

        public string DeliveryTimeFrame
        {
            get { return this.deliveryTimeFrame; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.DeliveryTimeFrame));
                this.deliveryTimeFrame = value;
            }
        }

        public string GiftCardAmount
        {
            get { return this.giftCardAmount; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 15, nameof(this.GiftCardAmount));
                this.giftCardAmount = value;
            }
        }

        public string GiftCardCount
        {
            get { return this.giftCardCount; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.GiftCardCount));
                this.giftCardCount = value;
            }
        }

        public string GiftCardCurrency
        {
            get { return this.giftCardCurrency; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 3, nameof(this.GiftCardCurrency));
                this.giftCardCurrency = value;
            }
        }

        public string PreOrderDate
        {
            get { return this.preOrderDate; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthDateDefault, nameof(this.preOrderDate));
                this.preOrderDate = value;
            }
        }

        public string PreOrderPurchaseInd
        {
            get { return this.preOrderPurchaseInd; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.PreOrderPurchaseInd));
                this.preOrderPurchaseInd = value;
            }
        }

        public string ReorderItemsInd
        {
            get { return this.reorderItemsInd; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.ReorderItemsInd));
                this.reorderItemsInd = value;
            }
        }

        public string ShipIndicator
        {
            get { return this.shipIndicator; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.ShipIndicator));
                this.shipIndicator = value;
            }
        }

        public string RebillExpiry
        {
            get { return this.rebillExpiry; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthDateDefault, nameof(this.RebillExpiry));
                this.rebillExpiry = value;
            }
        }

        public string RebillFrequency
        {
            get { return this.rebillFrequency; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 4, nameof(this.RebillFrequency));
                this.rebillFrequency = value;
            }
        }

        public string ChallengeWindowSize
        {
            get { return this.challengeWindowSize; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.ChallengeWindowSize));
                this.challengeWindowSize = value;
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

        public bool ExceptionPayment3DAuth { get; set; }
    }
}
