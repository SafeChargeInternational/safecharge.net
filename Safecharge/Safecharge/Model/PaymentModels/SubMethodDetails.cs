using Safecharge.Utils;

namespace Safecharge.Model.PaymentModels
{
    /// <summary>
    /// Holder for submethod details information.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Related to advanced APMs flows. 
    /// See <a href="https://docs.safecharge.com/documentation/guides/alternative-payments/apm-submethods/">APM Submethods</a> for details.
    /// </para>
    /// <para>The submethod parameter enables working with a specific payment method in multiple flows.</para>
    /// </remarks>
    public class SubMethodDetails
    {
        private string subMethod;
        private string subMethodField1;
        private string subMethodField2;

        public string SubMethod
        {
            get { return this.subMethod; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 50, nameof(this.SubMethod));
                this.subMethod = value;
            }
        }

        public string SubMethodField1
        {
            get { return this.subMethodField1; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2048, nameof(this.SubMethodField1));
                this.subMethodField1 = value;
            }
        }

        public string SubMethodField2
        {
            get { return this.subMethodField2; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2048, nameof(this.SubMethodField2));
                this.subMethodField2 = value;
            }
        }
    }
}
