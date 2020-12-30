using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using Safecharge.Model.Common;
using Safecharge.Model.PaymentOptionModels.CardModels;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core.Common
{
    public class SafechargeBaseTest
    {
        protected static readonly string MerchantKeyValue = ConfigurationManager.AppSettings["MerchantKey"];
        protected static readonly string MerchantIdValue = ConfigurationManager.AppSettings["MerchantId"];
        protected static readonly string MerchantSiteIdValue = ConfigurationManager.AppSettings["MerchantSiteId"];
        protected static readonly string ServerHostValue = ConfigurationManager.AppSettings["ServerHost"];

        protected ISafecharge safecharge;

        protected readonly string amount = "10";
        protected readonly string currency = "USD";
        protected readonly Card card = new Card
        {
            CardNumber = "4000023104662535",
            CardHolderName = "John Smith",
            ExpirationMonth = "12",
            ExpirationYear = "22",
            CVV = "217"
        };
        protected readonly UserAddress userAddress = new UserAddress
        {
            Email = "asd@asdasd.com",
            Address = "address",
            City = "city",
            Country = "DE",
            State = "",
            Zip = "1335"            
        };
        protected readonly List<Item> items = new List<Item>
        {
            new Item
            {
                Name = "testItem1",
                Price = "10",
                Quantity = "1"
            }
        };

        [SetUp]
        public void Setup()
        {
            safecharge = new Safecharge(
                MerchantKeyValue,
                MerchantIdValue,
                MerchantSiteIdValue,
                ServerHostValue,
                HashAlgorithmType.SHA256);
        }
    }
}
