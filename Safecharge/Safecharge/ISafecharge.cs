using System.Collections.Generic;
using System.Threading.Tasks;
using Safecharge.Response;
using Safecharge.Response.Payment;
using Safecharge.Response.Transaction;
using Safecharge.Model.Common;
using Safecharge.Model.Common.Addendum;
using Safecharge.Model.PaymentModels;
using Safecharge.Model.PaymentOptionModels.CardModels;
using Safecharge.Model.PaymentOptionModels.InitPayment;
using Safecharge.Model.PaymentOptionModels.OpenOrder;
using Safecharge.Model.PaymentOptionModels.Verify3d;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Utils.Exceptions;

namespace Safecharge
{
    /// <summary>
    /// This is an interface for a wrapper for the most used endpoints in Safecharge's REST API. It makes it easier to execute openOrder, 
    /// initPayment and createPayment requests. First you have to invoke initialize method and then choose the corresponding request method.
    /// </summary>
    public interface ISafecharge
    {
        /// <summary>
        /// This method should be used to create request for payment endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#payment">here</a>.</para>
        /// </summary>
        /// <param name="currency">(Required) The three character ISO currency code of the transaction.</param>
        /// <param name="amount">(Required) The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="paymentOption">(Required) Details about the payment method.</param>
        /// <param name="items">List of items that will be purchased.</param>
        /// <param name="userTokenId">ID of the user in merchant system.</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="clientRequestId">Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        /// <param name="isRebilling">When performing recurring/rebilling, use this field to indicate the recurring step. (0 or 1)</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="amountDetails">Amount details information.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="userDetails">Details about the user which include the user's name, email, address, etc.</param>
        /// <param name="shippingAddress">Shipping address related to a user's order.</param>
        /// <param name="billingAddress">Billing address related to a user payment option. Since order can contain only one payment option billing address is part of the order parameters.</param>
        /// <param name="dynamicDescriptor">Merchant descriptor - this is the message(Merchant's name and phone) that the user will see in his payment bank report.</param>
        /// <param name="merchantDetails">Optional custom fields.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <param name="urlDetails">Although DMN response can be configured per merchant site, it will allow to dynamically return the DMN to the provided address per request.</param>
        /// <param name="customSiteName">The merchant’s site name. This is useful for merchants operating many websites that are distinguished only by name. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="productId">A free text field used to identify the product/service sold. If this parameter is not sent or is sent with an empty value, then it contains the concatenation of all item names up until the parameter maximum length. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="customData">This parameter can be used to pass any type of information. If sent in request, then it is passed on to the payments gateway, and is visible in Nuvei’s back-office tool transaction reporting and is returned in response.</param>
        /// <param name="relatedTransactionId">The ID of the original transaction.</param>
        /// <param name="transactionType">ApiConstants.TransactionTypeSale / ApiConstants.TransactionTypeAuth / ApiConstants.TransactionTypePreAuth</param>
        /// <param name="autoPayment3D"></param>
        /// <param name="isMoto">Mark the transaction as MOTO (mail order/telephone order).</param>
        /// <param name="subMethodDetails">Submethod details.</param>
        /// <param name="isPartialApproval">This describes a situation where the deposit was completed and processed with an amount lower than the requested amount due to a consumer’s lack of funds within the desired payment method.</param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <param name="orderId">Merchant Order ID to be used as input parameter in update method and payment methods. The parameter passed to define which merchant order to update.</param>
        /// <returns><see cref="PaymentResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<PaymentResponse> Payment(
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
            string orderId = null);

        /// <summary>
        /// This method should be used to create request for settleTransaction endpoint in Safecharge's REST API.
        /// <para>If "<see cref="DynamicDescriptor"/> dynamicDescriptor" parameter is passed, it overrides descriptorMerchantName and descriptorMerchantPhone parameters.</para>
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#settleTransaction">here</a>.</para>
        /// </summary>
        /// <param name="currency">(Required) The three character ISO currency code of the transaction.</param>
        /// <param name="amount">(Required) The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="relatedTransactionId">(Required) The ID of the original transaction.</param>
        /// <<param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="clientRequestId">Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <param name="descriptorMerchantName">The name that will appear in the payment statement.</param>
        /// <param name="descriptorMerchantPhone">The phone that will appear in the payment statement.</param>
        /// <param name="dynamicDescriptor">Merchant descriptor - this is the message(Merchant's name and phone) that the user will see in his payment bank report</param>
        /// <param name="urlDetails">Although DMN response can be configured per merchant site, it will allow to dynamically return the DMN to the provided address per request.</param>
        /// <param name="authCode">The authorization code of the related auth transaction, to be compared to the original one.</param>
        /// <param name="customData">This parameter can be used to pass any type of information. If sent in request, then it is passed on to the payments gateway, and is visible in Nuvei’s back-office tool transaction reporting and is returned in response.</param>
        /// <param name="comment">Enables the addition of a free text comment to the request.</param>
        /// <param name="customSiteName">The merchant’s site name. This is useful for merchants operating many websites that are distinguished only by name. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="productId">A free text field used to identify the product/service sold. If this parameter is not sent or is sent with an empty value, then it contains the concatenation of all item names up until the parameter maximum length. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <returns><see cref="SettleTransactionResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<SettleTransactionResponse> SettleTransaction(
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
            SubMerchant subMerchant = null);

        /// <summary>
        /// This method should be used to create request for voidTransaction endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#voidTransaction">here</a>.</para>
        /// </summary>
        /// <param name="currency">(Required) The three character ISO currency code of the transaction.</param>
        /// <param name="amount">(Required) The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="relatedTransactionId">(Required) The ID of the original transaction.</param>
        /// <<param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="clientRequestId">Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="urlDetails">Although DMN response can be configured per merchant site, it will allow to dynamically return the DMN to the provided address per request.</param>
        /// <param name="authCode">The authorization code of the related auth transaction, to be compared to the original one.</param>
        /// <param name="customData">This parameter can be used to pass any type of information. If sent in request, then it is passed on to the payments gateway, and is visible in Nuvei’s back-office tool transaction reporting and is returned in response.</param>
        /// <param name="comment">Enables the addition of a free text comment to the request.</param>
        /// <param name="customSiteName">The merchant’s site name. This is useful for merchants operating many websites that are distinguished only by name. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="productId">A free text field used to identify the product/service sold. If this parameter is not sent or is sent with an empty value, then it contains the concatenation of all item names up until the parameter maximum length. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <returns><see cref="VoidTransactionResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<VoidTransactionResponse> VoidTransaction(
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
            Addendums addendums = null);

        /// <summary>
        /// This method should be used to create request for refundTransaction endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#refundTransaction">here</a>.</para>
        /// </summary>
        /// <param name="currency">(Required) The three character ISO currency code of the transaction.</param>
        /// <param name="amount">(Required) The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="relatedTransactionId">(Required) The ID of the original transaction.</param>
        /// <<param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="clientRequestId">Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="urlDetails">Although DMN response can be configured per merchant site, it will allow to dynamically return the DMN to the provided address per request.</param>
        /// <param name="authCode">The authorization code of the related auth transaction, to be compared to the original one.</param>
        /// <param name="customData">This parameter can be used to pass any type of information. If sent in request, then it is passed on to the payments gateway, and is visible in Nuvei’s back-office tool transaction reporting and is returned in response.</param>
        /// <param name="comment">Enables the addition of a free text comment to the request.</param>
        /// <param name="customSiteName">The merchant’s site name. This is useful for merchants operating many websites that are distinguished only by name. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="productId">A free text field used to identify the product/service sold. If this parameter is not sent or is sent with an empty value, then it contains the concatenation of all item names up until the parameter maximum length. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <returns><see cref="RefundTransactionResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<RefundTransactionResponse> RefundTransaction(
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
            Addendums addendums = null);

        /// <summary>
        /// This method should be used to create request for getPaymentStatus endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#getPaymentStatus">here</a>.</para>
        /// </summary>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <returns><see cref="GetPaymentStatusResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<GetPaymentStatusResponse> GetPaymentStatus(
            string userId = null,
            DeviceDetails deviceDetails = null,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null,
            Addendums addendums = null);

        /// <summary>
        /// This method should be used to create request for openOrder endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#openOrder">here</a>.</para>
        /// </summary>
        /// <param name="currency">(Required) The three character ISO currency code of the transaction.</param>
        /// <param name="amount">(Required) The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="items">List of items that will be purchased.</param>
        /// <param name="paymentOption">Details about the payment method.</param>
        /// <param name="userPaymentOption">User payment option details</param>
        /// <param name="paymentMethod">Specifies the payment method name of the payment option to be charged.</param>
        /// <param name="userTokenId">ID of the user in merchant system.</param>
        /// <<param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="clientRequestId">Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="amountDetails">Amount details information.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="userDetails">Details about the user which include the user's name, email, address, etc.</param>
        /// <param name="shippingAddress">Shipping address related to a user's order.</param>
        /// <param name="billingAddress">Billing address related to a user payment option. Since order can contain only one payment option billing address is part of the order parameters.</param>
        /// <param name="dynamicDescriptor">Merchant descriptor - this is the message(Merchant's name and phone) that the user will see in his payment bank report</param>
        /// <param name="merchantDetails">Optional custom fields.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <param name="urlDetails">Although DMN response can be configured per merchant site, it will allow to dynamically return the DMN to the provided address per request.</param>
        /// <param name="customSiteName">The merchant’s site name. This is useful for merchants operating many websites that are distinguished only by name. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="productId">A free text field used to identify the product/service sold. If this parameter is not sent or is sent with an empty value, then it contains the concatenation of all item names up until the parameter maximum length. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="customData">This parameter can be used to pass any type of information. If sent in request, then it is passed on to the payments gateway, and is visible in Nuvei’s back-office tool transaction reporting and is returned in response.</param>
        /// <param name="transactionType">ApiConstants.TransactionTypeSale / ApiConstants.TransactionTypeAuth / ApiConstants.TransactionTypePreAuth</param>
        /// <param name="isMoto">Mark the transaction as MOTO (mail order/telephone order).</param>
        /// <param name="isRebilling">When performing recurring/rebilling, use this field to indicate the recurring step. (0 or 1)</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <returns><see cref="OpenOrderResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<OpenOrderResponse> OpenOrder(
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
            SubMerchant subMerchant = null);

        /// <summary>
        /// This method should be used to create request for initPayment endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#initPayment">here</a>.</para>
        /// </summary>
        /// <param name="currency">(Required) The three character ISO currency code of the transaction.</param>
        /// <param name="amount">(Required) The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="paymentOption">(Required) Details about the payment method.</param>
        /// <param name="userTokenId">ID of the user in merchant system.</param>
        /// <<param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="clientRequestId">Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="urlDetails">Although DMN response can be configured per merchant site, it will allow to dynamically return the DMN to the provided address per request.</param>
        /// <param name="customData">This parameter can be used to pass any type of information. If sent in request, then it is passed on to the payments gateway, and is visible in Nuvei’s back-office tool transaction reporting and is returned in response.</param>
        /// <param name="billingAddress">Billing address related to a user payment option. Since order can contain only one payment option billing address is part of the order parameters.</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <param name="orderId">Merchant Order ID to be used as input parameter in update method and payment methods. The parameter passed to define which merchant order to update.</param>
        /// <returns><see cref="InitPaymentResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<InitPaymentResponse> InitPayment(
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
            string orderId = null);

        /// <summary>
        /// This method should be used to create request for authorize3d endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#authorize3dAPI">here</a>.</para>
        /// </summary>
        /// <param name="currency">(Required) The three character ISO currency code of the transaction.</param>
        /// <param name="amount">(Required) The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="paymentOption">(Required) Details about the payment method.</param>
        /// <param name="relatedTransactionId">(Required) The ID of the original transaction.</param>
        /// <param name="items">List of items that will be purchased.</param>
        /// <param name="userTokenId">ID of the user in merchant system.</param>
        /// <<param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="clientRequestId">Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        /// <param name="isRebilling">When performing recurring/rebilling, use this field to indicate the recurring step. (0 or 1)</param>
        /// <param name="amountDetails">Amount details information.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="userDetails">Details about the user which include the user's name, email, address, etc.</param>
        /// <param name="shippingAddress">Shipping address related to a user's order.</param>
        /// <param name="billingAddress">Billing address related to a user payment option. Since order can contain only one payment option billing address is part of the order parameters.</param>
        /// <param name="dynamicDescriptor">Merchant descriptor - this is the message(Merchant's name and phone) that the user will see in his payment bank report</param>
        /// <param name="merchantDetails">Optional custom fields.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <param name="urlDetails">Although DMN response can be configured per merchant site, it will allow to dynamically return the DMN to the provided address per request.</param>
        /// <param name="customSiteName">The merchant’s site name. This is useful for merchants operating many websites that are distinguished only by name. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="productId">A free text field used to identify the product/service sold. If this parameter is not sent or is sent with an empty value, then it contains the concatenation of all item names up until the parameter maximum length. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="customData">This parameter can be used to pass any type of information. If sent in request, then it is passed on to the payments gateway, and is visible in Nuvei’s back-office tool transaction reporting and is returned in response.</param>
        /// <param name="transactionType">Transaction Type of the request. Possible values for payment request: Auth / Sale / PreAuth.</param>
        /// <param name="autoPayment3D"></param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <returns><see cref="Authorize3dResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<Authorize3dResponse> Authorize3d(
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
            SubMerchant subMerchant = null);

        /// <summary>
        /// This method should be used to create request for verify3d endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#verify3d">here</a>.</para>
        /// </summary>
        /// <param name="currency">(Required) The three character ISO currency code of the transaction.</param>
        /// <param name="amount">(Required) The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="paymentOption">(Required) Details about the payment method.</param>
        /// <param name="relatedTransactionId">(Required) The ID of the original transaction.</param>
        /// <<param name="clientUniqueId">ID of the transaction in the merchant’s system. This must be sent in order to perform future actions, such as: reconciliation, identifying the transaction in the event of any issues, etc.</param>
        /// <param name="clientRequestId">Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        /// <param name="billingAddress">Billing address related to a user payment option. Since order can contain only one payment option billing address is part of the order parameters.</param>
        /// <param name="customData">This parameter can be used to pass any type of information. If sent in request, then it is passed on to the payments gateway, and is visible in Nuvei’s back-office tool transaction reporting and is returned in response.</param>
        /// <param name="customSiteName">The merchant’s site name. This is useful for merchants operating many websites that are distinguished only by name. Risk rules and traffic management rules are usually built based on this field value.</param>
        /// <param name="merchantDetails">Optional custom fields.</param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="userTokenId">ID of the user in merchant system.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <returns><see cref="Verify3dResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<Verify3dResponse> Verify3d(
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
            Addendums addendums = null);

        /// <summary>
        /// This method should be used to create request for payout endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#payout">here</a>.</para>
        /// </summary>
        /// <param name="userTokenId">(Required) ID of the user in merchant system.</param>
        /// <param name="clientUniqueId">(Required) ID of the transaction in the merchant’s system.</param>
        /// <param name="amount">(Required) The transaction amount. (E.g. 1, 101.10 - decimal representation of the amount as <see cref="string"/>.</param>
        /// <param name="currency">(Required) The three character ISO currency code of the transaction.</param>
        /// <param name="userPaymentOption">(Required) User payment option details.</param>
        /// <param name="comment">Enables the addition of a free text comment to the request.</param>
        /// <param name="dynamicDescriptor">Merchant descriptor - this is the message(Merchant's name and phone) that the user will see in his payment bank report</param>
        /// <param name="merchantDetails">Optional custom fields.</param>
        /// <param name="urlDetails">Although DMN response can be configured per merchant site, it will allow to dynamically return the DMN to the provided address per request.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="cardData">Credit/debit/prepaid card data.</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <returns><see cref="PayoutResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<PayoutResponse> Payout(
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
            Addendums addendums = null);

        /// <summary>
        /// This method should be used to create request for getCardDetails endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/API/main/indexMain_v1_0.html?json#getCardDetails">here</a>.</para>
        /// </summary>
        /// <param name="clientUniqueId">(Required) ID of the transaction in the merchant’s system.</param>
        /// <param name="cardNumber">(Required) CardNumber or bin must be populated</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <returns><see cref="GetCardDetailsResponse"/> data</returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<GetCardDetailsResponse> GetCardDetails(
            string clientUniqueId,
            string cardNumber,
            string userId = null,
            DeviceDetails deviceDetails = null,
            string rebillingType = null,
            string authenticationTypeOnly = null,
            SubMerchant subMerchant = null,
            Addendums addendums = null);

        /// <summary>
        /// This method should be used to create request for GetMerchantPaymentMethods endpoint in Safecharge's REST API.
        /// <para>See documentation <a href="https://www.safecharge.com/docs/api/?json#getMerchantPaymentMethods">here</a>.</para>
        /// </summary>
        /// <param name="clientRequestId">(Required) Use this advanced field to prevent idempotency. Use it to uniquely identify the request you are submitting. If our system receives two calls with the same clientRequestId, it refuses the second call as it will assume idempotency.</param>
        /// <param name="currencyCode">The three letter ISO currency code that the transaction is to be completed in.</param>
        /// <param name="countryCode">The two-letter ISO country code the transaction is to be completed in.</param>
        /// <param name="languageCode">The language the transaction is to be completed in.</param>
        /// <param name="type">The type of the payment methods to be returned. Possible values: DEPOSIT, WITHDRAWAL. If no value sent, then default value is DEPOSIT.</param>
        /// <param name="userId">The customer’s ID as per the merchant’s userid.</param>
        /// <param name="deviceDetails">Device details information.</param>
        /// <param name="rebillingType">When performing recurring/rebilling, use this field to indicate the recurring type. (RECURRING, MIT)</param>
        /// <param name="authenticationTypeOnly"></param>
        /// <param name="subMerchant">Submerchant information.</param>
        /// <param name="addendums">This block contain industry specific addendums such as: Local payment, Hotel, Airline etc.</param>
        /// <returns>
        /// <see cref="GetMerchantPaymentMethodsResponse"/> data
        /// </returns>
        /// <exception cref="SafechargeConfigurationException">If status of the response is Error</exception>
        Task<GetMerchantPaymentMethodsResponse> GetMerchantPaymentMethods(
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
            Addendums addendums = null);
    }
}
