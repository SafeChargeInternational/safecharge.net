using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Model.PaymentOptionModels.CardModels;
using Safecharge.Model.PaymentOptionModels.InitPayment;
using Safecharge.Model.PaymentOptionModels.ThreeDModels;
using Safecharge.Request;
using Safecharge.Response.Common;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class DocumentationSamplesDataTest : SafechargeBaseTest
    {
        private static JsonSerializerSettings SerializerSettings =>
           new JsonSerializerSettings
           {
               DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
               ContractResolver = new CamelCasePropertyNamesContractResolver(),
               Formatting = Formatting.Indented
           };

        [Test]
        public void TestGetSessionTokenSample()
        {
            var requestExecutor = new SafechargeRequestExecutor();

            var merchantInfo = new MerchantInfo(
                MerchantKeyValue,
                MerchantIdValue,
                MerchantSiteIdValue,
                ServerHostValue,
                HashAlgorithmType.SHA256);

            var getSessionTokenRequest = new GetSessionTokenRequest(merchantInfo);

            var response = requestExecutor.GetSessionToken(getSessionTokenRequest).GetAwaiter().GetResult();

            SaveResponse(response, "GetSessionTokenResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
        }

        [Test]
        public void TestOpenOrderSample()
        {
            var response = safecharge.OpenOrder(
                "USD",
                "10",
                clientUniqueId: "12345",
                deviceDetails: new DeviceDetails { IpAddress = "10.12.13.14" }).GetAwaiter().GetResult();

            SaveResponse(response, "OpenOrderResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
        }

        [Test]
        public void TestPaymentSimpleCardSample()
        {
            var response = safecharge.Payment(
                "USD",
                "10",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "4000023104662535",
                        CardHolderName = "John Smith",
                        ExpirationMonth = "12",
                        ExpirationYear = "22",
                        CVV = "217"
                    }
                },
                clientRequestId: "1C6CT7V1L",
                userTokenId: "230811147",
                clientUniqueId: "12345",
                orderId: "34383481",
                deviceDetails: new DeviceDetails { IpAddress = "93.146.254.172" }).GetAwaiter().GetResult();

            SaveResponse(response, "PaymentSimpleCardResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestPaymentFullCardSample()
        {
            var response = safecharge.Payment(
                "USD",
                "10",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "5115806139808464",
                        CardHolderName = "test name",
                        ExpirationMonth = "01",
                        ExpirationYear = "20",
                        CVV = "122",
                        ThreeD = new ThreeD
                        {
                            Version = "2",
                            NotificationURL = "https://www.merchant.com/notificationURL/",
                            MerchantURL = "https://www.merchant-website.com",
                            PlatformType = "02",
                            V2AdditionalParams = new V2AdditionalParams
                            {
                                ChallengePreference = "1",
                                DeliveryEmail = "deliveryEmail@somedomain.com",
                                DeliveryTimeFrame = "1",
                                GiftCardAmount = "456",
                                GiftCardCount = "10",
                                GiftCardCurrency = "826",
                                PreOrderDate = "20190219",
                                PreOrderPurchaseInd = "2",
                                ReorderItemsInd = "2",
                                ShipIndicator = "1",
                                RebillExpiry = "",
                                RebillFrequency = "",
                                ChallengeWindowSize = "2"
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
                            }
                        }
                    }
                },
                isRebilling: 0,
                orderId: "34383481",
                clientUniqueId: "12345",
                clientRequestId: "1C6CT7V1L",
                userTokenId: "230811147",
                deviceDetails: new DeviceDetails { IpAddress = "127.0.0.1" },
                userDetails: new CashierUserDetails
                {
                    FirstName = "first_name",
                    LastName = "last_name",
                    Email = "email@email.com",
                    Phone = "phone"
                },
                shippingAddress: new UserAddress
                {
                    Address = "address",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Zip = "1340",
                },
                billingAddress: new UserAddress
                {
                    Email = "asd@asdasd.com",
                    Address = "address",
                    City = "city",
                    Country = "DE",
                    State = "",
                    Zip = "1335"
                },
                dynamicDescriptor: new DynamicDescriptor { MerchantName = "merchant Name", MerchantPhone = "Phone" },
                merchantDetails: new MerchantDetails { CustomField1 = "customField1" }).GetAwaiter().GetResult();

            SaveResponse(response, "PaymentFullCardResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreEqual(ApiConstants.TransactionStatusDeclined, response.TransactionStatus);
        }

        [Test]
        public void TestPaymentApmRedirectSample()
        {
            var response = safecharge.Payment(
                "USD",
                "10",
                new PaymentOption
                {
                    AlternativePaymentMethod = new Dictionary<string, string>()
                    {
                        { "paymentMethod", "apmgw_MoneyBookers" },
                        { "account_id", "SkrillTestUser3" }
                    }
                },
                clientUniqueId: "12345",
                clientRequestId: "1C6CT7V1L",
                userTokenId: "230811147",
                orderId: "34383481",
                billingAddress: new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                deviceDetails: new DeviceDetails { IpAddress = "93.146.254.172" }).GetAwaiter().GetResult();

            SaveResponse(response, "PaymentApmRedirectResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusDeclined, response.TransactionStatus);
        }

        [Test]
        public void TestPaymentApmDirectSample()
        {
            var response = safecharge.Payment(
                "USD",
                "10",
                new PaymentOption
                {
                    AlternativePaymentMethod = new Dictionary<string, string>()
                    {
                        { "paymentMethod", "apmgw_Neteller" },
                        { "nettelerAccount", "453313818311" },
                        { "nettelerSecureId", "173419" }
                    }
                },
                clientUniqueId: "12345",
                clientRequestId: "1C6CT7V1L",
                userTokenId: "230811147",
                orderId: "34383481",
                billingAddress: new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com"
                },
                deviceDetails: new DeviceDetails { IpAddress = "93.146.254.172" }).GetAwaiter().GetResult();

            SaveResponse(response, "PaymentApmDirectResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusDeclined, response.TransactionStatus);
        }

        //1st 3D
        //2nd 3D
        [Test]
        public void TestPayment1st3dSample()
        {
            var response = safecharge.Payment(
                "USD",
                "10",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "5111426646345761",
                        CardHolderName = "john smith",
                        ExpirationMonth = "12",
                        ExpirationYear = "25",
                        CVV = "217",
                        ThreeD = new ThreeD
                        {
                            MethodCompletionInd = "Y",
                            Version = "2.1.0",
                            NotificationURL = "wwww.Test-Notification-URL-After-The-Challange-Is-Complete-Which-Recieves-The-CRes-Message.com",
                            MerchantURL = "www.The-Merchant-Website-Fully-Quallified-URL.com",
                            PlatformType = "02",
                            V2AdditionalParams = new V2AdditionalParams
                            {
                                ChallengePreference = "02",
                                DeliveryEmail = "The_Email_Address_The_Merchandise_Was_Delivered@yoyoyo.com",
                                DeliveryTimeFrame = "03",
                                GiftCardAmount = "1",
                                GiftCardCount = "41",
                                GiftCardCurrency = "USD",
                                PreOrderDate = "20220511",
                                PreOrderPurchaseInd = "02",
                                ReorderItemsInd = "01",
                                ShipIndicator = "06",
                                RebillExpiry = "20200101", //in case of recurring
                                RebillFrequency = "13", //in case of recurring
                                ChallengeWindowSize = "05"
                            },
                            BrowserDetails = new BrowserDetails // collected on the 3D fingerprinting
                            {
                                AcceptHeader = "Y",
                                Ip = "190.0.23.160",
                                JavaEnabled = "TRUE",
                                JavaScriptEnabled = "TRUE",
                                Language = "BG",
                                ColorDepth = "64",
                                ScreenHeight = "1024",
                                ScreenWidth = "1024",
                                TimeZone = "+3",
                                UserAgent = "Mozilla"
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
                            },
                            Acquirer = new Acquirer
                            {
                                Bin = "665544",
                                MerchantId = "9876556789",
                                MerchantName = "Acquirer Merchant Name",
                            }

                        }
                    }
                },
                clientUniqueId: "12345",
                clientRequestId: "1C6CT7V1L",
                userTokenId: "230811147",
                orderId: "34383481",
                relatedTransactionId: "2110000000001208909", // as returned from initPayment
                shippingAddress: new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com",
                },
                billingAddress: new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com",
                },
                deviceDetails: new DeviceDetails { IpAddress = "93.146.254.172" },
                dynamicDescriptor: new DynamicDescriptor { MerchantName = "Sample Merchant Name", MerchantPhone = "359889526524" },
                merchantDetails: new MerchantDetails { CustomField1 = "customField1-valueU" },
                productId: "5RS2LR2HUVGB").GetAwaiter().GetResult();

            SaveResponse(response, "Payment1st3dResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreEqual(ApiConstants.TransactionStatusDeclined, response.TransactionStatus);
        }

        [Test]
        public void TestPayment2nd3dSample()
        {
            var response = safecharge.Payment(
                "USD",
                "10",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "5111426646345761",
                        CardHolderName = "john smith",
                        ExpirationMonth = "12",
                        ExpirationYear = "25",
                        CVV = "217"
                    }
                },
                clientUniqueId: "12345",
                clientRequestId: "1C6CT7V1L",
                userTokenId: "230811147",
                orderId: "34383481",
                relatedTransactionId: "2110000000001209192", // as returned from 1st payment call
                billingAddress: new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com",
                },
                shippingAddress: new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com",
                },
                deviceDetails: new DeviceDetails { IpAddress = "93.146.254.172" }).GetAwaiter().GetResult();

            SaveResponse(response, "Payment2nd3dResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreEqual(ApiConstants.TransactionStatusDeclined, response.TransactionStatus);
        }

        [Test]
        public void TestPaymentExternalMpiSample()
        {
            var response = safecharge.Payment(
                "USD",
                "10",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "5111426646345761",
                        CardHolderName = "john smith",
                        ExpirationMonth = "12",
                        ExpirationYear = "25",
                        CVV = "217",
                        ThreeD = new ThreeD
                        {
                            ExternalMpi = new ExternalMpi
                            {
                                Eci = "2",
                                ThreeDProtocolVersion = "2",
                                Cavv = "ejJRWG9SWWRpU2I1M21DelozSXU",
                                DsTransID = "9e6c6e9b-b390-4b11-ada9-0a8f595e8600", //xid in case of 3Dv1 
                            }
                        }
                    }
                },
                clientUniqueId: "12345",
                clientRequestId: "1C6CT7V1L",
                userTokenId: "230811147",
                orderId: "34383481",
                billingAddress: new UserAddress
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "340689 main St.",
                    City = "London",
                    Country = "GB",
                    Email = "john.smith@safecharge.com",
                },
                deviceDetails: new DeviceDetails { IpAddress = "93.146.254.172" }).GetAwaiter().GetResult();

            SaveResponse(response, "PaymentExternalMpiResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreEqual(ApiConstants.TransactionStatusDeclined, response.TransactionStatus);
        }

        [Test]
        public void TestSettleTransactionSample()
        {
            var paymentResponse = safecharge
                .Payment("EUR", "9.0", new PaymentOption { Card = card }, transactionType: ApiConstants.TransactionTypeAuth)
                .GetAwaiter()
                .GetResult();

            var response = safecharge.SettleTransaction(
                "EUR",
                "9.0",
                paymentResponse.TransactionId, // returned from the payment auth
                clientRequestId: "1C6CT7V1L",
                clientUniqueId: "12345",
                descriptorMerchantName: "Name",
                descriptorMerchantPhone: "+4412378",
                comment: "some comment",
                urlDetails: new UrlDetails { NotificationUrl = "" },
                productId: "",
                customData: "").GetAwaiter().GetResult();

            SaveResponse(response, "SettleTransactionResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestRefundTransactionSample()
        {
            var paymentResponse = safecharge.Payment("EUR", "9.0", new PaymentOption { Card = card }).GetAwaiter().GetResult();

            var response = safecharge.RefundTransaction(
                "EUR",
                "9.0",
                paymentResponse.TransactionId, // returned from the payment
                clientUniqueId: "12345",
                authCode: paymentResponse.AuthCode, // returned from the payment
                comment: "some comment",
                urlDetails: new UrlDetails { NotificationUrl = "http://notificationUrl.com" },
                productId: "",
                customData: "").GetAwaiter().GetResult();

            SaveResponse(response, "RefundTransactionResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestVoidTransactionSample()
        {
            var paymentResponse = safecharge
                .Payment("EUR", "9.0", new PaymentOption { Card = card })
                .GetAwaiter()
                .GetResult();

            var response = safecharge.VoidTransaction(
                "EUR",
                "9.0",
                paymentResponse.TransactionId, // returned from the payment
                clientUniqueId: "12345",
                authCode: paymentResponse.AuthCode, // returned from the payment
                comment: "some comment",
                urlDetails: new UrlDetails { NotificationUrl = "http://notificationUrl.com" },
                productId: "",
                customData: "").GetAwaiter().GetResult();

            SaveResponse(response, "VoidTransactionResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestPayoutSample()
        {
            var paymentResponse = safecharge.Payment(currency, amount, new PaymentOption { Card = card }, userTokenId: "487106").GetAwaiter().GetResult();

            var response = safecharge.Payout(
                "487106",
                "12345",
                "9.0",
                "EUR",
                new UserPaymentOption { UserPaymentOptionId = paymentResponse.PaymentOption.UserPaymentOptionId },
                dynamicDescriptor: new DynamicDescriptor { MerchantName = "merchantName", MerchantPhone = "Phone" },
                merchantDetails: new MerchantDetails { CustomField1 = "" },
                comment: "some comment",
                urlDetails: new UrlDetails { NotificationUrl = "" },
                deviceDetails: new DeviceDetails { IpAddress = "127.0.0.1" }).GetAwaiter().GetResult();

            SaveResponse(response, "PayoutResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestGetPaymentStatusSample()
        {
            _ = safecharge.Payment("GBP", "6", new PaymentOption { Card = card }).GetAwaiter().GetResult();

            var response = safecharge.GetPaymentStatus().GetAwaiter().GetResult();

            SaveResponse(response, "GetPaymentStatusResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestGetCardDetailsSample()
        {
            var response = safecharge.GetCardDetails(
                "695701003",
                "4017356934955740").GetAwaiter().GetResult();

            SaveResponse(response, "GetCardDetailsResponse.json");

            Assert.IsNotNull(response);
            Assert.IsNull(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
        }

        [Test]
        public void TestInitPaymentSample()
        {
            var response = safecharge.InitPayment(
                "USD",
                "500",
                new InitPaymentPaymentOption
                {
                    Card = new InitPaymentCard
                    {
                        CardNumber = "5111426646345761",
                        CardHolderName = "CL-BRW1",
                        ExpirationMonth = "12",
                        ExpirationYear = "25",
                        CVV = "217",
                        ThreeD = new InitPaymentThreeD
                        {
                            MethodNotificationUrl = "www.ThisIsAMethodNotificationURL.com"
                        }
                    }
                },
                userTokenId: "230811147",
                orderId: "33704071",
                clientUniqueId: "695701003",
                billingAddress: new UserAddress
                {
                    City = "Billing City",
                    Country = "DE",
                    Address = "340689 Billing Str.",
                    Zip = "48957",
                    State = "",
                    Email = "mhsbg.xxnbx@udapl.rgm",
                    Phone = "359888526527",
                    Cell = "359881526527",
                    FirstName = "Zilsijihrw",
                    LastName = "Jgethizxrr",
                    County = "TTT"
                },
                deviceDetails: new DeviceDetails { IpAddress = "127.0.0.1" },
                urlDetails: new UrlDetails { NotificationUrl = "http://srv-bsf-devppptrunk.gw-4u.com/ppptest/NotifyMerchantTest" },
                customData: "merchant custom data").GetAwaiter().GetResult();

            SaveResponse(response, "InitPaymentResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestAuthorize3dSample()
        {
            var initPaymentResponse = safecharge.InitPayment(
                "EUR",
                "10",
                new InitPaymentPaymentOption
                {
                    Card = new InitPaymentCard
                    {
                        CardNumber = "5115806139808464",
                        CardHolderName = "test name",
                        ExpirationMonth = "01",
                        ExpirationYear = "20",
                        CVV = "122",
                        ThreeD = new InitPaymentThreeD
                        {
                            MethodNotificationUrl = "https://www.merchant.com/notificationURL/",
                        }
                    }
                },
                clientUniqueId: "uniqueIdCC").GetAwaiter().GetResult();

            var paymentOption = new PaymentOption
            {
                Card = new Card
                {
                    CardNumber = "5115806139808464",
                    CardHolderName = "test name",
                    ExpirationMonth = "01",
                    ExpirationYear = "20",
                    CVV = "122",
                    ThreeD = new ThreeD
                    {
                        Acquirer = new Acquirer { Bin = "411111", MerchantId = "123456789", MerchantName = "Merchant Inc." },
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
                            ChallengePreference = "1",
                            DeliveryEmail = "deliveryEmail@somedomain.com",
                            DeliveryTimeFrame = "1",
                            GiftCardAmount = "456",
                            GiftCardCount = "10",
                            GiftCardCurrency = "826",
                            PreOrderDate = "20190219",
                            PreOrderPurchaseInd = "2",
                            ReorderItemsInd = "2",
                            ShipIndicator = "1",
                            RebillExpiry = "",
                            RebillFrequency = "",
                            ChallengeWindowSize = "2"
                        },
                        NotificationURL = "https://www.merchant.com/notificationURL/",
                        MerchantURL = "https://www.merchant-website.com",
                        PlatformType = "02"
                    }
                }
            };
            var response = safecharge.Authorize3d(
                "EUR",
                "10",
                paymentOption,
                initPaymentResponse.TransactionId,
                clientUniqueId: "uniqueIdCC",
                deviceDetails: new DeviceDetails { IpAddress = "127.0.0.1" },
                shippingAddress: new UserAddress
                {
                    Address = "address",
                    City = "city",
                    Country = "DE",
                    State = "",
                    Zip = "1340",
                },
                billingAddress: new UserAddress
                {
                    Address = "address",
                    City = "city",
                    Country = "DE",
                    State = "",
                    Zip = "1335",
                },
                merchantDetails: new MerchantDetails { CustomField1 = "customField1" }).GetAwaiter().GetResult();
            SaveResponse(response, "Authorize3dResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        // Test for Verify3d is missing, because of the the challenge process 
        [Test]
        public void TestGetMerchantPaymentMethodsSample()
        {
            var response = safecharge.GetMerchantPaymentMethods(
                "1484759782197",
                currencyCode: "GBP",
                countryCode: "GB",
                type: ApiConstants.PaymentMethodTypeDeposit,
                languageCode: "en").GetAwaiter().GetResult();

            SaveResponse(response, "GetMerchantPaymentMethodsResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
        }

        private static void SaveResponse(SafechargeResponse response, string fileName)
        {
            var paymentResponseJson = JsonConvert.SerializeObject(response, SerializerSettings);
            var dirPath = Directory.GetCurrentDirectory() + "/Response";
            _ = Directory.CreateDirectory(dirPath);
            string filePath = Path.Combine(dirPath, fileName);
            File.WriteAllText(filePath, paymentResponseJson);
        }
    }
}
