namespace Safecharge.Model.PaymentOptionModels.InitPayment
{
    /// <summary>
    /// Holder for PaymentOption in InitPayment.
    /// </summary>
    public class InitPaymentPaymentOption : BasePaymentOption
    {
        public InitPaymentCard Card { get; set; }
    }
}
