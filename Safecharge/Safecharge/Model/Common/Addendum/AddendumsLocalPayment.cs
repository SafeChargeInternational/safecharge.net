using Safecharge.Utils;

namespace Safecharge.Model.Common.Addendum
{
    /// <summary>
    /// Local payment info specific for some clients. Part of <see cref="Addendums"/>.
    /// </summary>
    public class AddendumsLocalPayment
    {
        private string nationalId;
        private string debitType;
        private string firstInstallment;
        private string periodicalInstallment;
        private string numberOfInstallments;

        public string NationalId
        {
            get { return this.nationalId; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.NationalId));
                this.nationalId = value;
            }
        }

        public string DebitType
        {
            get { return this.debitType; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.DebitType));
                this.debitType = value;
            }
        }

        public string FirstInstallment
        {
            get { return this.firstInstallment; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.DebitType));
                this.firstInstallment = value;
            }
        }

        public string PeriodicalInstallment
        {
            get { return this.periodicalInstallment; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.PeriodicalInstallment));
                this.periodicalInstallment = value;
            }
        }

        public string NumberOfInstallments
        {
            get { return this.numberOfInstallments; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthStringDefault, nameof(this.NumberOfInstallments));
                this.numberOfInstallments = value;
            }
        }
    }
}
