using Safecharge.Model.PaymentOptionModels.ThreeDModels;
using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.InitPayment
{
    /// <summary>
    /// Holder for ThreeD info in PaymentOption's card data in InitPayment.
    /// </summary>
    public class InitPaymentThreeD
    {
        private string methodNotificationUrl;

        public string MethodNotificationUrl
        {
            get { return this.methodNotificationUrl; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.MethodNotificationUrl));
                this.methodNotificationUrl = value;
            }
        }

        public Acquirer Acquirer { get; set; }
    }
}
