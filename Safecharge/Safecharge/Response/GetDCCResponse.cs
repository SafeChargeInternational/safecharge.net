using Safecharge.Response.Common;

namespace Safecharge.Response
{
    public class GetDCCResponse : SafechargeResponse
    {
        public string RateValueWitMarkUp { get; set; }
        public string MarkUpValue { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string OriginalAmount { get; set; }
        public string OriginalCurrency { get; set; }
    }
}
