using NUnit.Framework;
using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Model.PaymentOptionModels.CardModels;
using Safecharge.Model.PaymentOptionModels.InitPayment;
using Safecharge.Model.PaymentOptionModels.ThreeDModels;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class ThreeDWorkflowsTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestAuthorize3dSuccess()
        {
            var initPaymentPaymentOption = new InitPaymentPaymentOption
            {
                Card = new InitPaymentCard
                {
                    CardNumber = "4000020951595032",
                    CardHolderName = "FL-BRW1",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217",
                    ThreeD = new InitPaymentThreeD
                    {
                        MethodNotificationUrl = "www.ThisIsAMethodNotificationURL.com"
                    }
                }
            };

            var initPaymentRequest = new InitPaymentRequest(merchantInfo, sessionToken, "USD", "10", initPaymentPaymentOption) 
            {
                UserTokenId = "asdasd"
            };

            var initPaymentResponse = requestExecutor.InitPayment(initPaymentRequest).GetAwaiter().GetResult();

            var authorize3dPaymentOption = new PaymentOption
            {
                Card = new Card
                {
                    CardNumber = "4000020951595032",
                    CardHolderName = "CL-BRW1",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217",
                    ThreeD = new ThreeD
                    {
                        Acquirer = new Acquirer { Bin = "123", MerchantId = "Musala", MerchantName = "Musala" },
                        MethodCompletionInd = "U",
                        BrowserDetails = new BrowserDetails
                        {
                            AcceptHeader = "text/html,application/xhtml+xml",
                            Ip = "192.168.1.11",
                            JavaEnabled = "TRUE",
                            JavaScriptEnabled = "TRUE",
                            Language = "EN",
                            ColorDepth = "48",
                            ScreenHeight = "400",
                            ScreenWidth = "600",
                            TimeZone = "0",
                            UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47"
                        },
                        Version = initPaymentResponse.PaymentOption.Card.ThreeD.Version,
                        V2AdditionalParams = new V2AdditionalParams
                        {
                            ChallengePreference = "01",
                            DeliveryEmail = "deliveryEmail@somedomain.com",
                            DeliveryTimeFrame = "03", 
                            GiftCardAmount = "1",
                            GiftCardCount = "41",
                            GiftCardCurrency = "USD",
                            PreOrderDate = "20220511",
                            PreOrderPurchaseInd = "02",
                            ReorderItemsInd = "02",
                            ShipIndicator = "06",
                            RebillExpiry = "20200101",
                            RebillFrequency = "13",
                            ChallengeWindowSize = "05"
                        },
                        NotificationURL = "http://wwww.Test-Notification-URL-After-The-Challange-Is-Complete-Which-Recieves-The-CRes-Message.com",
                        MerchantURL = "http://www.The-Merchant-Website-Fully-Quallified-URL.com",
                        PlatformType = "02",
                        Account = new Account
                        {
                            NameInd = "02",
                            Age = "05",
                            LastChangeDate = "20190220",
                            LastChangeInd = "04",
                            RegistrationDate = "20190221",
                            PasswordChangeDate = "20190222",
                            ResetInd = "01",
                            PurchasesCount6M = "6",
                            AddCardAttepmts24H = "24",
                            TransactionsCount24H = "23",
                            TransactionsCount1Y = "998",
                            CardSavedDate = "20190223",
                            CardSavedInd = "02",
                            AddressFirstUseDate = "20190224",
                            AddressFirstUseInd = "03",
                            SuspiciousActivityInd = "01"
                        }
                    }
                }
            };
            var request = new Authorize3dRequest(merchantInfo, sessionToken, "USD", "10", authorize3dPaymentOption, initPaymentResponse.TransactionId)
            {
                DeviceDetails = new DeviceDetails { IpAddress = "192.168.1.54" },
                ShippingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com",
                },
                BillingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                TransactionType = ApiConstants.TransactionTypeAuth
            };

            var response = requestExecutor.Authorize3d(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

    }
}
