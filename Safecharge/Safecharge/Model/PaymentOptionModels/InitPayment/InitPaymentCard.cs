using Safecharge.Model.PaymentOptionModels.CardModels;

namespace Safecharge.Model.PaymentOptionModels.InitPayment
{
    /// <summary>
    /// Holder for PaymentOption's card data in InitPayment.
    /// </summary>
    public class InitPaymentCard : CardData
    {
        public InitPaymentThreeD ThreeD { get; set; }
    }
}
