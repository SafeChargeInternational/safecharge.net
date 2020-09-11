using System.Threading.Tasks;
using Safecharge.Request;
using Safecharge.Request.Common;
using Safecharge.Response;
using Safecharge.Response.Payment;
using Safecharge.Response.Transaction;
using Safecharge.Utils.Exceptions;

namespace Safecharge
{
    /// <summary>
    /// This is an interface for the class providing functionality to execute SafechargeRequests directly to the SafeCharge's REST API
    /// </summary>
    public interface ISafechargeRequestExecutor
    {
        /// <summary>
        /// This method sends <see cref="GetSessionTokenRequest"/> to the SafeCharge's REST API to get the session token.
        /// </summary>
        /// <param name="getSessionTokenRequest">Request model for getting the session token</param>
        /// <returns><see cref="GetSessionTokenResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<GetSessionTokenResponse> GetSessionToken(GetSessionTokenRequest getSessionTokenRequest);

        /// <summary>
        /// This method should be used to create request for payment endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="paymentRequest">Request model</param>
        /// <returns><see cref="PaymentResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<PaymentResponse> Payment(PaymentRequest paymentRequest);

        /// <summary>
        /// This method should be used to create request for settleTransaction endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="settleTransactionRequest">Request model</param>
        /// <returns><see cref="SettleTransactionResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<SettleTransactionResponse> SettleTransaction(SettleTransactionRequest settleTransactionRequest);

        /// <summary>
        /// This method should be used to create request for voidTransaction endpoint in Safecharge's REST API.
        /// </summary>s
        /// <returns><see cref="VoidTransactionResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<VoidTransactionResponse> VoidTransaction(VoidTransactionRequest voidTransactionRequest);

        /// <summary>
        /// This method should be used to create request for refundTransaction endpoint in Safecharge's REST API.
        /// </summary>
        /// <returns><see cref="RefundTransactionResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<RefundTransactionResponse> RefundTransaction(RefundTransactionRequest refundTransactionRequest);

        /// <summary>
        /// This method should be used to create request for getPaymentStatus endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="getPaymentStatusRequest"><see cref="GetPaymentStatusRequest"/> data</param>
        /// <returns><see cref="GetPaymentStatusResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<GetPaymentStatusResponse> GetPaymentStatus(GetPaymentStatusRequest getPaymentStatusRequest);

        /// <summary>
        /// This method should be used to create request for openOrder endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="openOrderRequest"><see cref="OpenOrderRequest"/> data</param>
        /// <returns><see cref="OpenOrderResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<OpenOrderResponse> OpenOrder(OpenOrderRequest openOrderRequest);

        /// <summary>
        /// This method should be used to create request for initPayment endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="initPaymentRequest"><see cref="InitPaymentRequest"/> data</param>
        /// <returns><see cref="InitPaymentResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<InitPaymentResponse> InitPayment(InitPaymentRequest initPaymentRequest);

        /// <summary>
        /// This method should be used to create request for authorize3d endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="authorize3dRequest"><see cref="Authorize3dRequest"/> data</param>
        /// <returns><see cref="Authorize3dResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<Authorize3dResponse> Authorize3d(Authorize3dRequest authorize3dRequest);

        /// <summary>
        /// This method should be used to create request for verify3d endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="verify3dRequest"><see cref="Verify3dRequest"/> data</param>
        /// <returns><see cref="Verify3dResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<Verify3dResponse> Verify3d(Verify3dRequest verify3dRequest);

        /// <summary>
        /// This method should be used to create request for payout endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="payoutRequest"><see cref="PayoutRequest"/> data</param>
        /// <returns><see cref="PayoutResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<PayoutResponse> Payout(PayoutRequest payoutRequest);

        /// <summary>
        /// This method should be used to create request for getCardDetails endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="request"><see cref="GetCardDetailsRequest"/> data</param>
        /// <returns><see cref="GetCardDetailsResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<GetCardDetailsResponse> GetCardDetails(GetCardDetailsRequest request);

        /// <summary>
        /// This method should be used to create request for GetMerchantPaymentMethods endpoint in Safecharge's REST API.
        /// </summary>
        /// <param name="request"><see cref="GetMerchantPaymentMethodsRequest"/> data</param>
        /// <returns><see cref="GetMerchantPaymentMethodsResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<GetMerchantPaymentMethodsResponse> GetMerchantPaymentMethods(GetMerchantPaymentMethodsRequest request);

        /// <summary>
        /// This method executes POST SafechargeRequests to the SafeCharge's REST API 
        /// </summary>
        /// <typeparam name="T1"><see cref="Response.Common.SafechargeResponse"/> data</typeparam>
        /// <typeparam name="T2"><see cref="SafechargeRequest"/> data</typeparam>
        /// <param name="request"><see cref="SafechargeBaseRequest"/> data</param>
        /// <returns><see cref="Response.Common.SafechargeResponse"/> data</returns>
        Task<T1> PostAsync<T1, T2>(SafechargeBaseRequest request);
    }
}
