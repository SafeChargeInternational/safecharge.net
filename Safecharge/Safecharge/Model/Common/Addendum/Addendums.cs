namespace Safecharge.Model.Common.Addendum
{
    /// <summary>
    /// Container for additional domain specific payment info such as Airplane tickets data, country's law specific payment data, etc.
    /// </summary>
    public class Addendums
    {
        public AddendumsLocalPayment LocalPayment { get; set; }

        public AddendumsCardPresentPointOfSale CardPresentPointOfSale { get; set; }

        public AddendumsAirlines Airlines { get; set; }
    }
}
