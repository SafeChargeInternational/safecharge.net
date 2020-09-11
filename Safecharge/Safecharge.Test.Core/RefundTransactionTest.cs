using NUnit.Framework;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class RefundTransactionTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestRefundTransactionSuccess()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard) { Items = items };

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var request = new RefundTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                paymentResponse.TransactionId)
            { AuthCode = paymentResponse.AuthCode };

            var response = requestExecutor.RefundTransaction(request).GetAwaiter().GetResult();
            
            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestSimpleSalePaymentWithPartialRefund()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard) { Items = items };

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var request = new RefundTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                "2",
                paymentResponse.TransactionId);

            var response = requestExecutor.RefundTransaction(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }
    }
}
