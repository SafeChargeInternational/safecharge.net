using Safecharge.Model.PaymentOptionModels.CardModels;

namespace Safecharge.Model.PaymentOptionModels.OpenOrder
{
    /// <summary>
    /// Holder for PaymentOption's card data in OpenOrder.
    /// </summary>
    public class OpenOrderCard
    {
        public string AcquirerId { get; set; }

        public StoredCredentials StoredCredentials { get; set; }

        public OpenOrderThreeD ThreeD { get; set; }
    }
}
