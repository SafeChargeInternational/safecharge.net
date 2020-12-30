namespace Safecharge.Model.Common.Addendum
{
    /// <summary>
    /// Local payment info specific for some clients. Part of <see cref="AddendumsAirlinesReservationDetails"/>.
    /// </summary>
    public class AddendumsAirlinesFlightLegDetails
    {
        public string FlightLegId { get; set; }

        public string AirlineCode { get; set; }

        public string FlightNumber { get; set; }

        public string DepartureDate { get; set; }

        public string ArrivalDate { get; set; }

        public string DepartureCountry { get; set; }

        public string DepartureCity { get; set; }

        public string DepartureAirport { get; set; }

        public string DestinationCountry { get; set; }

        public string DestinationCity { get; set; }

        public string DestinationAirport { get; set; }

        public string Type { get; set; }

        public string FlightType { get; set; }

        public string TicketDeliveryMethod { get; set; }

        public string TicketDeliveryRecipient { get; set; }

        public string FareBasisCode { get; set; }

        public string ServiceClass { get; set; }

        public string SeatClass { get; set; }

        public string StopOverCode { get; set; }

        public string DepartureTaxAmount { get; set; }

        public string DepartureTaxCurrency { get; set; }

        public string FareAmount { get; set; }

        public string FeeAmount { get; set; }

        public string TaxAmount { get; set; }

        public string LayoutIntegererval { get; set; }
    }
}
