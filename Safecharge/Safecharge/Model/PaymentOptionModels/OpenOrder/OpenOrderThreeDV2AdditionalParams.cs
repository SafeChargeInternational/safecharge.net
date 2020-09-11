using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.OpenOrder
{
    /// <summary>
    /// Holder for ThreeD info's V2AdditionalParams in PaymentOption's card data in OpenOrder.
    /// </summary>
    public class OpenOrderThreeDV2AdditionalParams
    {
        private string deliveryEmail;
        private string deliveryTimeFrame;
        private string giftCardAmount;
        private string giftCardCurrency;
        private string preOrderDate;
        private string preOrderPurchaseInd;

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

        public string GiftCardCurrency
        {
            get { return this.giftCardCurrency; }
            set
            {
                Guard.RequiresLength(value?.Length, 3, nameof(this.GiftCardCurrency));
                this.giftCardCurrency = value;
            }
        }

        public string PreOrderDate
        {
            get { return this.preOrderDate; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 8, nameof(this.PreOrderDate));
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

        public string ReorderItemsInd { get; set; }

        public string RebillExpiry { get; set; }

        public string RebillFrequency { get; set; }

        public bool ExceptionPayment3DAuth { get; set; }
    }
}
