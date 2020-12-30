using System.Collections.Generic;

namespace Safecharge.Model.FraudModels
{
    /// <summary>
    /// Holder for fraud details system information., which risk management system provided the fraud information. 
    /// The provider can be Nuvei (systemId=1) or an external provider.
    /// </summary>
    public class FraudDetailsSystem
    {
        public string SystemId { get; set; }

        public string SystemName { get; set; }

        public string Decision { get; set; }

        public List<FraudRule> Rules { get; set; }
    }
}
