using Safecharge.Utils.Enum;

namespace Safecharge.Response.Common
{
    /// <summary>
    /// Abstract class to be used as a base for all of the responses from SafeCharge's servers.
    /// </summary>
    public abstract class SafechargeResponse
    {
        public long InternalRequestId { get; set; }

        public ResponseStatus Status { get; set; }

        public int ErrCode { get; set; }

        public string Reason { get; set; }

        public string MerchantId { get; set; }

        public string MerchantSiteId { get; set; }

        public string Version { get; set; }

        public string ClientRequestId { get; set; }

        public string SessionToken { get; set; }

        public string ClientUniqueId { get; set; }

        public ErrorType? ErrorType { get; set; } = null;

        public ApiType ApiType { get; set; }

        public string Hint { get; set; }

    }
}
