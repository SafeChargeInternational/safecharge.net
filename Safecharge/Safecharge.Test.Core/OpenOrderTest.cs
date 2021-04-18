using NUnit.Framework;
using Safecharge.Model.Common;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils.Enum;

namespace Safecharge.Test.Core
{
    public class OpenOrderTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestOpenOrderSuccess()
        {
            var request = new OpenOrderRequest(merchantInfo,
                sessionToken, currency, amount)
            {
                MerchantDetails = new MerchantDetails
                {
                    CustomField1 = "test",
                    CustomField2 = "test",
                    CustomField3 = "test",
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
                }

            };

            var response = requestExecutor.OpenOrder(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
        }
    }
}
