using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Safecharge.Model.Common;
using Safecharge.Request;
using Safecharge.Response;
using Safecharge.Response.Common;
using Safecharge.Response.Payment;
using Safecharge.Utils.Enum;

namespace Safecharge.Sample
{
    class Program
    {
        private static JsonSerializerSettings SerializerSettings =>
            new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

        private static readonly IConfigurationBuilder builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.local.json", optional: true);

        private static IConfigurationRoot config;
        private static MerchantConfig merchantInfoConfig;
        private static PaymentRequest paymentRequestConfig;
        private static SettleTransactionRequest settleTransactionRequestConfig;
        private static VoidTransactionRequest voidTransactionRequestConfig;
        private static RefundTransactionRequest refundTransactionRequestConfig;
        private static GetPaymentStatusRequest getPaymentStatusRequestConfig;
        private static OpenOrderRequest openOrderRequestConfig;
        private static InitPaymentRequest initPaymentRequestConfig;
        private static Authorize3dRequest authorize3dRequestConfig;
        private static Verify3dRequest verify3dRequestConfig;
        private static PayoutRequest payoutRequestConfig;
        private static GetCardDetailsRequest getCardDetailsRequestConfig;
        private static GetMerchantPaymentMethodsRequest getMerchantPaymentMethodsRequestConfig;

        static void Main()
        {
            ReloadConfig();

            bool showMenu = true;
            while (showMenu)
            {
                var requestExecutor = new SafechargeRequestExecutor();
                Safecharge safecharge;
                try
                {
                    safecharge = new Safecharge(
                        merchantInfoConfig.MerchantKey,
                        merchantInfoConfig.MerchantId,
                        merchantInfoConfig.MerchantSiteId,
                        merchantInfoConfig.ServerHost,
                        HashAlgorithmType.SHA256);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                    break;
                }

                showMenu = MainMenu(requestExecutor, safecharge);
            }
        }

        private static void ReloadConfig()
        {
            Console.WriteLine("Loading config from 'appsettings.json'...");

            try
            {
                config = builder.Build();
                merchantInfoConfig = config.GetSection("merchantConfig").Get<MerchantConfig>();
                paymentRequestConfig = config.GetSection("paymentRequest").Get<PaymentRequest>();
                settleTransactionRequestConfig = config.GetSection("settleTransactionRequest").Get<SettleTransactionRequest>();
                voidTransactionRequestConfig = config.GetSection("voidTransactionRequest").Get<VoidTransactionRequest>();
                refundTransactionRequestConfig = config.GetSection("refundTransactionRequest").Get<RefundTransactionRequest>();
                getPaymentStatusRequestConfig = config.GetSection("getPaymentStatusRequest").Get<GetPaymentStatusRequest>();
                openOrderRequestConfig = config.GetSection("openOrderRequest").Get<OpenOrderRequest>();
                initPaymentRequestConfig = config.GetSection("initPaymentRequest").Get<InitPaymentRequest>();
                authorize3dRequestConfig = config.GetSection("authorize3dRequest").Get<Authorize3dRequest>();
                verify3dRequestConfig = config.GetSection("verify3dRequest").Get<Verify3dRequest>();
                payoutRequestConfig = config.GetSection("payoutRequest").Get<PayoutRequest>();
                getCardDetailsRequestConfig = config.GetSection("getCardDetailsRequest").Get<GetCardDetailsRequest>();
                getMerchantPaymentMethodsRequestConfig = config.GetSection("getMerchantPaymentMethodsRequest").Get<GetMerchantPaymentMethodsRequest>();

                Console.WriteLine("Config loaded.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Please, add 'appsettings.json' file, containing 'merchantConfig' and 'paymentOption' objects.");
                Console.WriteLine(ex.ToString());
            }
        }

        private static (MerchantInfo MerchantInfo, GetSessionTokenResponse Response) ExecuteGetSessionTokenUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var merchantInfo = new MerchantInfo(
                merchantInfoConfig.MerchantKey,
                merchantInfoConfig.MerchantId,
                merchantInfoConfig.MerchantSiteId,
                merchantInfoConfig.ServerHost,
                HashAlgorithmType.SHA256);

            // GetSessionToken
            Console.WriteLine("Executing GetSessionTokenRequest...");
            var getSessionTokenRequest = new GetSessionTokenRequest(merchantInfo);
            var getSessionTokenResponse = requestExecutor.GetSessionToken(getSessionTokenRequest).GetAwaiter().GetResult();

            SaveResponse(getSessionTokenResponse, "GetSessionTokenResponseRequestExecutor.json");

            return (merchantInfo, getSessionTokenResponse);
        }

        private static PaymentResponse ExecutePaymentUsingSafecharge(Safecharge safecharge)
        {
            Console.WriteLine("Executing Safecharge.Payment()...");
            var paymentResponseSafecharge = safecharge.Payment(
                paymentRequestConfig.Currency,
                paymentRequestConfig.Amount,
                paymentRequestConfig.PaymentOption,
                items: paymentRequestConfig.Items,
                userTokenId: paymentRequestConfig.UserTokenId,
                clientUniqueId: paymentRequestConfig.ClientUniqueId,
                clientRequestId: paymentRequestConfig.ClientRequestId,
                isRebilling: paymentRequestConfig.IsRebilling,
                amountDetails: paymentRequestConfig.AmountDetails,
                deviceDetails: paymentRequestConfig.DeviceDetails,
                userDetails: paymentRequestConfig.UserDetails,
                shippingAddress: paymentRequestConfig.ShippingAddress,
                billingAddress: paymentRequestConfig.BillingAddress,
                dynamicDescriptor: paymentRequestConfig.DynamicDescriptor,
                merchantDetails: paymentRequestConfig.MerchantDetails,
                addendums: paymentRequestConfig.Addendums,
                urlDetails: paymentRequestConfig.UrlDetails,
                customSiteName: paymentRequestConfig.CustomSiteName,
                productId: paymentRequestConfig.ProductId,
                customData: paymentRequestConfig.CustomData,
                relatedTransactionId: paymentRequestConfig.RelatedTransactionId,
                transactionType: paymentRequestConfig.TransactionType,
                autoPayment3D: paymentRequestConfig.AutoPayment3D,
                isMoto: paymentRequestConfig.IsMoto,
                subMethodDetails: paymentRequestConfig.SubMethodDetails,
                isPartialApproval: paymentRequestConfig.IsPartialApproval,
                userId: paymentRequestConfig.IsPartialApproval,
                rebillingType: paymentRequestConfig.RebillingType,
                authenticationTypeOnly: paymentRequestConfig.AuthenticationTypeOnly,
                subMerchant: paymentRequestConfig.SubMerchant,
                orderId: paymentRequestConfig.OrderId).GetAwaiter().GetResult();

            SaveResponse(paymentResponseSafecharge, "PaymentResponseSafecharge.json");

            return paymentResponseSafecharge;
        }

        private static (MerchantInfo MerchantInfo, PaymentResponse Response) ExecutePaymentUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var getSessionTokenResponse = ExecuteGetSessionTokenUsingSafechargeRequestExecutor(requestExecutor);

            // Payment
            Console.WriteLine("Executing PaymentRequest...");
            var paymentRequest = new PaymentRequest(
                getSessionTokenResponse.MerchantInfo,
                getSessionTokenResponse.Response.SessionToken,
                paymentRequestConfig.Currency,
                paymentRequestConfig.Amount,
                paymentRequestConfig.PaymentOption)
            {
                Items = paymentRequestConfig.Items,
                UserTokenId = paymentRequestConfig.UserTokenId,
                ClientRequestId = paymentRequestConfig.ClientRequestId,
                ClientUniqueId = paymentRequestConfig.ClientUniqueId,
                IsRebilling = paymentRequestConfig.IsRebilling,
                AmountDetails = paymentRequestConfig.AmountDetails,
                DeviceDetails = paymentRequestConfig.DeviceDetails,
                UserDetails = paymentRequestConfig.UserDetails,
                ShippingAddress = paymentRequestConfig.ShippingAddress,
                BillingAddress = paymentRequestConfig.BillingAddress,
                DynamicDescriptor = paymentRequestConfig.DynamicDescriptor,
                MerchantDetails = paymentRequestConfig.MerchantDetails,
                Addendums = paymentRequestConfig.Addendums,
                UrlDetails = paymentRequestConfig.UrlDetails,
                CustomSiteName = paymentRequestConfig.CustomSiteName,
                ProductId = paymentRequestConfig.ProductId,
                CustomData = paymentRequestConfig.CustomData,
                RelatedTransactionId = paymentRequestConfig.RelatedTransactionId,
                TransactionType = paymentRequestConfig.TransactionType,
                AutoPayment3D = paymentRequestConfig.AutoPayment3D,
                IsMoto = paymentRequestConfig.IsMoto,
                UserId = paymentRequestConfig.UserId,
                RebillingType = paymentRequestConfig.RebillingType,
                AuthenticationTypeOnly = paymentRequestConfig.AuthenticationTypeOnly,
                SubMerchant = paymentRequestConfig.SubMerchant,
                OrderId = paymentRequestConfig.OrderId
            };

            var paymentResponse = requestExecutor.Payment(paymentRequest).GetAwaiter().GetResult();

            SaveResponse(paymentResponse, "PaymentResponseRequestExecutor.json");

            return (getSessionTokenResponse.MerchantInfo, paymentResponse);
        }

        private static void ExecuteSettleTransactionUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var paymentResponse = ExecutePaymentUsingSafechargeRequestExecutor(requestExecutor);

            var request = new SettleTransactionRequest(
                paymentResponse.MerchantInfo,
                paymentResponse.Response.SessionToken,
                paymentRequestConfig.Currency,
                paymentRequestConfig.Amount,
                paymentResponse.Response.TransactionId)
            {
                AuthCode = settleTransactionRequestConfig.AuthCode,
                ClientUniqueId = settleTransactionRequestConfig.ClientUniqueId,
                ClientRequestId = settleTransactionRequestConfig.ClientRequestId,
                Addendums = settleTransactionRequestConfig.Addendums,
                DescriptorMerchantName = settleTransactionRequestConfig.DescriptorMerchantName,
                DescriptorMerchantPhone = settleTransactionRequestConfig.DescriptorMerchantPhone,
                UrlDetails = settleTransactionRequestConfig.UrlDetails,
                CustomData = settleTransactionRequestConfig.CustomData,
                Comment = settleTransactionRequestConfig.Comment,
                CustomSiteName = settleTransactionRequestConfig.CustomSiteName,
                ProductId = settleTransactionRequestConfig.ProductId,
                UserId = settleTransactionRequestConfig.UserId,
                DeviceDetails = settleTransactionRequestConfig.DeviceDetails,
                RebillingType = settleTransactionRequestConfig.RebillingType,
                AuthenticationTypeOnly = settleTransactionRequestConfig.AuthenticationTypeOnly,
                SubMerchant = settleTransactionRequestConfig.SubMerchant,
            };

            var response = requestExecutor.SettleTransaction(request).GetAwaiter().GetResult();

            SaveResponse(response, "SettleTransactionResponseRequestExecutor.json");
        }

        private static void ExecuteSettleTransactionUsingSafecharge(Safecharge safecharge)
        {
            var paymentResponseSafecharge = ExecutePaymentUsingSafecharge(safecharge);

            var transactionResponse = safecharge.SettleTransaction(
                paymentRequestConfig.Currency,
                paymentRequestConfig.Amount,
                paymentResponseSafecharge.TransactionId,
                clientUniqueId: settleTransactionRequestConfig.ClientUniqueId,
                clientRequestId: settleTransactionRequestConfig.ClientRequestId,
                addendums: settleTransactionRequestConfig.Addendums,
                descriptorMerchantName: settleTransactionRequestConfig.DescriptorMerchantName,
                descriptorMerchantPhone: settleTransactionRequestConfig.DescriptorMerchantPhone,
                urlDetails: settleTransactionRequestConfig.UrlDetails,
                authCode: settleTransactionRequestConfig.AuthCode,
                customData: settleTransactionRequestConfig.CustomData,
                comment: settleTransactionRequestConfig.Comment,
                customSiteName: settleTransactionRequestConfig.CustomSiteName,
                productId: settleTransactionRequestConfig.ProductId,
                userId: settleTransactionRequestConfig.UserId,
                deviceDetails: settleTransactionRequestConfig.DeviceDetails,
                rebillingType: settleTransactionRequestConfig.RebillingType,
                authenticationTypeOnly: settleTransactionRequestConfig.AuthenticationTypeOnly,
                subMerchant: settleTransactionRequestConfig.SubMerchant).GetAwaiter().GetResult();

            SaveResponse(transactionResponse, "SettleTransactionResponseSafecharge.json");
        }

        private static void ExecuteVoidTransactionUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var paymentResponse = ExecutePaymentUsingSafechargeRequestExecutor(requestExecutor);

            var request = new VoidTransactionRequest(
                paymentResponse.MerchantInfo,
                paymentResponse.Response.SessionToken,
                paymentRequestConfig.Currency,
                paymentRequestConfig.Amount,
                paymentResponse.Response.TransactionId)
            {
                AuthCode = voidTransactionRequestConfig.AuthCode,
                ClientUniqueId = voidTransactionRequestConfig.ClientUniqueId,
                ClientRequestId = voidTransactionRequestConfig.ClientRequestId,
                UrlDetails = voidTransactionRequestConfig.UrlDetails,
                CustomData = voidTransactionRequestConfig.CustomData,
                Comment = voidTransactionRequestConfig.Comment,
                CustomSiteName = voidTransactionRequestConfig.CustomSiteName,
                ProductId = voidTransactionRequestConfig.ProductId,
                UserId = voidTransactionRequestConfig.UserId,
                DeviceDetails = voidTransactionRequestConfig.DeviceDetails,
                RebillingType = voidTransactionRequestConfig.RebillingType,
                AuthenticationTypeOnly = voidTransactionRequestConfig.AuthenticationTypeOnly,
                SubMerchant = voidTransactionRequestConfig.SubMerchant,
                Addendums = voidTransactionRequestConfig.Addendums
            };

            var response = requestExecutor.VoidTransaction(request).GetAwaiter().GetResult();

            SaveResponse(response, "VoidTransactionResponseRequestExecutor.json");
        }

        private static void ExecuteVoidTransactionUsingSafecharge(Safecharge safecharge)
        {
            var paymentResponseSafecharge = ExecutePaymentUsingSafecharge(safecharge);

            var transactionResponse = safecharge.VoidTransaction(
                paymentRequestConfig.Currency,
                paymentRequestConfig.Amount,
                paymentResponseSafecharge.TransactionId,
                clientUniqueId: voidTransactionRequestConfig.ClientUniqueId,
                clientRequestId: voidTransactionRequestConfig.ClientRequestId,
                urlDetails: voidTransactionRequestConfig.UrlDetails,
                authCode: voidTransactionRequestConfig.AuthCode,
                customData: voidTransactionRequestConfig.CustomData,
                comment: voidTransactionRequestConfig.Comment,
                customSiteName: voidTransactionRequestConfig.CustomSiteName,
                productId: voidTransactionRequestConfig.ProductId,
                userId: voidTransactionRequestConfig.UserId,
                deviceDetails: voidTransactionRequestConfig.DeviceDetails,
                rebillingType: voidTransactionRequestConfig.RebillingType,
                authenticationTypeOnly: voidTransactionRequestConfig.AuthenticationTypeOnly,
                subMerchant: voidTransactionRequestConfig.SubMerchant,
                addendums: voidTransactionRequestConfig.Addendums).GetAwaiter().GetResult();

            SaveResponse(transactionResponse, "VoidTransactionResponseSafecharge.json");
        }

        private static void ExecuteRefundTransactionUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var paymentResponse = ExecutePaymentUsingSafechargeRequestExecutor(requestExecutor);

            var request = new RefundTransactionRequest(
                paymentResponse.MerchantInfo,
                paymentResponse.Response.SessionToken,
                paymentRequestConfig.Currency,
                paymentRequestConfig.Amount,
                paymentResponse.Response.TransactionId)
            {
                AuthCode = refundTransactionRequestConfig.AuthCode,
                ClientUniqueId = refundTransactionRequestConfig.ClientUniqueId,
                ClientRequestId = refundTransactionRequestConfig.ClientRequestId,
                UrlDetails = refundTransactionRequestConfig.UrlDetails,
                CustomData = refundTransactionRequestConfig.CustomData,
                Comment = refundTransactionRequestConfig.Comment,
                CustomSiteName = refundTransactionRequestConfig.CustomSiteName,
                ProductId = refundTransactionRequestConfig.ProductId,
                UserId = refundTransactionRequestConfig.UserId,
                DeviceDetails = refundTransactionRequestConfig.DeviceDetails,
                RebillingType = refundTransactionRequestConfig.RebillingType,
                AuthenticationTypeOnly = refundTransactionRequestConfig.AuthenticationTypeOnly,
                SubMerchant = refundTransactionRequestConfig.SubMerchant,
                Addendums = refundTransactionRequestConfig.Addendums
            };

            var response = requestExecutor.RefundTransaction(request).GetAwaiter().GetResult();

            SaveResponse(response, "RefundTransactionResponseRequestExecutor.json");
        }

        private static void ExecuteRefundTransactionUsingSafecharge(Safecharge safecharge)
        {
            var paymentResponseSafecharge = ExecutePaymentUsingSafecharge(safecharge);

            var transactionResponse = safecharge.RefundTransaction(
                paymentRequestConfig.Currency,
                paymentRequestConfig.Amount,
                paymentResponseSafecharge.TransactionId,
                clientUniqueId: refundTransactionRequestConfig.ClientUniqueId,
                clientRequestId: refundTransactionRequestConfig.ClientRequestId,
                urlDetails: refundTransactionRequestConfig.UrlDetails,
                authCode: refundTransactionRequestConfig.AuthCode,
                customData: refundTransactionRequestConfig.CustomData,
                comment: refundTransactionRequestConfig.Comment,
                customSiteName: refundTransactionRequestConfig.CustomSiteName,
                productId: refundTransactionRequestConfig.ProductId,
                userId: refundTransactionRequestConfig.UserId,
                deviceDetails: refundTransactionRequestConfig.DeviceDetails,
                rebillingType: refundTransactionRequestConfig.RebillingType,
                authenticationTypeOnly: refundTransactionRequestConfig.AuthenticationTypeOnly,
                subMerchant: refundTransactionRequestConfig.SubMerchant,
                addendums: refundTransactionRequestConfig.Addendums).GetAwaiter().GetResult();

            SaveResponse(transactionResponse, "RefundTransactionResponseSafecharge.json");
        }

        private static void ExecuteGetPaymentStatusUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var paymentResponse = ExecutePaymentUsingSafechargeRequestExecutor(requestExecutor);

            var request = new GetPaymentStatusRequest(
                paymentResponse.MerchantInfo,
                paymentResponse.Response.SessionToken)
            {
                UserId = getPaymentStatusRequestConfig.UserId,
                DeviceDetails = getPaymentStatusRequestConfig.DeviceDetails,
                RebillingType = getPaymentStatusRequestConfig.RebillingType,
                AuthenticationTypeOnly = getPaymentStatusRequestConfig.AuthenticationTypeOnly,
                SubMerchant = getPaymentStatusRequestConfig.SubMerchant,
                Addendums = getPaymentStatusRequestConfig.Addendums
            };

            var response = requestExecutor.GetPaymentStatus(request).GetAwaiter().GetResult();

            SaveResponse(response, "GetPaymentStatusResponseRequestExecutor.json");
        }

        private static void ExecuteGetPaymentStatusUsingSafecharge(Safecharge safecharge)
        {
            _ = ExecutePaymentUsingSafecharge(safecharge);

            var response = safecharge.GetPaymentStatus(
                userId: getPaymentStatusRequestConfig.UserId,
                deviceDetails: getPaymentStatusRequestConfig.DeviceDetails,
                rebillingType: getPaymentStatusRequestConfig.RebillingType,
                authenticationTypeOnly: getPaymentStatusRequestConfig.AuthenticationTypeOnly,
                subMerchant: getPaymentStatusRequestConfig.SubMerchant,
                addendums: getPaymentStatusRequestConfig.Addendums).GetAwaiter().GetResult();

            SaveResponse(response, "GetPaymentStatusResponseSafecharge.json");
        }

        private static void ExecuteOpenOrderUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var getSessionTokenResponse = ExecuteGetSessionTokenUsingSafechargeRequestExecutor(requestExecutor);

            var request = new OpenOrderRequest(
                getSessionTokenResponse.MerchantInfo,
                getSessionTokenResponse.Response.SessionToken,
                openOrderRequestConfig.Currency,
                openOrderRequestConfig.Amount)
            {
                Items = openOrderRequestConfig.Items,
                PaymentOption = openOrderRequestConfig.PaymentOption,
                UserPaymentOption = openOrderRequestConfig.UserPaymentOption,
                PaymentMethod = openOrderRequestConfig.PaymentMethod,
                UserTokenId = openOrderRequestConfig.UserTokenId,
                ClientRequestId = openOrderRequestConfig.ClientRequestId,
                ClientUniqueId = openOrderRequestConfig.ClientUniqueId,
                AmountDetails = openOrderRequestConfig.AmountDetails,
                DeviceDetails = openOrderRequestConfig.DeviceDetails,
                UserDetails = openOrderRequestConfig.UserDetails,
                ShippingAddress = openOrderRequestConfig.ShippingAddress,
                BillingAddress = openOrderRequestConfig.BillingAddress,
                DynamicDescriptor = openOrderRequestConfig.DynamicDescriptor,
                MerchantDetails = openOrderRequestConfig.MerchantDetails,
                Addendums = openOrderRequestConfig.Addendums,
                UrlDetails = openOrderRequestConfig.UrlDetails,
                CustomSiteName = openOrderRequestConfig.CustomSiteName,
                ProductId = openOrderRequestConfig.ProductId,
                CustomData = openOrderRequestConfig.CustomData,
                TransactionType = openOrderRequestConfig.TransactionType,
                IsMoto = openOrderRequestConfig.IsMoto,
                IsRebilling = openOrderRequestConfig.IsRebilling,
                RebillingType = openOrderRequestConfig.RebillingType,
                SubMerchant = openOrderRequestConfig.SubMerchant,
                UserId = openOrderRequestConfig.UserId,
                AuthenticationTypeOnly = openOrderRequestConfig.AuthenticationTypeOnly,
            };
            var response = requestExecutor.OpenOrder(request).GetAwaiter().GetResult();

            SaveResponse(response, "OpenOrderResponseRequestExecutor.json");
        }

        private static void ExecuteOpenOrderUsingSafecharge(Safecharge safecharge)
        {
            var response = safecharge.OpenOrder(
                openOrderRequestConfig.Currency,
                openOrderRequestConfig.Amount,
                items: openOrderRequestConfig.Items,
                paymentOption: openOrderRequestConfig.PaymentOption,
                userTokenId: openOrderRequestConfig.UserTokenId,
                clientUniqueId: openOrderRequestConfig.ClientUniqueId,
                clientRequestId: openOrderRequestConfig.ClientRequestId,
                amountDetails: openOrderRequestConfig.AmountDetails,
                deviceDetails: openOrderRequestConfig.DeviceDetails,
                userDetails: openOrderRequestConfig.UserDetails,
                shippingAddress: openOrderRequestConfig.ShippingAddress,
                billingAddress: openOrderRequestConfig.BillingAddress,
                dynamicDescriptor: openOrderRequestConfig.DynamicDescriptor,
                merchantDetails: openOrderRequestConfig.MerchantDetails,
                addendums: openOrderRequestConfig.Addendums,
                urlDetails: openOrderRequestConfig.UrlDetails,
                customSiteName: openOrderRequestConfig.CustomSiteName,
                productId: openOrderRequestConfig.ProductId,
                customData: openOrderRequestConfig.CustomData,
                transactionType: openOrderRequestConfig.TransactionType,
                isMoto: openOrderRequestConfig.IsMoto,
                isRebilling: openOrderRequestConfig.IsRebilling,
                rebillingType: openOrderRequestConfig.RebillingType,
                subMerchant: openOrderRequestConfig.SubMerchant,
                userId: openOrderRequestConfig.UserId,
                authenticationTypeOnly: openOrderRequestConfig.AuthenticationTypeOnly).GetAwaiter().GetResult();

            SaveResponse(response, "OpenOrderResponseSafecharge.json");
        }

        private static (MerchantInfo MerchantInfo, InitPaymentResponse Response) ExecuteInitPaymentUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var getSessionTokenResponse = ExecuteGetSessionTokenUsingSafechargeRequestExecutor(requestExecutor);

            var request = new InitPaymentRequest(
                getSessionTokenResponse.MerchantInfo,
                getSessionTokenResponse.Response.SessionToken,
                initPaymentRequestConfig.Currency,
                initPaymentRequestConfig.Amount,
                initPaymentRequestConfig.PaymentOption)
            {
                UserTokenId = initPaymentRequestConfig.UserTokenId,
                ClientRequestId = initPaymentRequestConfig.ClientRequestId,
                ClientUniqueId = initPaymentRequestConfig.ClientUniqueId,
                DeviceDetails = initPaymentRequestConfig.DeviceDetails,
                UrlDetails = initPaymentRequestConfig.UrlDetails,
                CustomData = initPaymentRequestConfig.CustomData,
                BillingAddress = initPaymentRequestConfig.BillingAddress,
                UserId = initPaymentRequestConfig.UserId,
                RebillingType = initPaymentRequestConfig.RebillingType,
                AuthenticationTypeOnly = initPaymentRequestConfig.AuthenticationTypeOnly,
                SubMerchant = initPaymentRequestConfig.SubMerchant,
                Addendums = initPaymentRequestConfig.Addendums,
                OrderId = initPaymentRequestConfig.OrderId
            };

            var response = requestExecutor.InitPayment(request).GetAwaiter().GetResult();

            SaveResponse(response, "InitPaymentResponseRequestExecutor.json");

            return (getSessionTokenResponse.MerchantInfo, response);
        }

        private static InitPaymentResponse ExecuteInitPaymentUsingSafecharge(Safecharge safecharge)
        {
            var response = safecharge.InitPayment(
                initPaymentRequestConfig.Currency,
                initPaymentRequestConfig.Amount,
                initPaymentRequestConfig.PaymentOption,
                userTokenId: initPaymentRequestConfig.UserTokenId,
                clientUniqueId: initPaymentRequestConfig.ClientUniqueId,
                clientRequestId: initPaymentRequestConfig.ClientRequestId,
                deviceDetails: initPaymentRequestConfig.DeviceDetails,
                billingAddress: initPaymentRequestConfig.BillingAddress,
                urlDetails: initPaymentRequestConfig.UrlDetails,
                customData: initPaymentRequestConfig.CustomData,
                userId: initPaymentRequestConfig.UserId,
                rebillingType: initPaymentRequestConfig.RebillingType,
                authenticationTypeOnly: initPaymentRequestConfig.AuthenticationTypeOnly,
                subMerchant: initPaymentRequestConfig.SubMerchant,
                addendums: initPaymentRequestConfig.Addendums,
                orderId: initPaymentRequestConfig.OrderId).GetAwaiter().GetResult();

            SaveResponse(response, "InitPaymentResponseSafecharge.json");

            return response;
        }

        private static (MerchantInfo MerchantInfo, Authorize3dResponse Response) ExecuteAuthorize3dUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var initPaymentResponse = ExecuteInitPaymentUsingSafechargeRequestExecutor(requestExecutor);

            var request = new Authorize3dRequest(
                initPaymentResponse.MerchantInfo,
                initPaymentResponse.Response.SessionToken,
                authorize3dRequestConfig.Currency,
                authorize3dRequestConfig.Amount,
                authorize3dRequestConfig.PaymentOption,
                initPaymentResponse.Response.TransactionId)
            {
                UserTokenId = authorize3dRequestConfig.UserTokenId,
                ClientRequestId = authorize3dRequestConfig.ClientRequestId,
                ClientUniqueId = authorize3dRequestConfig.ClientUniqueId,
                DeviceDetails = authorize3dRequestConfig.DeviceDetails,
                UrlDetails = authorize3dRequestConfig.UrlDetails,
                CustomData = authorize3dRequestConfig.CustomData,
                BillingAddress = authorize3dRequestConfig.BillingAddress,
                UserId = authorize3dRequestConfig.UserId,
                RebillingType = authorize3dRequestConfig.RebillingType,
                AuthenticationTypeOnly = authorize3dRequestConfig.AuthenticationTypeOnly,
                SubMerchant = authorize3dRequestConfig.SubMerchant,
                Addendums = authorize3dRequestConfig.Addendums
            };

            var response = requestExecutor.Authorize3d(request).GetAwaiter().GetResult();

            SaveResponse(response, "Authorize3dResponseRequestExecutor.json");

            return (initPaymentResponse.MerchantInfo, response);
        }

        private static Authorize3dResponse ExecuteAuthorize3dUsingSafecharge(Safecharge safecharge)
        {
            var initPaymentResponse = ExecuteInitPaymentUsingSafecharge(safecharge);

            var response = safecharge.Authorize3d(
                authorize3dRequestConfig.Currency,
                authorize3dRequestConfig.Amount,
                authorize3dRequestConfig.PaymentOption,
                initPaymentResponse.TransactionId,
                userTokenId: authorize3dRequestConfig.UserTokenId,
                clientUniqueId: authorize3dRequestConfig.ClientUniqueId,
                clientRequestId: authorize3dRequestConfig.ClientRequestId,
                deviceDetails: authorize3dRequestConfig.DeviceDetails,
                billingAddress: authorize3dRequestConfig.BillingAddress,
                urlDetails: authorize3dRequestConfig.UrlDetails,
                customData: authorize3dRequestConfig.CustomData,
                userId: authorize3dRequestConfig.UserId,
                rebillingType: authorize3dRequestConfig.RebillingType,
                authenticationTypeOnly: authorize3dRequestConfig.AuthenticationTypeOnly,
                subMerchant: authorize3dRequestConfig.SubMerchant,
                addendums: authorize3dRequestConfig.Addendums).GetAwaiter().GetResult();

            SaveResponse(response, "Authorize3dResponseSafecharge.json");

            return response;
        }

        private static void ExecuteVerify3dUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var authorize3dResponse = ExecuteAuthorize3dUsingSafechargeRequestExecutor(requestExecutor);
            var acsUrl = authorize3dResponse.Response.PaymentOption.Card.ThreeD.AcsUrl;
            var cReq = authorize3dResponse.Response.PaymentOption.Card.ThreeD.CReq;

            if (string.IsNullOrEmpty(authorize3dResponse.Response.Reason) &&
                !string.IsNullOrEmpty(acsUrl) &&
                !string.IsNullOrEmpty(cReq))
            {
                Console.WriteLine("Next, we will simulate the challenge process. Copy the following URL and open it in a browser:");
                Console.WriteLine($"https://3dsecuresafecharge.000webhostapp.com/3Dv2/showUrl.php?acsUrl={acsUrl}&creq={cReq}");
                Console.WriteLine("Press enter when challenge is completed to perform verify3d...");
                Console.ReadLine();

                var request = new Verify3dRequest(
                    authorize3dResponse.MerchantInfo,
                    authorize3dResponse.Response.SessionToken,
                    verify3dRequestConfig.Currency,
                    verify3dRequestConfig.Amount,
                    verify3dRequestConfig.PaymentOption,
                    authorize3dResponse.Response.TransactionId)
                {
                    UserId = verify3dRequestConfig.UserId,
                    UserTokenId = verify3dRequestConfig.UserTokenId,
                    ClientRequestId = verify3dRequestConfig.ClientRequestId,
                    ClientUniqueId = verify3dRequestConfig.ClientUniqueId,
                    SubMerchant = verify3dRequestConfig.SubMerchant,
                    MerchantDetails = verify3dRequestConfig.MerchantDetails,
                    CustomSiteName = verify3dRequestConfig.CustomSiteName,
                    CustomData = verify3dRequestConfig.CustomData,
                    BillingAddress = verify3dRequestConfig.BillingAddress,
                    DeviceDetails = verify3dRequestConfig.DeviceDetails,
                    RebillingType = verify3dRequestConfig.RebillingType,
                    AuthenticationTypeOnly = verify3dRequestConfig.AuthenticationTypeOnly,
                    Addendums = verify3dRequestConfig.Addendums
                };

                var response = requestExecutor.Verify3d(request).GetAwaiter().GetResult();

                SaveResponse(response, "Verify3dResponseRequestExecutor.json");
            }
        }

        private static void ExecuteVerify3dUsingSafecharge(Safecharge safecharge)
        {
            var authorize3dResponse = ExecuteAuthorize3dUsingSafecharge(safecharge);
            var acsUrl = authorize3dResponse.PaymentOption.Card.ThreeD.AcsUrl;
            var cReq = authorize3dResponse.PaymentOption.Card.ThreeD.CReq;

            if (string.IsNullOrEmpty(authorize3dResponse.Reason) &&
                !string.IsNullOrEmpty(acsUrl) &&
                !string.IsNullOrEmpty(cReq))
            {
                Console.WriteLine("Next, we will simulate the challenge process. Copy the following URL and open it in a browser:");
                Console.WriteLine($"https://3dsecuresafecharge.000webhostapp.com/3Dv2/showUrl.php?acsUrl={acsUrl}&creq={cReq}");
                Console.WriteLine("Press enter when challenge is completed to perform verify3d...");
                Console.ReadLine();

                var response = safecharge.Verify3d(
                    verify3dRequestConfig.Currency,
                    verify3dRequestConfig.Amount,
                    verify3dRequestConfig.PaymentOption,
                    authorize3dResponse.TransactionId,
                    clientUniqueId: verify3dRequestConfig.ClientUniqueId,
                    clientRequestId: verify3dRequestConfig.ClientRequestId,
                    billingAddress: verify3dRequestConfig.BillingAddress,
                    customData: verify3dRequestConfig.CustomData,
                    customSiteName: verify3dRequestConfig.CustomSiteName,
                    merchantDetails: verify3dRequestConfig.MerchantDetails,
                    subMerchant: verify3dRequestConfig.SubMerchant,
                    userId: verify3dRequestConfig.UserId,
                    userTokenId: verify3dRequestConfig.UserTokenId,
                    deviceDetails: verify3dRequestConfig.DeviceDetails,
                    rebillingType: verify3dRequestConfig.RebillingType,
                    authenticationTypeOnly: verify3dRequestConfig.AuthenticationTypeOnly,
                    addendums: verify3dRequestConfig.Addendums).GetAwaiter().GetResult();

                SaveResponse(response, "Verify3dResponseSafecharge.json");
            }
        }

        private static void ExecutePayoutUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var getSessionTokenResponse = ExecuteGetSessionTokenUsingSafechargeRequestExecutor(requestExecutor);

            var request = new PayoutRequest(
                getSessionTokenResponse.MerchantInfo,
                getSessionTokenResponse.Response.SessionToken,
                payoutRequestConfig.UserTokenId,
                payoutRequestConfig.ClientUniqueId,
                payoutRequestConfig.Amount,
                payoutRequestConfig.Currency,
                payoutRequestConfig.UserPaymentOption)
            {
                Comment = payoutRequestConfig.Comment,
                DynamicDescriptor = payoutRequestConfig.DynamicDescriptor,
                MerchantDetails = payoutRequestConfig.MerchantDetails,
                UrlDetails = payoutRequestConfig.UrlDetails,
                DeviceDetails = payoutRequestConfig.DeviceDetails,
                CardData = payoutRequestConfig.CardData,
                UserId = payoutRequestConfig.UserId,
                RebillingType = payoutRequestConfig.RebillingType,
                AuthenticationTypeOnly = payoutRequestConfig.AuthenticationTypeOnly,
                SubMerchant = payoutRequestConfig.SubMerchant,
                Addendums = payoutRequestConfig.Addendums
            };

            var response = requestExecutor.Payout(request).GetAwaiter().GetResult();

            SaveResponse(response, "PayoutResponseRequestExecutor.json");
        }

        private static void ExecutePayoutUsingSafecharge(Safecharge safecharge)
        {
            var transactionResponse = safecharge.Payout(
                payoutRequestConfig.UserTokenId,
                payoutRequestConfig.ClientUniqueId,
                payoutRequestConfig.Amount,
                payoutRequestConfig.Currency,
                payoutRequestConfig.UserPaymentOption,
                comment: payoutRequestConfig.Comment,
                dynamicDescriptor: payoutRequestConfig.DynamicDescriptor,
                merchantDetails: payoutRequestConfig.MerchantDetails,
                urlDetails: payoutRequestConfig.UrlDetails,
                deviceDetails: payoutRequestConfig.DeviceDetails,
                cardData: payoutRequestConfig.CardData,
                userId: payoutRequestConfig.UserId,
                rebillingType: payoutRequestConfig.RebillingType,
                authenticationTypeOnly: payoutRequestConfig.AuthenticationTypeOnly,
                subMerchant: payoutRequestConfig.SubMerchant,
                addendums: payoutRequestConfig.Addendums).GetAwaiter().GetResult();

            SaveResponse(transactionResponse, "PayoutResponseSafecharge.json");
        }

        private static void ExecuteGetCardDetailsUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var getSessionTokenResponse = ExecuteGetSessionTokenUsingSafechargeRequestExecutor(requestExecutor);

            var request = new GetCardDetailsRequest(
                getSessionTokenResponse.MerchantInfo,
                getSessionTokenResponse.Response.SessionToken,
                getCardDetailsRequestConfig.ClientUniqueId,
                getCardDetailsRequestConfig.CardNumber)
            {
                UserId = getCardDetailsRequestConfig.UserId,
                DeviceDetails = getCardDetailsRequestConfig.DeviceDetails,
                RebillingType = getCardDetailsRequestConfig.RebillingType,
                AuthenticationTypeOnly = getCardDetailsRequestConfig.AuthenticationTypeOnly,
                SubMerchant = getCardDetailsRequestConfig.SubMerchant,
                Addendums = getCardDetailsRequestConfig.Addendums
            };

            var response = requestExecutor.GetCardDetails(request).GetAwaiter().GetResult();

            SaveResponse(response, "GetCardDetailsResponseRequestExecutor.json");
        }

        private static void ExecuteGetCardDetailsUsingSafecharge(Safecharge safecharge)
        {
            var transactionResponse = safecharge.GetCardDetails(
                getCardDetailsRequestConfig.ClientUniqueId,
                getCardDetailsRequestConfig.CardNumber,
                userId: getCardDetailsRequestConfig.UserId,
                deviceDetails: getCardDetailsRequestConfig.DeviceDetails,
                rebillingType: getCardDetailsRequestConfig.RebillingType,
                authenticationTypeOnly: getCardDetailsRequestConfig.AuthenticationTypeOnly,
                subMerchant: getCardDetailsRequestConfig.SubMerchant,
                addendums: getCardDetailsRequestConfig.Addendums).GetAwaiter().GetResult();

            SaveResponse(transactionResponse, "GetCardDetailsResponseSafecharge.json");
        }

        private static void ExecuteGetMerchantPaymentMethodsUsingSafechargeRequestExecutor(SafechargeRequestExecutor requestExecutor)
        {
            var getSessionTokenResponse = ExecuteGetSessionTokenUsingSafechargeRequestExecutor(requestExecutor);

            var request = new GetMerchantPaymentMethodsRequest(
                getSessionTokenResponse.MerchantInfo,
                getSessionTokenResponse.Response.SessionToken,
                getMerchantPaymentMethodsRequestConfig.ClientRequestId)
            {
                CurrencyCode = getMerchantPaymentMethodsRequestConfig.CurrencyCode,
                CountryCode = getMerchantPaymentMethodsRequestConfig.CountryCode,
                LanguageCode = getMerchantPaymentMethodsRequestConfig.LanguageCode,
                Type = getMerchantPaymentMethodsRequestConfig.Type,
                UserId = getMerchantPaymentMethodsRequestConfig.UserId,
                DeviceDetails = getMerchantPaymentMethodsRequestConfig.DeviceDetails,
                RebillingType = getMerchantPaymentMethodsRequestConfig.RebillingType,
                AuthenticationTypeOnly = getMerchantPaymentMethodsRequestConfig.AuthenticationTypeOnly,
                SubMerchant = getMerchantPaymentMethodsRequestConfig.SubMerchant,
                Addendums = getMerchantPaymentMethodsRequestConfig.Addendums
            };

            var response = requestExecutor.GetMerchantPaymentMethods(request).GetAwaiter().GetResult();

            SaveResponse(response, "GetMerchantPaymentMethodsResponseRequestExecutor.json");
        }

        private static void ExecuteGetMerchantPaymentMethodsUsingSafecharge(Safecharge safecharge)
        {
            var transactionResponse = safecharge.GetMerchantPaymentMethods(
                getMerchantPaymentMethodsRequestConfig.ClientRequestId,
                currencyCode: getMerchantPaymentMethodsRequestConfig.CurrencyCode,
                countryCode: getMerchantPaymentMethodsRequestConfig.CountryCode,
                languageCode: getMerchantPaymentMethodsRequestConfig.LanguageCode,
                type: getMerchantPaymentMethodsRequestConfig.Type,
                userId: getMerchantPaymentMethodsRequestConfig.UserId,
                deviceDetails: getMerchantPaymentMethodsRequestConfig.DeviceDetails,
                rebillingType: getMerchantPaymentMethodsRequestConfig.RebillingType,
                authenticationTypeOnly: getMerchantPaymentMethodsRequestConfig.AuthenticationTypeOnly,
                subMerchant: getMerchantPaymentMethodsRequestConfig.SubMerchant,
                addendums: getMerchantPaymentMethodsRequestConfig.Addendums).GetAwaiter().GetResult();

            SaveResponse(transactionResponse, "GetMerchantPaymentMethodsResponseSafecharge.json");
        }

        private static void SaveResponse(SafechargeResponse response, string fileName)
        {
            if (response.Status == ResponseStatus.Error)
            {
                Console.WriteLine("Reason: " + response.Reason);
            }
            else
            {
                Console.WriteLine("Status: " + response.Status);
                Console.WriteLine("Session token: " + response.SessionToken);
            }

            var paymentResponseJson = JsonConvert.SerializeObject(response, SerializerSettings);
            var dirPath = Directory.GetCurrentDirectory() + "/Response";
            _ = Directory.CreateDirectory(dirPath);
            string filePath = Path.Combine(dirPath, fileName);
            File.WriteAllText(filePath, paymentResponseJson);

            Console.WriteLine($"The whole response is saved in /Response/{fileName}");
            Console.WriteLine("---");
        }

        private static bool MainMenu(SafechargeRequestExecutor requestExecutor, Safecharge safecharge)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Requests will be executed with the data provided in 'appsettings.json' file.");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Reload config from 'appsettings.json'");
            Console.WriteLine("2) Send PaymentRequest using SafechargeRequestExecutor");
            Console.WriteLine("3) Send PaymentRequest using Safecharge");
            Console.WriteLine("4) Send SettleTransactionRequest using SafechargeRequestExecutor (paymentRequest.TransactionType=Auth)");
            Console.WriteLine("5) Send SettleTransactionRequest using Safecharge (paymentRequest.TransactionType=Auth)");
            Console.WriteLine("6) Send VoidTransactionRequest using SafechargeRequestExecutor");
            Console.WriteLine("7) Send VoidTransactionRequest using Safecharge");
            Console.WriteLine("8) Send RefundTransactionRequest using SafechargeRequestExecutor");
            Console.WriteLine("9) Send RefundTransactionRequest using Safecharge");
            Console.WriteLine("10) Send GetPaymentStatusRequest using SafechargeRequestExecutor");
            Console.WriteLine("11) Send GetPaymentStatusRequest using Safecharge");
            Console.WriteLine("12) Send OpenOrder using SafechargeRequestExecutor");
            Console.WriteLine("13) Send OpenOrder using Safecharge");
            Console.WriteLine("14) Send InitPayment using SafechargeRequestExecutor");
            Console.WriteLine("15) Send InitPayment using Safecharge");
            Console.WriteLine("16) Send Authorize3d using SafechargeRequestExecutor");
            Console.WriteLine("17) Send Authorize3d using Safecharge");
            Console.WriteLine("18) Send Verify3d using SafechargeRequestExecutor");
            Console.WriteLine("19) Send Verify3d using Safecharge");
            Console.WriteLine("20) Send PayoutRequest using SafechargeRequestExecutor");
            Console.WriteLine("21) Send PayoutRequest using Safecharge");
            Console.WriteLine("22) Send GetCardDetailsRequest using SafechargeRequestExecutor");
            Console.WriteLine("23) Send GetCardDetailsRequest using Safecharge");
            Console.WriteLine("24) Send GetMerchantPaymentMethodsRequest using SafechargeRequestExecutor");
            Console.WriteLine("25) Send GetMerchantPaymentMethodsRequest using Safecharge");
            Console.WriteLine("0) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Execute(() => ReloadConfig());
                    return true;
                case "2":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecutePaymentUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "3":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecutePaymentUsingSafecharge(safecharge));
                    return true;
                case "4":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteSettleTransactionUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "5":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteSettleTransactionUsingSafecharge(safecharge));
                    return true;
                case "6":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteVoidTransactionUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "7":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteVoidTransactionUsingSafecharge(safecharge));
                    return true;
                case "8":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteRefundTransactionUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "9":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteRefundTransactionUsingSafecharge(safecharge));
                    return true;
                case "10":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteGetPaymentStatusUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "11":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteGetPaymentStatusUsingSafecharge(safecharge));
                    return true;
                case "12":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteOpenOrderUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "13":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteOpenOrderUsingSafecharge(safecharge));
                    return true;
                case "14":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteInitPaymentUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "15":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteInitPaymentUsingSafecharge(safecharge));
                    return true;
                case "16":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteAuthorize3dUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "17":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteAuthorize3dUsingSafecharge(safecharge));
                    return true;
                case "18":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteVerify3dUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "19":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteVerify3dUsingSafecharge(safecharge));
                    return true;
                case "20":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecutePayoutUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "21":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecutePayoutUsingSafecharge(safecharge));
                    return true;
                case "22":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteGetCardDetailsUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "23":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteGetCardDetailsUsingSafecharge(safecharge));
                    return true;
                case "24":
                    Console.WriteLine("Sample using SafechargeRequestExecutor:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteGetMerchantPaymentMethodsUsingSafechargeRequestExecutor(requestExecutor));
                    return true;
                case "25":
                    Console.WriteLine("Sample using Safecharge service:");
                    Console.WriteLine("---");

                    Execute(() => ExecuteGetMerchantPaymentMethodsUsingSafecharge(safecharge));
                    return true;
                case "0":
                    return false;
                default:
                    return true;
            }
        }

        private static void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
