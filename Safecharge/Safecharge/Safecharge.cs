using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Safecharge.Model.Common;
using Safecharge.Model.Common.Addendum;
using Safecharge.Model.PaymentModels;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Model.PaymentOptionModels.CardModels;
using Safecharge.Model.PaymentOptionModels.InitPayment;
using Safecharge.Model.PaymentOptionModels.OpenOrder;
using Safecharge.Model.PaymentOptionModels.Verify3d;
using Safecharge.Request;
using Safecharge.Response;
using Safecharge.Response.Payment;
using Safecharge.Response.Transaction;
using Safecharge.Utils.Enum;
using Safecharge.Utils.Exceptions;

namespace Safecharge
{
    /// <summary>
    /// This class is a wrapper for the most used endpoints in Safecharge's REST API. 
    /// It makes it easier to execute openOrder, initPayment and createPayment requests.
    /// It keeps information about the merchant and the session token.
    /// </summary>
    /// <inheritdoc/>
    public class Safecharge : ISafecharge
    {
        private readonly MerchantInfo merchantInfo;
        private readonly string sessionToken;
        private readonly SafechargeRequestExecutor safechargeRequestExecutor;

        /// <summary>
        /// Initializes a new instance of the Safecharge wrapper with a configured HttpClient and server information.
        /// </summary>
        /// <param name="configuredHttpClient">httpClient to get the client's properties from</param>
        /// <param name="merchantInfo">Merchant inforamtion</param>
        public Safecharge(
            HttpClient configuredHttpClient,
            MerchantInfo merchantInfo)
        {
            this.merchantInfo = merchantInfo;
            this.safechargeRequestExecutor = new SafechargeRequestExecutor(configuredHttpClient);
            this.sessionToken = this.GetSessionToken();
        }

        /// <summary>
        /// Initializes a new instance of the Safecharge wrapper with a default Safecharge's HttpClient and server information.
        /// </summary>
        /// <param name="merchantInfo">Merchant inforamtion</param>
        public Safecharge(MerchantInfo merchantInfo)
        {
            this.merchantInfo = merchantInfo;
            this.safechargeRequestExecutor = new SafechargeRequestExecutor();
            this.sessionToken = this.GetSessionToken();
        }

        /// <summary>
        /// Initializes a new instance of the Safecharge wrapper with a default Safecharge's HttpClient and server information.
        /// </summary>
        /// <param name="merchantKey">The secret merchant key obtained by the Merchant during integration process with Safecharge</param>
        /// <param name="merchantId">Merchant id in the Safecharge's system</param>
        /// <param name="siteId">Merchant site id in the Safecharge's system</param>
        /// <param name="serverHost">The Safecharge's server address to send the request to</param>
        /// <param name="algorithmType">The hashing algorithm used to generate the checksum</param>
        public Safecharge(
            string merchantKey,
            string merchantId,
            string siteId,
            string serverHost,
            HashAlgorithmType algorithmType)
        {
            this.merchantInfo = new MerchantInfo(merchantKey, merchantId, siteId, serverHost, algorithmType);
            this.safechargeRequestExecutor = new SafechargeRequestExecutor();
            this.sessionToken = this.GetSessionToken();
        }

        public async Task<PaymentResponse> Payment(
            string currency,
            string amount,
            PaymentOption paymentOption,
            List<Item> items = null,
            string userTokenId = null,
            string userId = null,
            string clientUniqueId = null,
            string clientRequestId = null,
            int isRebilling = default,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            AmountDetails amountDetails = null,
            DeviceDetails deviceDetails = null,
            CashierUserDetails userDetails = null,
            UserAddress shippingAddress = null,
            UserAddress billingAddress = null,
            DynamicDescriptor dynamicDescriptor = null,
            MerchantDetails merchantDetails = null,
            Addendums addendums = null,
            UrlDetails urlDetails = null,
            string customSiteName = null,
            string productId = null,
            string customData = null,
            string relatedTransactionId = null,
            string transactionType = null,
            bool autoPayment3D = default,
            string isMoto = null,
            SubMethodDetails subMethodDetails = null,
            string isPartialApproval = null,
            SubMerchant subMerchant = null,
            string orderId = null)
        {
            var paymentRequest = new PaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOption)
            {
                Items = items,
                UserTokenId = userTokenId,
                UserId = userId,
                ClientRequestId = clientRequestId,
                ClientUniqueId = clientUniqueId,
                IsRebilling = isRebilling,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                AmountDetails = amountDetails,
                DeviceDetails = deviceDetails,
                UserDetails = userDetails,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                DynamicDescriptor = dynamicDescriptor,
                MerchantDetails = merchantDetails,
                Addendums = addendums,
                UrlDetails = urlDetails,
                CustomSiteName = customSiteName,
                ProductId = productId,
                CustomData = customData,
                RelatedTransactionId = relatedTransactionId,
                TransactionType = transactionType,
                AutoPayment3D = autoPayment3D,
                IsMoto = isMoto,
                SubMethodDetails = subMethodDetails,
                IsPartialApproval = isPartialApproval,
                SubMerchant = subMerchant,
                OrderId = orderId
            };

            return await safechargeRequestExecutor.Payment(paymentRequest);
        }

        public async Task<SettleTransactionResponse> SettleTransaction(
            string currency,
            string amount,
            string relatedTransactionId,
            string clientUniqueId = null,
            string clientRequestId = null,
            string userId = null,
            Addendums addendums = null,
            string descriptorMerchantName = null,
            string descriptorMerchantPhone = null,
            DynamicDescriptor dynamicDescriptor = null,
            UrlDetails urlDetails = null,
            string authCode = null,
            string customData = null,
            string comment = null,
            string customSiteName = null,
            string productId = null,
            DeviceDetails deviceDetails = null, 
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null)
        {
            var request = new SettleTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                relatedTransactionId)
            { 
                AuthCode = authCode,
                ClientUniqueId = clientUniqueId,
                ClientRequestId = clientRequestId,
                UserId = userId,
                Addendums = addendums,
                DescriptorMerchantName = dynamicDescriptor?.MerchantName ?? descriptorMerchantName,
                DescriptorMerchantPhone = dynamicDescriptor?.MerchantPhone ?? descriptorMerchantPhone,
                UrlDetails = urlDetails,
                CustomData = customData,
                Comment = comment,
                CustomSiteName = customSiteName,
                ProductId = productId,
                DeviceDetails = deviceDetails,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                SubMerchant = subMerchant
            };

            return await safechargeRequestExecutor.SettleTransaction(request);
        }

        public async Task<VoidTransactionResponse> VoidTransaction(
            string currency,
            string amount,
            string relatedTransactionId,
            string clientUniqueId = null,
            string clientRequestId = null,
            string userId = null,
            UrlDetails urlDetails = null,
            string authCode = null,
            string customData = null,
            string comment = null,
            string customSiteName = null,
            string productId = null,
            DeviceDetails deviceDetails = null, 
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null,
            Addendums addendums = null)
        {
            var request = new VoidTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                relatedTransactionId)
            {
                AuthCode = authCode,
                ClientUniqueId = clientUniqueId,
                ClientRequestId = clientRequestId,
                UserId = userId,
                UrlDetails = urlDetails,
                CustomData = customData,
                Comment = comment,
                CustomSiteName = customSiteName,
                ProductId = productId,
                DeviceDetails = deviceDetails,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                SubMerchant = subMerchant,
                Addendums = addendums
            };

            return await safechargeRequestExecutor.VoidTransaction(request);
        }

        public async Task<RefundTransactionResponse> RefundTransaction(
            string currency,
            string amount,
            string relatedTransactionId,
            string clientUniqueId = null,
            string clientRequestId = null,
            string userId = null,
            UrlDetails urlDetails = null,
            string authCode = null,
            string customData = null,
            string comment = null,
            string customSiteName = null,
            string productId = null,
            DeviceDetails deviceDetails = null, 
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null,
            Addendums addendums = null)
        {
            var request = new RefundTransactionRequest(
                merchantInfo,
                sessionToken,
                currency,
                amount,
                relatedTransactionId)
            {
                AuthCode = authCode,
                ClientUniqueId = clientUniqueId,
                ClientRequestId = clientRequestId,
                UserId = userId,
                UrlDetails = urlDetails,
                CustomData = customData,
                Comment = comment,
                CustomSiteName = customSiteName,
                ProductId = productId,
                DeviceDetails = deviceDetails,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                SubMerchant = subMerchant,
                Addendums = addendums
            };

            return await safechargeRequestExecutor.RefundTransaction(request);
        }

        public async Task<GetPaymentStatusResponse> GetPaymentStatus(
            string userId = null,
            DeviceDetails deviceDetails = null,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null,
            Addendums addendums = null)
        {
            var request = new GetPaymentStatusRequest(merchantInfo, sessionToken) 
            {
                UserId = userId,
                DeviceDetails = deviceDetails,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                SubMerchant = subMerchant,
                Addendums = addendums
            };

            return await safechargeRequestExecutor.GetPaymentStatus(request);
        }

        public async Task<OpenOrderResponse> OpenOrder(

            string currency,
            string amount,
            List<Item> items = null,
            OpenOrderPaymentOption paymentOption = null,
            UserPaymentOption userPaymentOption = null,
            string paymentMethod = null,
            string userTokenId = null,
            string clientUniqueId = null,
            string clientRequestId = null,
            string userId = null,
            string authenticationTypeOnly = null,
            AmountDetails amountDetails = null,
            DeviceDetails deviceDetails = null,
            CashierUserDetails userDetails = null,
            UserAddress shippingAddress = null,
            UserAddress billingAddress = null,
            DynamicDescriptor dynamicDescriptor = null,
            MerchantDetails merchantDetails = null,
            Addendums addendums = null,
            UrlDetails urlDetails = null,
            string customSiteName = null,
            string productId = null,
            string customData = null,
            string transactionType = null,
            string isMoto = null,
            string isRebilling = null,
            string rebillingType = null,
            SubMerchant subMerchant = null)
        {
            var request = new OpenOrderRequest(merchantInfo, sessionToken, currency, amount)
            {
                Items = items,
                PaymentOption = paymentOption,
                UserPaymentOption = userPaymentOption,
                PaymentMethod = paymentMethod,
                UserTokenId = userTokenId,
                ClientRequestId = clientRequestId,
                ClientUniqueId = clientUniqueId,
                UserId = userId,
                AuthenticationTypeOnly = authenticationTypeOnly,
                AmountDetails = amountDetails,
                DeviceDetails = deviceDetails,
                UserDetails = userDetails,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                DynamicDescriptor = dynamicDescriptor,
                MerchantDetails = merchantDetails,
                Addendums = addendums,
                UrlDetails = urlDetails,
                CustomSiteName = customSiteName,
                ProductId = productId,
                CustomData = customData,
                TransactionType = transactionType,
                IsMoto = isMoto,
                IsRebilling = isRebilling,
                RebillingType = rebillingType,
                SubMerchant = subMerchant
            };

            return await safechargeRequestExecutor.OpenOrder(request);
        }

        public async Task<InitPaymentResponse> InitPayment(
            string currency,
            string amount,
            InitPaymentPaymentOption paymentOption,
            string userTokenId = null,
            string clientUniqueId = null,
            string clientRequestId = null,
            DeviceDetails deviceDetails = null,
            UrlDetails urlDetails = null,
            string customData = null,
            UserAddress billingAddress = null,
            string userId = null,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null,
            Addendums addendums = null,
            string orderId = null)
        {
            var request = new InitPaymentRequest(merchantInfo, sessionToken, currency, amount, paymentOption)
            {
                UserTokenId = userTokenId,
                ClientRequestId = clientRequestId,
                ClientUniqueId = clientUniqueId,
                DeviceDetails = deviceDetails,
                UrlDetails = urlDetails,
                CustomData = customData,
                BillingAddress = billingAddress,
                UserId = userId,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                SubMerchant = subMerchant,
                Addendums = addendums,
                OrderId = orderId
            };

            return await safechargeRequestExecutor.InitPayment(request);
        }

        public async Task<Authorize3dResponse> Authorize3d(
            string currency,
            string amount,
            PaymentOption paymentOption,
            string relatedTransactionId,
            List<Item> items = null,
            string userTokenId = null,
            string clientUniqueId = null,
            string clientRequestId = null,
            int isRebilling = default,
            AmountDetails amountDetails = null,
            DeviceDetails deviceDetails = null,
            CashierUserDetails userDetails = null,
            UserAddress shippingAddress = null,
            UserAddress billingAddress = null,
            DynamicDescriptor dynamicDescriptor = null,
            MerchantDetails merchantDetails = null,
            Addendums addendums = null,
            UrlDetails urlDetails = null,
            string customSiteName = null,
            string productId = null,
            string customData = null,
            string transactionType = null,
            bool autoPayment3D = default,
            string userId = null,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null)
        {
            var request = new Authorize3dRequest(merchantInfo, sessionToken, currency, amount, paymentOption, relatedTransactionId)
            {
                Items = items,
                UserTokenId = userTokenId,
                ClientRequestId = clientRequestId,
                ClientUniqueId = clientUniqueId,
                IsRebilling = isRebilling,
                AmountDetails = amountDetails,
                DeviceDetails = deviceDetails,
                UserDetails = userDetails,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                DynamicDescriptor = dynamicDescriptor,
                MerchantDetails = merchantDetails,
                Addendums = addendums,
                UrlDetails = urlDetails,
                CustomSiteName = customSiteName,
                ProductId = productId,
                CustomData = customData,
                TransactionType = transactionType,
                AutoPayment3D = autoPayment3D,
                UserId = userId,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                SubMerchant = subMerchant
            };

            return await safechargeRequestExecutor.Authorize3d(request);
        }

        public async Task<Verify3dResponse> Verify3d(
            string currency,
            string amount,
            Verify3dPaymentOption paymentOption,
            string relatedTransactionId,
            string clientUniqueId = null,
            string clientRequestId = null,
            UserAddress billingAddress = null,
            string customData = null,
            string customSiteName = null,
            MerchantDetails merchantDetails = null,
            SubMerchant subMerchant = null,
            string userId = null,
            string userTokenId = null,
            DeviceDetails deviceDetails = null,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            Addendums addendums = null)
        {
            var request = new Verify3dRequest(merchantInfo, sessionToken, currency, amount, paymentOption, relatedTransactionId)
            {
                UserId = userId,
                UserTokenId = userTokenId,
                ClientRequestId = clientRequestId,
                ClientUniqueId = clientUniqueId,
                SubMerchant = subMerchant,
                BillingAddress = billingAddress,
                MerchantDetails = merchantDetails,
                CustomSiteName = customSiteName,
                CustomData = customData,
                DeviceDetails = deviceDetails,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                Addendums = addendums
            };

            return await safechargeRequestExecutor.Verify3d(request);
        }

        public async Task<PayoutResponse> Payout(
            string userTokenId,
            string clientUniqueId,
            string amount,
            string currency,
            UserPaymentOption userPaymentOption,
            string comment = null,
            DynamicDescriptor dynamicDescriptor = null,
            MerchantDetails merchantDetails = null,
            UrlDetails urlDetails = null,
            DeviceDetails deviceDetails = null,
            CardData cardData = null,
            string userId = null,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null,
            Addendums addendums = null)
        {
            var request = new PayoutRequest(
                merchantInfo,
                sessionToken,
                userTokenId,
                clientUniqueId,
                amount,
                currency,
                userPaymentOption)
            {
                Comment = comment,
                DynamicDescriptor = dynamicDescriptor,
                MerchantDetails = merchantDetails,
                UrlDetails = urlDetails,
                DeviceDetails = deviceDetails,
                CardData = cardData,
                UserId = userId,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                SubMerchant = subMerchant,
                Addendums = addendums
            };

            return await safechargeRequestExecutor.Payout(request);
        }

        public async Task<GetCardDetailsResponse> GetCardDetails(
            string clientUniqueId,
            string cardNumber,
            string userId = null,
            DeviceDetails deviceDetails = null,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null,
            Addendums addendums = null)
        {
            var request = new GetCardDetailsRequest(
                merchantInfo,
                sessionToken,
                clientUniqueId,
                cardNumber)
            {
                UserId = userId,
                DeviceDetails = deviceDetails,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                SubMerchant = subMerchant,
                Addendums = addendums
            };

            return await safechargeRequestExecutor.GetCardDetails(request);
        }

        public async Task<GetMerchantPaymentMethodsResponse> GetMerchantPaymentMethods(
            string clientRequestId,
            string currencyCode = null,
            string countryCode = null,
            string languageCode = null,
            string type = null,
            string userId = null,
            DeviceDetails deviceDetails = null,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null,
            Addendums addendums = null)
        {
            var request = new GetMerchantPaymentMethodsRequest(
                merchantInfo,
                sessionToken,
                clientRequestId)
            {
                CurrencyCode = currencyCode,
                CountryCode = countryCode,
                LanguageCode = languageCode,
                Type = type,
                UserId = userId,
                DeviceDetails = deviceDetails,
                RebillingType = rebillingType,
                AuthenticationTypeOnly = authenticationTypeOnly,
                SubMerchant = subMerchant,
                Addendums = addendums
            };

            return await safechargeRequestExecutor.GetMerchantPaymentMethods(request);
        }

        public Task<GetDCCResponse> GetDccDetails(string clientRequestId, string clientUniqueId, string cardNumber, string originalAmount, string originalCurrency, string currency)
        {
            var response = this.safechargeRequestExecutor.GetDCCDetails(
                new GetDCCRequest(this.merchantInfo, this.sessionToken)
                {
                    SessionToken = sessionToken,
                    MerchantId = merchantInfo.MerchantId,
                    MerchantSiteId = merchantInfo.MerchantSiteId,
                    ClientRequestId = clientRequestId,
                    ClientUniqueId = clientUniqueId,
                    Amount = originalAmount,
                    OriginalAmount = originalAmount,
                    OriginalCurrency = originalCurrency,
                    Currency = currency,
                    Apm = "apmgw_expresscheckout"

                });

            return response;
        }

        private string GetSessionToken()
        {
            var request = new GetSessionTokenRequest(merchantInfo);

            var response = this.safechargeRequestExecutor.GetSessionToken(request).GetAwaiter().GetResult();

            if (response.Status == ResponseStatus.Error)
            {
                throw new SafechargeConfigurationException(response.Reason);
            }

            return response.SessionToken;
        }
    }
}
