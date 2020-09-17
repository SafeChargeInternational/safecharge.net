using Safecharge.Utils;

namespace Safecharge.Model.Common
{
    /// <summary>
    /// Holder for user address information.
    /// </summary>
    public class UserAddress
    {
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private string address;
        private string city;
        private string country;
        private string state;
        private string zip;
        private string cell;
        private string county;

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthFirstName, nameof(this.FirstName));
                this.firstName = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthLastName, nameof(this.LastName));
                this.lastName = value;
            }
        }

        public string Email
        {
            get { return this.email; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthEmail, nameof(this.Email));
                this.email = value;
            }
        }

        public string Phone
        {
            get { return this.phone; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthPhone, nameof(this.Phone));
                this.phone = value;
            }
        }

        public string Address
        {
            get { return this.address; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthAddress, nameof(this.Address));
                this.address = value;
            }
        }

        public string City
        {
            get { return this.city; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthCity, nameof(this.City));
                this.city = value;
            }
        }

        public string Country
        {
            get { return this.country; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthCountry, nameof(this.Country));
                this.country = value;
            }
        }

        public string State
        {
            get { return this.state; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthState, nameof(this.State));
                this.state = value;
            }
        }

        public string Zip
        {
            get { return this.zip; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthZip, nameof(this.Zip));
                this.zip = value;
            }
        }

        public string Cell
        {
            get { return this.cell; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthPhone, nameof(this.Cell));
                this.cell = value;
            }
        }

        public string County
        {
            get { return this.county; }
            set
            {
                Guard.RequiresMaxLength(value?.Length, Constants.MaxLengthCounty, nameof(this.County));
                this.county = value;
            }
        }

        public string AddressMatch { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string HomePhone { get; set; }

        public string WorkPhone { get; set; }
    }
}
