namespace Safecharge.Model.Common.Addendum
{
    /// <summary>
    /// Local payment info specific for some clients. Part of <see cref="Addendums"/>.
    /// </summary>
    public class AddendumsCardPresentPointOfSale
    {
        public string TerminalId { get; set; }

        public string TrackData { get; set; }

        public string TrackType { get; set; }

        public string Icc { get; set; }

        public string PinData { get; set; }

        public string EntryMode { get; set; }

        public string TerminalCapability { get; set; }

        public string TerminalAttendance { get; set; }

        public string CardSequenceNum { get; set; }

        public string OfflineResCode { get; set; }

        public string LocalTime { get; set; }

        public string LocalDate { get; set; }

        public string CvMethod { get; set; }

        public string CvEntity { get; set; }

        public string OutputCapability { get; set; }

        public string AutoReversal { get; set; }

        public string AutoReversalAmount { get; set; }

        public string AutoReversalCurrency { get; set; }

        public string Channel { get; set; }

        public string SuppressAuth { get; set; }

        public string TerminalCity { get; set; }

        public string TerminalAddress { get; set; }

        public string TerminalCountry { get; set; }

        public string TerminalZip { get; set; }

        public string TerminalState { get; set; }

        public string TerminalModel { get; set; }

        public string TerminalManufacturer { get; set; }

        public string TerminalMacAddress { get; set; }

        public string TerminalKernel { get; set; }

        public string TerminalImei { get; set; }

        public string MobileTerminal { get; set; }

        public string TerminalType { get; set; }

        public string SecurityControl { get; set; }
    }
}
