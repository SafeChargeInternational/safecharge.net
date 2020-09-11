using NUnit.Framework;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class VoidTransactionTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestVoidTransactionSuccess()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard) { Items = items };

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var request = new VoidTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                paymentResponse.TransactionId);

            var response = requestExecutor.VoidTransaction(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestSimpleAuthSettleScenarioWithVoid()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard)
            {
                TransactionType = ApiConstants.TransactionTypeAuth,
                Items = items
            };

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var settleRequest = new SettleTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                paymentResponse.TransactionId);

            var settleResponse = requestExecutor.SettleTransaction(settleRequest).GetAwaiter().GetResult();

            var request = new VoidTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                settleResponse.TransactionId);

            var response = requestExecutor.VoidTransaction(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }
    }
}
