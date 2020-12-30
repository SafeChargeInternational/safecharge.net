using Safecharge.Utils;

namespace Safecharge.Model.PaymentOptionModels.ThreeDModels
{
    /// <summary>
    /// Cardholder account information.
    /// </summary>
    public class Account
    {
        private string age;
        private string lastChangeDate;
        private string lastChangeInd;
        private string registrationDate;
        private string passwordChangeDate;
        private string resetInd;
        private string purchasesCount6M;
        private string addCardAttepmts24H;
        private string transactionsCount24H;
        private string transactionsCount1Y;
        private string cardSavedDate;
        private string cardSavedInd;
        private string addressFirstUseDate;
        private string addressFirstUseInd;
        private string nameInd;
        private string suspiciousActivityInd;

        public string Age
        {
            get { return this.age; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.Age));
                this.age = value;
            }
        }

        public string LastChangeDate
        {
            get { return this.lastChangeDate; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthDateDefault, nameof(this.LastChangeDate));
                this.lastChangeDate = value;
            }
        }

        public string LastChangeInd
        {
            get { return this.lastChangeInd; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.LastChangeInd));
                this.lastChangeInd = value;
            }
        }

        public string RegistrationDate
        {
            get { return this.registrationDate; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthDateDefault, nameof(this.RegistrationDate));
                this.registrationDate = value;
            }
        }

        public string PasswordChangeDate
        {
            get { return this.passwordChangeDate; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthDateDefault, nameof(this.PasswordChangeDate));
                this.passwordChangeDate = value;
            }
        }

        public string ResetInd
        {
            get { return this.resetInd; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.ResetInd));
                this.resetInd = value;
            }
        }

        public string PurchasesCount6M
        {
            get { return this.purchasesCount6M; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 4, nameof(this.PurchasesCount6M));
                this.purchasesCount6M = value;
            }
        }

        public string AddCardAttepmts24H
        {
            get { return this.addCardAttepmts24H; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 3, nameof(this.AddCardAttepmts24H));
                this.addCardAttepmts24H = value;
            }
        }

        public string TransactionsCount24H
        {
            get { return this.transactionsCount24H; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 3, nameof(this.TransactionsCount24H));
                this.transactionsCount24H = value;
            }
        }

        public string TransactionsCount1Y
        {
            get { return this.transactionsCount1Y; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 3, nameof(this.TransactionsCount1Y));
                this.transactionsCount1Y = value;
            }
        }

        public string CardSavedDate
        {
            get { return this.cardSavedDate; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthDateDefault, nameof(this.CardSavedDate));
                this.cardSavedDate = value;
            }
        }

        public string CardSavedInd
        {
            get { return this.cardSavedInd; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.CardSavedInd));
                this.cardSavedInd = value;
            }
        }

        public string AddressFirstUseDate
        {
            get { return this.addressFirstUseDate; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthDateDefault, nameof(this.AddressFirstUseDate));
                this.addressFirstUseDate = value;
            }
        }

        public string AddressFirstUseInd
        {
            get { return this.addressFirstUseInd; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.AddressFirstUseInd));
                this.addressFirstUseInd = value;
            }
        }

        public string NameInd
        {
            get { return this.nameInd; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.NameInd));
                this.nameInd = value;
            }
        }

        public string SuspiciousActivityInd
        {
            get { return this.suspiciousActivityInd; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, 2, nameof(this.SuspiciousActivityInd));
                this.suspiciousActivityInd = value;
            }
        }
    }
}
