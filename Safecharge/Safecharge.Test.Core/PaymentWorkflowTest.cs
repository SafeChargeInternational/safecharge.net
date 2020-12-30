using NUnit.Framework;
using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class PaymentWorkflowTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestSimplePaymentSuccess()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard);

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
            Assert.IsNull(paymentResponse.GwErrorReason);
            Assert.IsNull(paymentResponse.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, paymentResponse.TransactionStatus);
        }

        [Test]
        public void TestPaymentSuccess()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard)
            {
                BillingAddress = userAddress,
                DeviceDetails = new DeviceDetails { IpAddress = "93.146.254.172" },
                AmountDetails = new AmountDetails { TotalDiscount = "0", TotalHandling = "0", TotalShipping = "0", TotalTax = "0" },
                Items = items
            };

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
            Assert.IsNull(paymentResponse.GwErrorReason);
            Assert.IsNull(paymentResponse.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, paymentResponse.TransactionStatus);
        }

        [Test]
        public void TestSimpleUPORequestPaymentSuccess()
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard)
            {
                UserTokenId = "MusalaTestUser",
                BillingAddress = userAddress,
                DeviceDetails = new DeviceDetails { IpAddress = "93.146.254.172" },
                AmountDetails = new AmountDetails { TotalDiscount = "0", TotalHandling = "0", TotalShipping = "0", TotalTax = "0" },
                Items = items
            };

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var getSessionTokenRequest = new GetSessionTokenRequest(merchantInfo);

            var response = requestExecutor.GetSessionToken(getSessionTokenRequest).GetAwaiter().GetResult();
            sessionToken = response.SessionToken;
            var paymentOptionUpo = new PaymentOption
            {
                UserPaymentOptionId = paymentResponse.PaymentOption.UserPaymentOptionId
            };

            var paymentUPORequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionUpo)
            {
                UserTokenId = "MusalaTestUser",
                Items = items
            };

            var paymentUPOResponse = requestExecutor.Payment(paymentUPORequest).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentUPOResponse);
            Assert.IsEmpty(paymentUPOResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentUPOResponse.Status);
            Assert.IsNull(paymentUPOResponse.GwErrorReason);
            Assert.IsNull(paymentUPOResponse.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, paymentUPOResponse.TransactionStatus);
        }
    }
}
