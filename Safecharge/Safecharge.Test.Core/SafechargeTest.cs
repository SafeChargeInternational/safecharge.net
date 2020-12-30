using NUnit.Framework;
using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class SafechargeTest : SafechargeBaseTest
    {
        [Test]
        public void TestSimplePaymentWithRequiredFieldsSuccess()
        {
            var paymentResponse = safecharge.Payment(currency, amount, new PaymentOption { Card = card }, items: items).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
        }

        [Test]
        public void TestPaymentSuccess()
        {
            var paymentResponse = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items:items,
                billingAddress: userAddress).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
        }

        [Test]
        public void TestAlreadyConsumedSessionToken()
        {
            var paymentResponse = safecharge.Payment(
                currency, 
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);

            var secondPaymentResponse = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress).GetAwaiter().GetResult();

            Assert.IsNotNull(secondPaymentResponse);
            Assert.AreEqual("Requested operation can not be performed on this order", secondPaymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Error, secondPaymentResponse.Status);
        }

        [Test]
        public void TestSettleTransactionSuccess()
        {
            var paymentResponse = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress,
                transactionType: ApiConstants.TransactionTypeAuth).GetAwaiter().GetResult();

            var transactionResponse = safecharge.SettleTransaction(
                currency,
                amount,
                paymentResponse.TransactionId).GetAwaiter().GetResult();

            Assert.IsNotNull(transactionResponse);
            Assert.IsEmpty(transactionResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, transactionResponse.Status);
        }

        [Test]
        public void TestSettleTransactionWithAuthCodeSuccess()
        {
            var paymentResponse = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress,
                transactionType: ApiConstants.TransactionTypeAuth).GetAwaiter().GetResult();

            var transactionResponse = safecharge.SettleTransaction(
                currency,
                amount,
                paymentResponse.TransactionId,
                authCode: paymentResponse.AuthCode).GetAwaiter().GetResult();

            Assert.IsNotNull(transactionResponse);
            Assert.IsEmpty(transactionResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, transactionResponse.Status);
        }

        [Test]
        public void TestSettleTransactionWithDynamicDescriptorSuccess()
        {
            var paymentResponse = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress,
                transactionType: ApiConstants.TransactionTypeAuth).GetAwaiter().GetResult();

            var transactionResponse = safecharge.SettleTransaction(
                currency,
                amount,
                paymentResponse.TransactionId,
                dynamicDescriptor: new DynamicDescriptor { MerchantName = "MerchantName", MerchantPhone = "3598885111111" }).GetAwaiter().GetResult();

            Assert.IsNotNull(transactionResponse);
            Assert.IsEmpty(transactionResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, transactionResponse.Status);
        }

        [Test]
        public void TestSettleTransactionWithCommentSuccess()
        {
            var paymentResponse = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress,
                transactionType: ApiConstants.TransactionTypeAuth).GetAwaiter().GetResult();

            var transactionResponse = safecharge.SettleTransaction(
                currency,
                amount,
                paymentResponse.TransactionId,
                comment: "Comment").GetAwaiter().GetResult();

            Assert.IsNotNull(transactionResponse);
            Assert.IsEmpty(transactionResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, transactionResponse.Status);
        }

        [Test]
        public void TestVoidTransactionSuccess()
        {
            var paymentResponse = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress).GetAwaiter().GetResult();

            var transactionResponse = safecharge.VoidTransaction(
                currency,
                amount,
                paymentResponse.TransactionId).GetAwaiter().GetResult();

            Assert.IsNotNull(transactionResponse);
            Assert.IsEmpty(transactionResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, transactionResponse.Status);
        }

        [Test]
        public void TestVoidTransactionWithAuthCodeSuccess()
        {
            var paymentResponse = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress,
                transactionType: ApiConstants.TransactionTypeAuth).GetAwaiter().GetResult();

            var transactionResponse = safecharge.VoidTransaction(
                currency,
                amount,
                paymentResponse.TransactionId,
                authCode: paymentResponse.AuthCode).GetAwaiter().GetResult();

            Assert.IsNotNull(transactionResponse);
            Assert.IsEmpty(transactionResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, transactionResponse.Status);
        }

        [Test]
        public void TestRefundTransactionSuccess()
        {
            var paymentResponse = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress).GetAwaiter().GetResult();

            var transactionResponse = safecharge.RefundTransaction(
                currency,
                amount,
                paymentResponse.TransactionId).GetAwaiter().GetResult();

            Assert.IsNotNull(transactionResponse);
            Assert.IsEmpty(transactionResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, transactionResponse.Status);
        }

        [Test]
        public void TestGetPaymentStatusSuccess()
        {
            _ = safecharge.Payment(
                currency,
                amount,
                new PaymentOption { Card = card },
                items: items,
                billingAddress: userAddress).GetAwaiter().GetResult();

            var response = safecharge.GetPaymentStatus().GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
        }

        [Test]
        public void TestPayoutSuccess()
        {
            var response = safecharge.Payout(
                "MusalaTestUser",
                "12345",
                "9.0",
                "EUR",
                new UserPaymentOption { UserPaymentOptionId = "1459503" },
                dynamicDescriptor: new DynamicDescriptor { MerchantName = "merchantName", MerchantPhone = "Phone" },
                merchantDetails: new MerchantDetails { CustomField1 = "" },
                comment: "Comment",
                urlDetails: new UrlDetails { NotificationUrl = "https://example.com" }).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }
    }
}