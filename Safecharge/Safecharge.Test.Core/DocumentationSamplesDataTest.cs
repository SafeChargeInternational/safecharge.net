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
                userTokenId: "354687",
                clientUniqueId: "12334565",
                clientRequestId: "78789789",
                deviceDetails: new DeviceDetails { IpAddress = "192.168.1.54" }).GetAwaiter().GetResult();

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
                "10.02",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "4000020951595032",
                        CardHolderName = "Jane Smith",
                        ExpirationMonth = "01",
                        ExpirationYear = "24",
                        CVV = "123"
                    }
                },
                items: new List<Item>
                {
                    new Item
                    {
                        Name = "Item 1U",
                        Price = "0.01",
                        Quantity = "1"
                    },
                    new Item
                    {
                        Name = "testItem1",
                        Price = "10.01",
                        Quantity = "1"
                    }
                }).GetAwaiter().GetResult();

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
                "EUR",
                "10",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "4012001037141112",
                        CardHolderName = "some name",
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
                shippingAddress: new UserAddress
                {
                    FirstName = "some first name",
                    LastName = "some last name",
                    Address = "address",
                    Phone = "972502457558",
                    Zip = "123456",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Email = "someemail@somedomain.com",
                    County = "Anchorage"
                },
                dynamicDescriptor: new DynamicDescriptor { MerchantName = "merchant Name", MerchantPhone = "Phone" },
                billingAddress: new UserAddress
                {
                    FirstName = "some first name",
                    LastName = "some last name",
                    Address = "address",
                    Phone = "972502457558",
                    Zip = "123456",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Email = "someemail@somedomain.com",
                    County = "Anchorage"
                },
                deviceDetails: new DeviceDetails { IpAddress = "192.168.1.54" }).GetAwaiter().GetResult();

            SaveResponse(response, "PaymentFullCardResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestPaymentApmRedirectSample()
        {
            var response = safecharge.Payment(
                "EUR",
                "10",
                new PaymentOption
                {
                    AlternativePaymentMethod = new Dictionary<string, string>()
                    {
                        { "paymentMethod", "apmgw_MoneyBookers" },
                        { "account_id", "SkrillTestUser3" }
                    }
                },
                userTokenId: "73fdd373b171299e692932ac01052001",
                billingAddress: new UserAddress
                {
                    FirstName = "some first name",
                    LastName = "some last name",
                    Address = "some street",
                    Phone = "972502457558",
                    Zip = "123456",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Email = "someemail@somedomain.com",
                    County = "Anchorage"
                },
                deviceDetails: new DeviceDetails { IpAddress = "192.168.1.54" }).GetAwaiter().GetResult();

            SaveResponse(response, "PaymentApmRedirectResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestPaymentApmDirectSample()
        {
            var response = safecharge.Payment(
                "EUR",
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
                userTokenId: "73fdd373b171299e692932ac01052001",
                billingAddress: new UserAddress
                {
                    FirstName = "some first name",
                    LastName = "some last name",
                    Address = "some street",
                    Phone = "972502457558",
                    Zip = "123456",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Email = "someemail@somedomain.com",
                    County = "Anchorage"
                },
                deviceDetails: new DeviceDetails { IpAddress = "192.168.1.54" }).GetAwaiter().GetResult();

            SaveResponse(response, "PaymentApmDirectResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        //1st 3D
        //2nd 3D
        [Test]
        public void TestPayment1st3dSample()
        {
            var response = safecharge.Payment(
                "EUR",
                "10",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "4012001037141112",
                        CardHolderName = "some name",
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
                                RebillExpiry = "", //in case of recurring
                                RebillFrequency = "", //in case of recurring
                                ChallengeWindowSize = "2"
                            },
                            BrowserDetails = new BrowserDetails // collected on the 3D fingerprinting
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
                },
                userTokenId: "73fdd373b171299e692932ac01052001",
                relatedTransactionId: "2110000000001208909", // as returned from initPayment
                shippingAddress: new UserAddress
                {
                    FirstName = "some first name",
                    LastName = "some last name",
                    Address = "some street",
                    Phone = "972502457558",
                    Zip = "123456",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Email = "someemail@somedomain.com",
                    County = "Anchorage"
                },
                billingAddress: new UserAddress
                {
                    FirstName = "some first name",
                    LastName = "some last name",
                    Address = "some street",
                    Phone = "972502457558",
                    Zip = "123456",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Email = "someemail@somedomain.com",
                    County = "Anchorage"
                },
                deviceDetails: new DeviceDetails { IpAddress = "192.168.1.54" }).GetAwaiter().GetResult();

            SaveResponse(response, "Payment1st3dResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestPayment2nd3dSample()
        {
            var response = safecharge.Payment(
                "EUR",
                "10",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "4012001037141112",
                        CardHolderName = "some name",
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
                                RebillExpiry = "", //in case of recurring
                                RebillFrequency = "", //in case of recurring
                                ChallengeWindowSize = "2"
                            },
                            BrowserDetails = new BrowserDetails // collected on the 3D fingerprinting
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
                },
                userTokenId: "73fdd373b171299e692932ac01052001",
                relatedTransactionId: "2110000000001208909", // as returned from 1st payment call
                billingAddress: new UserAddress
                {
                    FirstName = "some first name",
                    LastName = "some last name",
                    Address = "some street",
                    Phone = "972502457558",
                    Zip = "123456",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Email = "someemail@somedomain.com",
                    County = "Anchorage"
                },
                deviceDetails: new DeviceDetails { IpAddress = "192.168.1.54" }).GetAwaiter().GetResult();

            SaveResponse(response, "Payment2nd3dResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestPaymentExternalMpiSample()
        {
            var response = safecharge.Payment(
                "EUR",
                "10",
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "4012001037141112",
                        CardHolderName = "some name",
                        ExpirationMonth = "01",
                        ExpirationYear = "20",
                        CVV = "122",
                        ThreeD = new ThreeD
                        {
                            ExternalMpi = new ExternalMpi
                            {
                                Eci = "2",
                                Cavv = "ejJRWG9SWWRpU2I1M21DelozSXU",
                                DsTransID = "9e6c6e9b-b390-4b11-ada9-0a8f595e8600", //xid in case of 3Dv1 
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
                },
                billingAddress: new UserAddress
                {
                    FirstName = "some first name",
                    LastName = "some last name",
                    Address = "address",
                    Phone = "972502457558",
                    Zip = "123456",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Email = "someemail@somedomain.com",
                    County = "Anchorage"
                },
                deviceDetails: new DeviceDetails { IpAddress = "192.168.1.54" }).GetAwaiter().GetResult();

            SaveResponse(response, "PaymentExternalMpiResponse.json");

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.IsNull(response.GwErrorReason);
            Assert.IsNull(response.PaymentMethodErrorReason);
            Assert.AreNotEqual(ApiConstants.TransactionStatusError, response.TransactionStatus);
        }

        [Test]
        public void TestSettleTransactionSample()
        {
            var paymentResponse = safecharge
                .Payment("GBP", "6.0", new PaymentOption { Card = card }, transactionType: ApiConstants.TransactionTypeAuth)
                .GetAwaiter()
                .GetResult();

            var response = safecharge.SettleTransaction(
                "GBP",
                "6.0",
                paymentResponse.TransactionId, // returned from the payment auth
                clientUniqueId: "695701003",
                descriptorMerchantName: "merchantName",
                descriptorMerchantPhone: "Phone",
                comment: "Comment",
                urlDetails: new UrlDetails { NotificationUrl = "https://example.com" },
                productId: "productId - 123",
                customData: "CustomData").GetAwaiter().GetResult();

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
            var paymentResponse = safecharge.Payment("GBP", "6.0", new PaymentOption { Card = card }).GetAwaiter().GetResult();

            var response = safecharge.RefundTransaction(
                "GBP",
                "6.0",
                paymentResponse.TransactionId, // returned from the payment
                clientUniqueId: "695701003",
                authCode: paymentResponse.AuthCode, // returned from the payment
                comment: "Comment",
                urlDetails: new UrlDetails { NotificationUrl = "https://www.safecharge.com/notification" },
                productId: "productId - 123",
                customData: "CustomData").GetAwaiter().GetResult();

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
                .Payment("GBP", "6.0", new PaymentOption { Card = card })
                .GetAwaiter()
                .GetResult();

            var response = safecharge.VoidTransaction(
                "GBP",
                "6.0",
                paymentResponse.TransactionId, // returned from the payment
                clientUniqueId: "695701003",
                authCode: paymentResponse.AuthCode, // returned from the payment
                comment: "Comment",
                urlDetails: new UrlDetails { NotificationUrl = "https://example.com" },
                productId: "productId - 123",
                customData: "CustomData").GetAwaiter().GetResult();

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
                comment: "Comment",
                urlDetails: new UrlDetails { NotificationUrl = "https://example.com" }).GetAwaiter().GetResult();

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
                "EUR",
                "10",
                new InitPaymentPaymentOption
                {
                    Card = new InitPaymentCard
                    {
                        CardNumber = "4012001037141112",
                        CardHolderName = "some name",
                        ExpirationMonth = "01",
                        ExpirationYear = "20",
                        CVV = "122"
                    }
                },
                userTokenId: "emilg@safecharge.com",
                billingAddress: new UserAddress
                {
                    FirstName = "some first name",
                    LastName = "some last name",
                    Address = "some street",
                    Phone = "972502457558",
                    Zip = "123456",
                    City = "some city",
                    Country = "DE",
                    State = "",
                    Email = "someemail@somedomain.com",
                    County = "Anchorage"
                }).GetAwaiter().GetResult();

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
