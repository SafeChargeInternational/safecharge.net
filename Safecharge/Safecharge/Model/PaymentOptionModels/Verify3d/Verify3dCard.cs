using Safecharge.Model.PaymentOptionModels.CardModels;

namespace Safecharge.Model.PaymentOptionModels.Verify3d
{
    /// <summary>
    /// Holder for PaymentOption's card data in Verify3d.
    /// </summary>
    public class Verify3dCard : CardData
    {
        public Verify3dThreeD ThreeD { get; set; }
    }
}
