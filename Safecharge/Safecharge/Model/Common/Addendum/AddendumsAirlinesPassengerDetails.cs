namespace Safecharge.Model.Common.Addendum
{
    /// <summary>
    /// Local payment info specific for some clients. Part of <see cref="AddendumsAirlinesReservationDetails"/>.
    /// </summary>
    public class AddendumsAirlinesPassengerDetails
    {
        public string PassangerId { get; set; }

        public string PassportNumber { get; set; }

        public string CustomerCode { get; set; }

        public string FrequentFlyerCode { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
    }
}
