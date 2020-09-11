using NUnit.Framework;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class SettleTransactionTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestSettleTransactionSuccess()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard)
            {
                TransactionType = ApiConstants.TransactionTypeAuth,
                Items = items
            };

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var request = new SettleTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                paymentResponse.TransactionId);

            var response = requestExecutor.SettleTransaction(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestSettleTransactionWithPaymentTransactionTypeSale()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard) { Items = items };

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var request = new SettleTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                paymentResponse.TransactionId);

            var response = requestExecutor.SettleTransaction(request).GetAwaiter().GetResult();
 
            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.AreEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
            Assert.AreEqual("Auth Code/Trans ID/Credit Card Number Mismatch", response.GwErrorReason);
        }
    }
}
