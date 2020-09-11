using Safecharge.Model.PaymentOptionModels.ThreeDModels;

namespace Safecharge.Model.PaymentOptionModels.OpenOrder
{
    /// <summary>
    /// Holder for ThreeD info in PaymentOption's card data in OpenOrder.
    /// </summary>
    public class OpenOrderThreeD
    {
        public string IsDynamic3D { get; set; }

        public string Dynamic3DMode { get; set; }

        public string ConvertNonEnrolled { get; set; }

        public OpenOrderThreeDV2AdditionalParams V2AdditionalParams { get; set; }

        public Account Account { get; set; }

        public Acquirer Acquirer { get; set; }
    }
}
