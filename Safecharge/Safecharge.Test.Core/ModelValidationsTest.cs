using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Safecharge.Model.Common;
using Safecharge.Model.Common.Addendum;
using Safecharge.Model.Payment;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Model.PaymentOptionModels.CardModels;
using Safecharge.Model.PaymentOptionModels.InitPayment;
using Safecharge.Model.PaymentOptionModels.OpenOrder;
using Safecharge.Model.PaymentOptionModels.ThreeDModels;
using Safecharge.Model.PaymentOptionModels.Verify3d;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class ModelValidationsTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestNullGuardInvalid()
        {
            ActualValueDelegate<object> createItemDelegate = () => new Item
            {
                Name = null,
                Price = "2",
                Quantity = "1"
            };

            Assert.That(createItemDelegate, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestRequiresLengthInvalid()
        {
            ActualValueDelegate<object> createItemDelegate = () => 
                new GetMerchantPaymentMethodsRequest(merchantInfo, sessionToken, "123")
                {
                    CurrencyCode = "1",
                    CountryCode = "GB",
                    LanguageCode = "en",
                    Type = ApiConstants.PaymentMethodTypeDeposit,
                };

            Assert.That(createItemDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestRequiresLengthValidNull()
        {
            _ = new GetMerchantPaymentMethodsRequest(merchantInfo, sessionToken, "123")
            {
                CurrencyCode = null,
                CountryCode = "GB",
                LanguageCode = "en",
                Type = ApiConstants.PaymentMethodTypeDeposit,
            };

            Assert.Pass();
        }

        [Test]
        public void TestRequiresBoolValid()
        {
            ActualValueDelegate<object> createDelegate = () =>
                new OpenOrderRequest(merchantInfo, sessionToken, currency, amount)
                {
                    IsRebilling = "0"
                };

            Assert.Pass();
        }

        [Test]
        public void TestRequiresBoolInvalid()
        {
            ActualValueDelegate<object> createDelegate = () =>
                new OpenOrderRequest(merchantInfo, sessionToken, currency, amount)
                {
                    IsRebilling = "5"
                };

            Assert.That(createDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestMaxGuardInvalid()
        {
            ActualValueDelegate<object> createItemDelegate = () => new Item
            {
                Name = "Test item",
                Price = "123456791011",
                Quantity = "1"
            };

            Assert.That(createItemDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestValueBetweenGuardMoreThanAllowed()
        {
            ActualValueDelegate<object> createCardDataDelegate = () => new CardData
            {
                CVV = "test5"
            };

            Assert.That(createCardDataDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestValueBetweenGuardLessThanAllowed()
        {
            ActualValueDelegate<object> createCardDataDelegate = () => new CardData
            {
                CVV = "12"
            };

            Assert.That(createCardDataDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestPatternGuardInvalid()
        {
            ActualValueDelegate<object> createDeviceDetailsDelegate = () => new DeviceDetails
            {
                IpAddress = "test"
            };

            Assert.That(createDeviceDetailsDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestRequiresAllowedValuesGuardInvalid()
        {
            ActualValueDelegate<object> createDelegate = () => new ExternalMpi
            {
                ThreeDProtocolVersion = "4"
            };

            Assert.That(createDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestRequiresAllowedValuesGuardValid()
        {
            _ = new ExternalMpi
            {
                ThreeDProtocolVersion = "2"
            };

            Assert.Pass();
        }

        [Test]
        public void TestDateFormatGuardInvalid()
        {
            ActualValueDelegate<object> createCashierUserDetailsDelegate = () => new CashierUserDetails
            {
                DateOfBirth = "test"
            }; ;

            Assert.That(createCashierUserDetailsDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestSettingNullsInAddendumsModel()
        {
            _ = new AddendumsLocalPayment
            {
                NationalId = null,
                DebitType = null,
                FirstInstallment = null,
                PeriodicalInstallment = null,
                NumberOfInstallments = null
            };

            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelAddendums()
        {
            var addendums = new Addendums
            {
                LocalPayment = new AddendumsLocalPayment
                {
                    NationalId = "FRA",
                    DebitType = "instalments",
                    FirstInstallment = "11",
                    PeriodicalInstallment = "11",
                    NumberOfInstallments = "121"
                },
                CardPresentPointOfSale = new AddendumsCardPresentPointOfSale
                {
                    TerminalId = "",
                    TrackData = "",
                    TrackType = "",
                    Icc = "",
                    PinData = "",
                    EntryMode = "",
                    TerminalCapability = "",
                    TerminalAttendance = "",
                    CardSequenceNum = "",
                    OfflineResCode = "",
                    LocalTime = "",
                    LocalDate = "",
                    CvMethod = "",
                    CvEntity = "",
                    OutputCapability = "",
                    AutoReversal = "",
                    AutoReversalAmount = "",
                    AutoReversalCurrency = "",
                    Channel = "",
                    SuppressAuth = "",
                    TerminalCity = "",
                    TerminalAddress = "",
                    TerminalCountry = "",
                    TerminalZip = "",
                    TerminalState = "",
                    TerminalModel = "",
                    TerminalManufacturer = "",
                    TerminalMacAddress = "",
                    TerminalKernel = "",
                    TerminalImei = "",
                    MobileTerminal = "",
                    TerminalType = "",
                    SecurityControl = ""
                },
                Airlines = new AddendumsAirlines
                {
                    ReservationDetails = new AddendumsAirlinesReservationDetails
                    {
                        AddendumSent = "",
                        PnrCode = "",
                        BookingSystemUniqueId = "",
                        ComputerizedReservationSystem = "",
                        TicketNumber = "",
                        DocumentType = "",
                        FlightDateUTC = "",
                        IssueDate = "",
                        TravelAgencyCode = "",
                        TravelAgencyName = "",
                        TravelAgencyInvoiceNumber = "",
                        TravelAgencyPlanName = "",
                        RestrictedTicketIndicator = "",
                        IssuingCarrierCode = "",
                        IsCardholderTraveling = "",
                        PassengersCount = "",
                        InfantsCount = "",
                        PayerPassportId = "",
                        TotalFare = "",
                        TotalTaxes = "",
                        TotalFee = "",
                        BoardingFee = "",
                        TicketIssueAddress = "",
                        PassengerDetails = new List<AddendumsAirlinesPassengerDetails>
                        {
                            new AddendumsAirlinesPassengerDetails
                            {
                                PassangerId = "",
                                PassportNumber = "",
                                CustomerCode = "",
                                FrequentFlyerCode = "",
                                Title = "",
                                FirstName = "",
                                LastName = "",
                                MiddleName = "",
                                DateOfBirth = "",
                                PhoneNumber = ""
                            }
                        },
                        FlightLegDetails = new List<AddendumsAirlinesFlightLegDetails>
                        {
                            new AddendumsAirlinesFlightLegDetails
                            {
                                FlightLegId = "",
                                AirlineCode = "",
                                FlightNumber = "",
                                DepartureDate = "",
                                ArrivalDate = "",
                                DepartureCountry = "",
                                DepartureCity = "",
                                DepartureAirport = "",
                                DestinationCountry = "",
                                DestinationCity = "",
                                DestinationAirport = "",
                                Type = "",
                                FlightType = "",
                                TicketDeliveryMethod = "",
                                TicketDeliveryRecipient = "",
                                FareBasisCode = "",
                                ServiceClass = "",
                                SeatClass = "",
                                StopOverCode = "",
                                DepartureTaxAmount = "",
                                DepartureTaxCurrency = "",
                                FareAmount = "",
                                FeeAmount = "",
                                TaxAmount = "",
                                LayoutIntegererval = ""
                            }
                        }
                    }
                }
            };

            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard)
            {
                Addendums = addendums
            };
            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
        }

        [Test]
        public void TestSettingNullsInMerchantDetailsModel()
        {
            _ = new MerchantDetails
            {
                CustomField1 = null,
                CustomField2 = null,
                CustomField3 = null,
                CustomField4 = null,
                CustomField5 = null,
                CustomField6 = null,
                CustomField7 = null,
                CustomField8 = null,
                CustomField9 = null,
                CustomField10 = null,
                CustomField11 = null,
                CustomField12 = null,
                CustomField13 = null,
                CustomField14 = null,
                CustomField15 = null
            };

            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelMerchantDetails()
        {
            var merchantDetails = new MerchantDetails
            {
                CustomField1 = "",
                CustomField2 = "",
                CustomField3 = "",
                CustomField4 = "",
                CustomField5 = "",
                CustomField6 = "",
                CustomField7 = "",
                CustomField8 = "",
                CustomField9 = "",
                CustomField10 = "",
                CustomField11 = "",
                CustomField12 = "",
                CustomField13 = "",
                CustomField14 = "",
                CustomField15 = ""
            };

            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard)
            {
                MerchantDetails = merchantDetails
            };
            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
        }

        [Test]
        public void TestSettingNullsInCardModel()
        {
            _ = new Card()
            {
                CardNumber = null,
                CardHolderName = null,
                ExpirationMonth = null,
                ExpirationYear = null,
                CVV = null,
                AcquirerId = null,
                StoredCredentials = new StoredCredentials
                {
                    StoredCredentialsMode = null
                },
                ThreeD = new ThreeD
                {
                    Acquirer = new Acquirer { Bin = null, MerchantId = null, MerchantName = null },
                    Account = new Account { NameInd = null, Age = null, LastChangeDate = null, LastChangeInd = null, RegistrationDate = null, PasswordChangeDate = null, ResetInd = null, PurchasesCount6M = null, AddCardAttepmts24H = null, TransactionsCount24H = null, TransactionsCount1Y = null, CardSavedDate = null, CardSavedInd = null, AddressFirstUseDate = null, AddressFirstUseInd = null, SuspiciousActivityInd = null },
                    ExternalMpi = new ExternalMpi { IsExternalMpi = "0", Cavv = "3q2+78r+ur7erb7vyv66vv", Eci = "05", Xid = null, DsTransID = null, ThreeDProtocolVersion = null },
                    V2AdditionalParams = new V2AdditionalParams { ChallengePreference = null, DeliveryEmail = null, DeliveryTimeFrame = null, GiftCardAmount = null, GiftCardCount = null, GiftCardCurrency = null, PreOrderDate = null, PreOrderPurchaseInd = null, ReorderItemsInd = null, ShipIndicator = null, RebillExpiry = null, RebillFrequency = null, ChallengeWindowSize = null },
                    MethodCompletionInd = "Y",
                    Version = null,
                    NotificationURL = null,
                    MerchantURL = null,
                    PlatformType = null,
                    BrowserDetails = new BrowserDetails { AcceptHeader = null, Ip = null, JavaEnabled = null, JavaScriptEnabled = null, Language = null, ColorDepth = null, ScreenHeight = null, ScreenWidth = null, TimeZone = null, UserAgent = null },
                    Sdk = new Sdk
                    {
                        AppId = null,
                        AppSdkInterface = null,
                        AppSdkUIType = null,
                        EncData = null,
                        EphemPubKey = null,
                        MaxTimeout = null,
                        ReferenceNumber = null,
                        TransId = null,
                    }
                }
            };

            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelCard()
        {
            paymentOptionCard.Card.AcquirerId = "01";
            paymentOptionCard.Card.StoredCredentials = new StoredCredentials
            {
                StoredCredentialsMode = "1"
            };
            paymentOptionCard.Card.ThreeD = new ThreeD
            {
                Acquirer = new Acquirer { Bin = "test", MerchantId = "testMerchantId", MerchantName = "testMerchantName" },
                Account = new Account { NameInd = "01", Age = "30", LastChangeDate = "20200810", LastChangeInd = "01", RegistrationDate = "20200808", PasswordChangeDate = "20200810", ResetInd = "01", PurchasesCount6M = "0001", AddCardAttepmts24H = "002", TransactionsCount24H = "002", TransactionsCount1Y = "010", CardSavedDate = "20200817", CardSavedInd = "02", AddressFirstUseDate = "20200812", AddressFirstUseInd = "03", SuspiciousActivityInd = "01" },
                ExternalMpi = new ExternalMpi { IsExternalMpi = "0", Cavv = "3q2+78r+ur7erb7vyv66vv", Eci = "05", Xid = "123123123", DsTransID = "c4e59ceb-a382-4d6a-bc87-385d591fa09d", ThreeDProtocolVersion = "1" },
                V2AdditionalParams = new V2AdditionalParams { ChallengePreference = "02", DeliveryEmail = "test@safecharge.com", DeliveryTimeFrame = "03", GiftCardAmount = "1", GiftCardCount = "41", GiftCardCurrency = "EUR", PreOrderDate = "20220511", PreOrderPurchaseInd = "02", ReorderItemsInd = "01", ShipIndicator = "06", RebillExpiry = "20200101", RebillFrequency = "13", ChallengeWindowSize = "05", ExceptionPayment3DAuth = false },
                MethodCompletionInd = "Y",
                Version = "2.1.0",
                NotificationURL = "test",
                MerchantURL = "test",
                PlatformType = "02",
                BrowserDetails = new BrowserDetails { AcceptHeader = "Y", Ip = "192.168.1.54", JavaEnabled = "true", JavaScriptEnabled = "true", Language = "BG", ColorDepth = "64", ScreenHeight = "1024", ScreenWidth = "1024", TimeZone = "+3", UserAgent = "Mozilla" },
                ConvertNonEnrolled = "",
                Dynamic3DMode = "",
                IsDynamic3D = "",
                PaResponse = "",
                Sdk = new Sdk
                {
                    AppId = "01",
                    AppSdkInterface = "",
                    AppSdkUIType = "",
                    EncData = "",
                    EphemPubKey = "",
                    MaxTimeout = "",
                    ReferenceNumber = "",
                    TransId = ""
                }
            };

            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard);
            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
        }

        [Test]
        public void TestSettingNullsInSubMethodModel()
        {
            _ = new SubMethod
            {
                Submethod = null,
                ReferenceId = null,
                Email = null
            };

            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelSubMethod()
        {
            paymentOptionCard.Submethod = new SubMethod
            {
                Submethod = "",
                ReferenceId = "",
                Email = ""
            };

            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard);
            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(paymentResponse);
            Assert.IsEmpty(paymentResponse.Reason);
            Assert.AreEqual(ResponseStatus.Success, paymentResponse.Status);
        }

        [Test]
        public void TestSettingNullsInPaymentRequestModel()
        {
            var urlDetails = new UrlDetails { BackUrl = null, FailureUrl = null, NotificationUrl = null, PendingUrl = null, SuccessUrl = null };
            var userDetails = new CashierUserDetails { FirstName = null, LastName = null, Address = null, City = null, Country = null, Email = null, County = null, DateOfBirth = null, Phone = null, State = null, Zip = null };
            var userAddress = new UserAddress { FirstName = null, LastName = null, Address = null, City = null, Country = null, Email = null, Cell = null, County = null, Phone = null, State = null, Zip = null };
            var browserDetails = new DeviceDetails { Browser = null, DeviceName = null, DeviceOS = null, DeviceType = null, IpAddress = null };
            var dynamicDescriptor = new DynamicDescriptor { MerchantName = null, MerchantPhone = null };
            _ = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOptionCard)
            {
                AmountDetails = null,
                BillingAddress = userAddress,
                CustomData = null,
                CustomSiteName = null,
                DeviceDetails = browserDetails,
                DynamicDescriptor = dynamicDescriptor,
                ProductId = null,
                RelatedTransactionId = null,
                UrlDetails = urlDetails,
                UserDetails = userDetails,
                Items = null
            };

            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelPaymentRequest()
        {
            var paymentOptionUpo = new PaymentOption
            {
                UserPaymentOptionId = ""
            };

            _ = new PaymentRequest(merchantInfo, sessionToken, currency, "0", paymentOptionUpo)
            {
                AmountDetails = new AmountDetails { TotalDiscount = "0", TotalHandling = "0", TotalShipping = "0", TotalTax = "0" },
                BillingAddress = userAddress,
                CustomData = "",
                CustomSiteName = "",
                ProductId = "",
                RelatedTransactionId = "",
                DeviceDetails = new DeviceDetails { Browser = "Mozilla", DeviceName = "Test Device", DeviceOS = "Win", DeviceType = "Test", IpAddress = "93.146.254.172" },
                DynamicDescriptor = new DynamicDescriptor { MerchantName = "Merchant", MerchantPhone = "Phone" },
                UrlDetails = new UrlDetails { BackUrl = "", FailureUrl = "", NotificationUrl = "", PendingUrl = "", SuccessUrl = "" },
                UserDetails = new CashierUserDetails { FirstName = "Jane", LastName = "Smith", Address = "340689 main St.", City = "London", Country = "GB", Email = "jane.smith@safecharge.com", County = "SCountyU", DateOfBirth = "1990-08-10", Phone = "3598885111111", State = "", Zip = "SDC 33334U" },
                Items = new List<Item> { new Item { Name = "Name", Price = "0", Quantity = "0" } }
            };

            Assert.Pass();
        }

        [Test]
        public void TestSetInvalidUserPaymentOptionId()
        {
            ActualValueDelegate<object> createUpoDelegate = () => new PaymentOption
            {
                UserPaymentOptionId = "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111"
            };

            Assert.That(createUpoDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestSetCVVValid()
        {
            ActualValueDelegate<object> createUpoDelegate = () => new UserPaymentOption
            {
                CVV = "1234"
            };

            Assert.Pass();
        }

        [Test]
        public void TestSetCVVInvalid()
        {
            ActualValueDelegate<object> createUpoDelegate = () => new UserPaymentOption
            {
                CVV = "12345"
            };

            Assert.That(createUpoDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestGetAndSetModelSettleTransactionRequest()
        {
            _ = new SettleTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                "relatedTransactionId")
            {
                ClientUniqueId = "",
                ClientRequestId = "",
                Addendums = new Addendums { },
                DescriptorMerchantName = "",
                DescriptorMerchantPhone = "",
                UrlDetails = new UrlDetails { },
                CustomData = "",
                Comment = "",
                CustomSiteName = "",
                ProductId = ""
            };

            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelOpenOrderRequest()
        {
            var openOrderRequest = new OpenOrderRequest(merchantInfo, sessionToken, currency, amount)
            {
                Items = items,
                PaymentOption = new OpenOrderPaymentOption
                {
                    Card = new OpenOrderCard
                    {
                        AcquirerId = "",
                        StoredCredentials = new StoredCredentials { StoredCredentialsMode = "0" },
                        ThreeD = new OpenOrderThreeD
                        {
                            Acquirer = new Acquirer { Bin = null, MerchantId = null, MerchantName = null },
                            Account = new Account { NameInd = null, Age = null, LastChangeDate = null, LastChangeInd = null, RegistrationDate = null, PasswordChangeDate = null, ResetInd = null, PurchasesCount6M = null, AddCardAttepmts24H = null, TransactionsCount24H = null, TransactionsCount1Y = null, CardSavedDate = null, CardSavedInd = null, AddressFirstUseDate = null, AddressFirstUseInd = null, SuspiciousActivityInd = null },
                            ConvertNonEnrolled = "",
                            Dynamic3DMode = "",
                            IsDynamic3D = "",
                            V2AdditionalParams = new OpenOrderThreeDV2AdditionalParams { DeliveryEmail = null, DeliveryTimeFrame = null, ExceptionPayment3DAuth = false, GiftCardAmount = null, GiftCardCurrency = null, PreOrderDate = null, PreOrderPurchaseInd = null, RebillExpiry = null, RebillFrequency = null, ReorderItemsInd = null },
                        }
                    }
                },
                UserPaymentOption = new UserPaymentOption
                {
                    UserPaymentOptionId = "1"
                },
                PaymentMethod = "",
                UserTokenId = "",
                ClientRequestId = "",
                ClientUniqueId = "",
                AmountDetails = new AmountDetails { },
                DeviceDetails = new DeviceDetails { },
                UserDetails = new CashierUserDetails { },
                ShippingAddress = new UserAddress { },
                BillingAddress = new UserAddress { },
                DynamicDescriptor = new DynamicDescriptor { },
                MerchantDetails = new MerchantDetails { },
                Addendums = new Addendums { },
                UrlDetails = new UrlDetails { },
                ProductId = "",
                CustomData = "",
                TransactionType = "",
                IsMoto = "",
                IsRebilling = "0",
                RebillingType = "",
                SubMerchant = new SubMerchant
                {
                    City = "",
                    Id = "",
                    CountryCode = "",
                }
            };

            var response = requestExecutor.OpenOrder(openOrderRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelInitPaymentRequest()
        {
            var initPaymentPaymentOption = new InitPaymentPaymentOption
            {
                Card = new InitPaymentCard
                {
                    CardNumber = "",
                    CardHolderName = "",
                    ExpirationMonth = "",
                    ExpirationYear = "",
                    CVV = "123",
                    ThreeD = new InitPaymentThreeD
                    {
                        Acquirer = new Acquirer { Bin = null, MerchantId = null, MerchantName = null },
                        MethodNotificationUrl = null
                    }
                }
            };

            _ = new InitPaymentRequest(merchantInfo, sessionToken, currency, amount, initPaymentPaymentOption)
            {
                UserTokenId = "",
                ClientRequestId = "",
                ClientUniqueId = "",
                DeviceDetails = new DeviceDetails { },
                UrlDetails = new UrlDetails { },
                CustomData = "",
                BillingAddress = new UserAddress { }
            };

            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelVerify3dRequest()
        {
            _ = new Verify3dRequest(
                    merchantInfo,
                    sessionToken,
                    currency,
                    amount,
                    new Verify3dPaymentOption 
                    { 
                        Card = new Verify3dCard
                        {
                            ThreeD = new Verify3dThreeD
                            {
                                ExternalRiskScore = "",
                                MpiChallengePreference = "",
                                PaResponse = ""
                            }
                        }
                    },
                    "")
            {
                UserId = "",
                UserTokenId = "",
                ClientRequestId = "",
                ClientUniqueId = "",
                SubMerchant = new SubMerchant { },
                MerchantDetails = new MerchantDetails { },
                CustomSiteName = "",
                CustomData = "",
                BillingAddress = new UserAddress { }
            };

            Assert.Pass();

        }
        [Test]
        public void TestGetAndSetModelPayoutRequest()
        {
            _ = new PayoutRequest(
                merchantInfo,
                sessionToken,
                null,
                null,
                null,
                null,
                new UserPaymentOption { })
            {
                Comment = null
            };

            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelGetCardDetailsRequest()
        {
            _ = new GetCardDetailsRequest(merchantInfo, sessionToken, null, null);

            Assert.Pass();
        }

        [Test]
        public void TestGetAndSetModelGetMerchantPaymentMethodsRequest()
        {
            _ = new GetMerchantPaymentMethodsRequest(merchantInfo, sessionToken, "123")
            {
                CurrencyCode = null,
                CountryCode = null,
                LanguageCode = null
            };

            Assert.Pass();
        }

        [Test]
        public void TestEmptyConstructorsUsedForMappingFromConfigFile()
        {
            _ = new PaymentRequest();
            _ = new SettleTransactionRequest();
            _ = new VoidTransactionRequest();
            _ = new RefundTransactionRequest();
            _ = new GetPaymentStatusRequest();
            _ = new OpenOrderRequest();
            _ = new InitPaymentRequest();
            _ = new Authorize3dRequest();
            _ = new Verify3dRequest();
            _ = new PayoutRequest();
            _ = new GetCardDetailsRequest();
            _ = new GetMerchantPaymentMethodsRequest();

            Assert.Pass();
        }
    }
}