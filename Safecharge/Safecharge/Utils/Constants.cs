using System.Collections.Generic;
using Safecharge.Utils.Enum;

namespace Safecharge.Utils
{
    public static class Constants
    {
        public const string TimeStampFormat = "yyyyMMddHHmmss";

        public static Dictionary<ChecksumOrderMapping, List<string>> RequestChecksumParameters = new Dictionary<ChecksumOrderMapping, List<string>>()
        {
            { ChecksumOrderMapping.ApiGenericChecksumMapping, new List<string>{ "MerchantId", "MerchantSiteId", "ClientRequestId", "Amount", "Currency", "TimeStamp" } },
            { ChecksumOrderMapping.ApiBasicChecksumMapping, new List<string>{ "MerchantId", "MerchantSiteId", "ClientRequestId", "TimeStamp" } },
            { 
                ChecksumOrderMapping.SettleGwTransactionChecksumMapping, 
                new List<string>
                { 
                    "MerchantId", "MerchantSiteId", "ClientRequestId", "ClientUniqueId", "Amount", "Currency", "RelatedTransactionId", "AuthCode", "DescriptorMerchantName", "DescriptorMerchantPhone", "Comment", "UrlDetails", "TimeStamp" 
                } 
            },
            {
                ChecksumOrderMapping.VoidGwTransactionChecksumMapping,
                new List<string>
                {
                    "MerchantId", "MerchantSiteId", "ClientRequestId", "ClientUniqueId", "Amount", "Currency", "RelatedTransactionId", "AuthCode", "Comment", "UrlDetails", "TimeStamp"
                }
            },
            {
                ChecksumOrderMapping.RefundGwTransactionChecksumMapping,
                new List<string>
                {
                    "MerchantId", "MerchantSiteId", "ClientRequestId", "ClientUniqueId", "Amount", "Currency", "RelatedTransactionId", "AuthCode", "Comment", "UrlDetails", "TimeStamp"
                }
            },
            { ChecksumOrderMapping.NoChecksumMapping, new List<string> { } },
        };

        public const int MaxLengthStringId = 45;
        public const int MaxLengthStringDefault = 255;
        public const int MaxLengthCardNumber = 20;
        public const int MaxLengthCardHolderName = 70;
        public const int MaxLengthFirstName = 30;
        public const int MaxLengthLastName = 40;
        public const int MaxLengthEmail = 100;
        public const int MaxLengthPhone = 18;
        public const int MaxLengthAddress = 60;
        public const int MaxLengthCity = 30;
        public const int MaxLengthCountry = 2;
        public const int MaxLengthState = 2;
        public const int MaxLengthZip = 10;
        public const int MaxLengthCounty = 255;
        public const int MaxLengthDateDefault = 8;
        public const int MaxLengthMerchantDescriptorName = 25;
        public const int MaxLengthMerchantDescriptorPhone = 13;
        public const int MaxLengthUrl = 1000;

        public const int MinLengthStringDefault = 1;

        public const string PatternDateOfBirth = "yyyy-MM-dd";
        public const string PatternIpAddress = "^(25[0-5]|2[0-4]\\d|[0-1]?\\d?\\d)(\\.(25[0-5]|2[0-4]\\d|[0-1]?\\d?\\d)){3}$";
    }
}
