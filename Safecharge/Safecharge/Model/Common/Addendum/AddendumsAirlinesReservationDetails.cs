using System.Collections.Generic;

namespace Safecharge.Model.Common.Addendum
{
    /// <summary>
    /// Local payment info specific for some clients. Part of <see cref="AddendumsAirlines"/>.
    /// </summary>
    public class AddendumsAirlinesReservationDetails
    {
        public string AddendumSent { get; set; }

        public string PnrCode { get; set; }

        public string BookingSystemUniqueId { get; set; }

        public string ComputerizedReservationSystem { get; set; }

        public string TicketNumber { get; set; }

        public string DocumentType { get; set; }

        public string FlightDateUTC { get; set; }

        public string IssueDate { get; set; }

        public string TravelAgencyCode { get; set; }

        public string TravelAgencyName { get; set; }

        public string TravelAgencyInvoiceNumber { get; set; }

        public string TravelAgencyPlanName { get; set; }

        public string RestrictedTicketIndicator { get; set; }

        public string IssuingCarrierCode { get; set; }

        public string IsCardholderTraveling { get; set; }

        public string PassengersCount { get; set; }

        public string InfantsCount { get; set; }

        public string PayerPassportId { get; set; }

        public string TotalFare { get; set; }

        public string TotalTaxes { get; set; }

        public string TotalFee { get; set; }

        public string BoardingFee { get; set; }

        public string TicketIssueAddress { get; set; }

        public List<AddendumsAirlinesPassengerDetails> PassengerDetails { get; set; }

        public List<AddendumsAirlinesFlightLegDetails> FlightLegDetails { get; set; }
    }
}
