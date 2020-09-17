using Safecharge.Model.Payment;

namespace Safecharge.Response.Payment
{
    /// <summary>
    /// Holder for ThreeD data response.
    /// </summary>
    public class ThreeDResponse
    {
        public string ThreeDReason { get; set; }

        public string ThreeDFlow { get; set; }

        public string PaRequest { get; set; }

        public string AcsUrl { get; set; }

        public string Eci { get; set; }

        public string MethodUrl { get; set; }

        public string Version { get; set; }

        public string V2supported { get; set; }

        public string MethodPayload { get; set; }

        public string DirectoryServerId { get; set; }

        public string DirectoryServerPublicKey { get; set; }

        public string ServerTransId { get; set; }

        public string WhiteListStatus { get; set; }

        public string Cavv { get; set; }

        public string AcsChallengeMandated { get; set; }

        public string CReq { get; set; }

        public string AuthenticationType { get; set; }

        public string CardHolderInfoText { get; set; }

        public Sdk Sdk { get; set; }

        public string Xid { get; set; }

        public string Result { get; set; }

        public string AcsTtransId { get; set; }

        public string DsTransId { get; set; }

        public string IsLiabilityOnIssuer { get; set; }
    }
}
