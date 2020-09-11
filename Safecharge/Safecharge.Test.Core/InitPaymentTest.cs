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
    public class InitPaymentTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestInitPaymentSuccess()
        {
            var paymentOptionInitPayment = new InitPaymentPaymentOption 
            { 
                Card = new InitPaymentCard 
                {
                    CardNumber = "5111426646345761",
                    CardHolderName = "CL-BRW1",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217"
                } 
            };

            var request = new InitPaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionInitPayment);

            var response = requestExecutor.InitPayment(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestRecurringWith3dSecureSuccess()
        {
            var paymentOptionInitPayment = new InitPaymentPaymentOption
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
                        MethodNotificationUrl = "www.ThisIsAMethodNotificationURL.com",
                    }
                }
            };

            var initPaymentRequest = new InitPaymentRequest(merchantInfo, sessionToken, "USD", "500", paymentOptionInitPayment)
            { 
                UserTokenId = "recurringUser"
            };

            var initPaymentResponse = requestExecutor.InitPayment(initPaymentRequest).GetAwaiter().GetResult();

            var po = new PaymentOption
            {
                Card = new Card
                {
                    CardNumber = "4000020951595032",
                    CardHolderName = "FL-BRW1",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217",
                    ThreeD = new ThreeD
                    {
                        MethodCompletionInd = "U",
                        Version = initPaymentResponse.PaymentOption.Card.ThreeD.Version,
                        V2AdditionalParams = new V2AdditionalParams
                        {
                            RebillExpiry = "20200101",
                            RebillFrequency = "13"
                        },
                        NotificationURL = "http://wwww.Test-Notification-URL-After-The-Challange-Is-Complete-Which-Recieves-The-CRes-Message.com",
                        MerchantURL = "http://www.The-Merchant-Website-Fully-Quallified-URL.com",
                        PlatformType = "02",
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
                        }
                    }
                }
            };
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, "USD", "500", po)
            { 
                RelatedTransactionId = initPaymentResponse.TransactionId,
                UserTokenId = "recurringUser",
                IsRebilling = 0,
                RebillingType = "RECURRING",
                BillingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                }, 
                ShippingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                DeviceDetails = new DeviceDetails { IpAddress = "93.146.254.172" }
            };
            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
            Assert.IsNull(paymentResponse.GwErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, paymentResponse.TransactionStatus);

            var getSessionTokenRequest = new GetSessionTokenRequest(merchantInfo);

            var getSessionTokenResponse = requestExecutor.GetSessionToken(getSessionTokenRequest).GetAwaiter().GetResult();
            sessionToken = getSessionTokenResponse.SessionToken;

            var recurringRequest = new PaymentRequest(merchantInfo, sessionToken, "USD", "10", new PaymentOption { UserPaymentOptionId = paymentResponse.PaymentOption.UserPaymentOptionId })
            {
                RelatedTransactionId = paymentResponse.TransactionId,
                IsRebilling = 1,
                UserTokenId = "recurringUser",
                BillingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                DeviceDetails = new DeviceDetails { IpAddress = "93.146.254.172" }
            };
            var recurringResponse = requestExecutor.Payment(recurringRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(recurringResponse);
            Assert.IsEmpty(recurringResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, recurringResponse.Status);
            Assert.IsNull(recurringResponse.GwErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, recurringResponse.TransactionStatus);
        }

        [Test]
        public void Test3dSecureChallengeFlowSuccess()
        {
            var paymentOptionInitPayment = new InitPaymentPaymentOption
            {
                Card = new InitPaymentCard
                {
                    CardNumber = "4000020951595032",
                    CardHolderName = "CL-BRW1",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217",
                    ThreeD = new InitPaymentThreeD
                    {
                        MethodNotificationUrl = "www.ThisIsAMethodNotificationURL.com",
                    }
                }
            };

            var initPaymentRequest = new InitPaymentRequest(merchantInfo, sessionToken, "USD", "500", paymentOptionInitPayment);
            var initPaymentResponse = requestExecutor.InitPayment(initPaymentRequest).GetAwaiter().GetResult();

            var po = new PaymentOption
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
                        MethodCompletionInd = "U",
                        Version = initPaymentResponse.PaymentOption.Card.ThreeD.Version,
                        V2AdditionalParams = new V2AdditionalParams
                        {
                            ChallengePreference = "01",
                            DeliveryEmail = "The_Email_Address_The_Merchandise_Was_Delivered@yoyoyo.com",
                            DeliveryTimeFrame = "03",
                            GiftCardAmount = "1",
                            GiftCardCount = "41",
                            GiftCardCurrency = "USD",
                            PreOrderDate = "20220511",
                            PreOrderPurchaseInd = "02",
                            ReorderItemsInd = "01",
                            ShipIndicator = "06",
                            RebillExpiry = "20200101",
                            RebillFrequency = "13",
                            ChallengeWindowSize = "05"
                        },
                        NotificationURL = "https://3dsecuresafecharge.000webhostapp.com/3Dv2/notificationUrl.php",
                        MerchantURL = "http://www.The-Merchant-Website-Fully-Quallified-URL.com",
                        PlatformType = "02",
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
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, "USD", "500", po)
            {
                RelatedTransactionId = initPaymentResponse.TransactionId,
                BillingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                ShippingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                DeviceDetails = new DeviceDetails { IpAddress = "93.146.254.172" }
            };
            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var acsUrl = paymentResponse.PaymentOption.Card.ThreeD.AcsUrl;
            var cReq = paymentResponse.PaymentOption.Card.ThreeD.CReq;
            var url = $"https://3dsecuresafecharge.000webhostapp.com/3Dv2/showUrl.php?acsUrl={acsUrl}&creq={cReq}";

            var paymentOptionC = new PaymentOption 
            {
                Card = new Card
                {
                    CardNumber = "4000020951595032",
                    CardHolderName = "CL-BRW1",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217",
                }
            };
            var liabilityShiftRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionC)
            {
                RelatedTransactionId = paymentResponse.TransactionId,
                TransactionType = ApiConstants.TransactionTypeAuth,
                BillingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                DeviceDetails = new DeviceDetails { IpAddress = "93.146.254.172" }
            };
            var liabilityShiftResponse = requestExecutor.Payment(liabilityShiftRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(liabilityShiftResponse);
            Assert.IsEmpty(liabilityShiftResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, liabilityShiftResponse.Status);
            Assert.IsNull(liabilityShiftResponse.GwErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, liabilityShiftResponse.TransactionStatus);
        }

        [Test]
        public void Test3dSecureFrictionlessFlowSuccess()
        {
            var paymentOptionInitPayment = new InitPaymentPaymentOption
            {
                Card = new InitPaymentCard
                {
                    CardNumber = "4000027891380961",
                    CardHolderName = "FL-BRW1",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217"
                }
            };

            var initPaymentRequest = new InitPaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionInitPayment);

            var initPaymentResponse = requestExecutor.InitPayment(initPaymentRequest).GetAwaiter().GetResult();

            var po = new PaymentOption
            {
                Card = new Card
                {
                    CardNumber = "4000027891380961",
                    CardHolderName = "FL-BRW1",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217",
                    ThreeD = new ThreeD
                    {
                        MethodCompletionInd = "U",
                        Version = initPaymentResponse.PaymentOption.Card.ThreeD.Version,
                        NotificationURL = "http://wwww.Test-Notification-URL-After-The-Challange-Is-Complete-Which-Recieves-The-CRes-Message.com",
                        MerchantURL = "http://www.The-Merchant-Website-Fully-Quallified-URL.com",
                        PlatformType = "02",
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
                        Account = new Account
                        {
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
                            NameInd = "02",
                            SuspiciousActivityInd = "01"
                        }
                    }
                }
            };
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, po)
            {
                RelatedTransactionId = initPaymentResponse.TransactionId,
                BillingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                ShippingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                DeviceDetails = new DeviceDetails { IpAddress = "93.146.254.172" }
            };
            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
            Assert.IsNull(paymentResponse.GwErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, paymentResponse.TransactionStatus);
        }

        [Test]
        public void Test3dSecureV1FallbackFlowSuccess()
        {
            var paymentOptionInitPayment = new InitPaymentPaymentOption
            {
                Card = new InitPaymentCard
                {
                    CardNumber = "4012001037490014",
                    CardHolderName = "john smith",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217",
                    ThreeD = new InitPaymentThreeD
                    {
                        MethodNotificationUrl = "www.ThisIsAMethodNotificationURL.com",
                    }
                }
            };

            var initPaymentRequest = new InitPaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionInitPayment);

            var initPaymentResponse = requestExecutor.InitPayment(initPaymentRequest).GetAwaiter().GetResult();

            var po = new PaymentOption
            {
                Card = new Card
                {
                    CardNumber = "4012001037490014",
                    CardHolderName = "asd asdas",
                    ExpirationMonth = "12",
                    ExpirationYear = "25",
                    CVV = "217",
                    ThreeD = new ThreeD
                    {
                    }
                }
            };
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, po)
            {
                RelatedTransactionId = initPaymentResponse.TransactionId,
                BillingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                ShippingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                DeviceDetails = new DeviceDetails { IpAddress = "93.146.254.172" }
            };
            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            var acsUrl = paymentResponse.PaymentOption.Card.ThreeD.AcsUrl;
            var paRequest = paymentResponse.PaymentOption.Card.ThreeD.PaRequest;
            
            var paResponse = ""; // result from paRequest
            po.Card.ThreeD.PaResponse = paResponse;

            var liabilityShiftRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, po)
            {
                BillingAddress = new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                DeviceDetails = new DeviceDetails { IpAddress = "93.146.254.172" }
            };
            var liabilityShiftResponse = requestExecutor.Payment(liabilityShiftRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(liabilityShiftResponse);
            Assert.IsEmpty(liabilityShiftResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, liabilityShiftResponse.Status);
            Assert.IsNull(liabilityShiftResponse.GwErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, liabilityShiftResponse.TransactionStatus);
        }
    }
}
