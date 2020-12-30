namespace Safecharge.Model.PaymentOptionModels.Verify3d
{
    /// <summary>
    /// Holder for PaymentOption in Verify3d.
    /// </summary>
    public class Verify3dPaymentOption : BasePaymentOption
    {
        public Verify3dCard Card { get; set; }
    }
}
