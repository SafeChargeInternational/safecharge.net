using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Safecharge.Model.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Request.Common
{
    /// <summary>
    /// Abstract class to be used as a base for all of the requests to SafeCharge's servers.
    /// </summary>
    public abstract class SafechargeBaseRequest
    {
        /// <summary>
        /// Empty constructor used for mapping from config file.
        /// </summary>
        public SafechargeBaseRequest() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafechargeBaseRequest"/> with the required parameters.
        /// </summary>
        /// <param name="merchantInfo">Merchant's data (E.g. secret key, the merchant id, the merchant site id, etc.)</param>
        /// <param name="checksumOrderMapping">Type of checksum.</param>
        /// <param name="sessionToken">The session identifier returned by /getSessionToken.</param>
        public SafechargeBaseRequest(
            MerchantInfo merchantInfo,
            ChecksumOrderMapping checksumOrderMapping,
            string sessionToken)
        {            
            this.ServerHost = merchantInfo.ServerHost;
            this.TimeStamp = DateTime.Now.ToString(Constants.TimeStampFormat);
            this.SessionToken = sessionToken;
            this.MerchantKey = merchantInfo.MerchantKey;
            this.HashAlgorithmType = merchantInfo.HashAlgorithm;
            this.ChecksumOrderMapping = checksumOrderMapping;
        }

        protected string MerchantKey { get; private set; }

        protected HashAlgorithmType HashAlgorithmType { get; private set; }

        protected ChecksumOrderMapping ChecksumOrderMapping { get; set; }

        public string InternalRequestId { get; set; }

        public string ClientRequestId { get; set; }

        public string TimeStamp { get; set; }

        public string Checksum => this.CalculateChecksum(this.ChecksumOrderMapping);

        public string SessionToken { get; set; }

        public string ServerHost { get; set; }

        public Uri RequestUri { get; set; }

        public string WebMasterId 
        {
            get
            {
                var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly()?.Location);

                return fvi != null ? $"{ApiConstants.SdkVersion}_{fvi.FileVersion}" : string.Empty; 
            }
        } 

        protected Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri($"{this.ServerHost}{relativePath}");
            var uriBuilder = new UriBuilder(endpoint) { Query = queryString };
            return uriBuilder.Uri;
        }

        private string CalculateChecksum(ChecksumOrderMapping checksumOrderMapping = ChecksumOrderMapping.ApiGenericChecksumMapping)
        {
            var checksum = new StringBuilder();

            if (Constants.RequestChecksumParameters.ContainsKey(checksumOrderMapping))
            {
                List<string> paramsOrder = Constants.RequestChecksumParameters[checksumOrderMapping];

                foreach (string parameterName in paramsOrder)
                {
                    checksum.Append(this.GetPropValue(parameterName));
                }
            }

            checksum.Append(this.MerchantKey);
            return this.GetHash(this.HashAlgorithmType, checksum.ToString());
        }

        private string GetHash(HashAlgorithmType hashAlgorithmType, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithmType == HashAlgorithmType.MD5
                ? new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(input))
                : hashAlgorithmType == HashAlgorithmType.SHA256
                    ? SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input))
                    : null;

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            if (data != null)
            {
                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private string GetPropValue(string propName)
        {
            return this.GetType()?.GetProperty(propName)?.GetValue(this, null)?.ToString();
        }
    }
}
