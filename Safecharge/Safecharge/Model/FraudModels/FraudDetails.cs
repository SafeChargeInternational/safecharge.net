namespace Safecharge.Model.FraudModels
{
    /// <summary>
    /// Holder for fraud details information.
    /// </summary>
    /// <remarks>
    /// To receive this information in response special configurations are required. 
    /// For more information, please contact Nuvei’s Integration Support Team.
    /// </remarks>
    public class FraudDetails
    {
        public string FinalDecision { get; set; }

        public string Recommendations { get; set; }

        public FraudDetailsSystem System { get; set; }
    }
}
