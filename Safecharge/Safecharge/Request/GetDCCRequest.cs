using Safecharge.Model.Common;
using Safecharge.Request.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safecharge.Request
{
    public class GetDCCRequest : SafechargeRequest
    {
        public GetDCCRequest() : base()
        {
        }
        public GetDCCRequest(MerchantInfo merchantInfo, string sessionToken) :
            base(merchantInfo, ChecksumOrderMapping.ApiGenericChecksumMapping, sessionToken)
        {
            this.RequestUri = this.CreateRequestUri(ApiConstants.GetDcc);
        }
        public string ClientUniqueId { get; set; }
        public string OriginalAmount { get; set; }
        public string Amount { get; set; }
        public string OriginalCurrency { get; set; }
        public string Currency { get; set; }
        public string Apm { get; set; }
    }
}
