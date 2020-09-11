namespace Safecharge.Model.FraudModels
{
    /// <summary>
    /// Holder for fraud rule information. 
    /// Refers to the risk management system rules triggered by the transaction.
    /// </summary>
    public class FraudRule
    {
        public string RuleId { get; set; }

        public string RuleDescription { get; set; }
    }
}
