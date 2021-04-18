namespace Safecharge.Utils
{
    public class ApiConstants
    {
        // Pre-configured hosts:
        public const string IntegrationHost = "https://ppp-test.safecharge.com/ppp/";

        // API enpoints:
        public const string GetSessionTokenUrl = "api/v1/getSessionToken.do";
        public const string PaymentUrl = "api/v1/payment.do";
        public const string SettleTransactionUrl = "api/v1/settleTransaction.do";
        public const string VoidTransactionUrl = "api/v1/voidTransaction.do";
        public const string RefundTransactionUrl = "api/v1/refundTransaction.do";
        public const string GetPaymentStatusUrl = "api/v1/getPaymentStatus.do";
        public const string OpenOrderUrl = "api/v1/openOrder.do";
        public const string InitPaymentUrl = "api/v1/initPayment.do";
        public const string Authorize3dUrl = "api/v1/authorize3d.do";
        public const string Verify3dUrl = "api/v1/verify3d.do";
        public const string PayoutUrl = "api/v1/payout.do";
        public const string GetCardDetailsUrl = "api/v1/getCardDetails.do";
        public const string GetMerchantPaymentMethodsUrl = "api/v1/getMerchantPaymentMethods.do";
        public const string GetDcc = "api/v1/getDccDetails";

        public const string TransactionStatusApproved = "APPROVED";
        public const string TransactionStatusDeclined = "DECLINED";
        public const string TransactionStatusError = "ERROR";
        public const string TransactionStatusRedirect = "REDIRECT";

        public const string TransactionTypeAuth = "Auth";
        public const string TransactionTypeSale = "Sale";
        public const string TransactionTypePreAuth = "PreAuth";
        public const string TransactionTypeInitAuth3D = "InitAuth3D";

        public const string PaymentMethodTypeDeposit = "DEPOSIT";
        public const string PaymentMethodTypeWithdrawal = "WITHDRAWAL";

        public const string SourceApplication = "30";

        public const string SdkVersion = "sdk_dotnet_ver";
    }
}
