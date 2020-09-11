using NUnit.Framework;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class GetPaymentStatusTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestGetPaymentStatusSuccess()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard) { Items = items };
            _ = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var request = new GetPaymentStatusRequest(merchantInfo, sessionToken);

            var response = requestExecutor.GetPaymentStatus(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestGetPaymentStatusWithNoPayment()
        {
            var request = new GetPaymentStatusRequest(merchantInfo, sessionToken);

            var response = requestExecutor.GetPaymentStatus(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Reason, "No order is associated with the session token submitted in the request.");
            Assert.AreEqual(ResponseStatus.Error, response.Status);
        }
    }
}
