using NUnit.Framework;
using Safecharge.Model.Common;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;


namespace Safecharge.Test.Core
{
    public class GetMerchantPaymentMethodsTest : SafechargeRequestExecutorBaseTest
    {
        [Test]
        public void TestGetPaymentStatusSuccess()
        {
            var request = new SafechargeRequestExecutor();
            var merchentInfo = new MerchantInfo()
            {
                MerchantId = MerchantIdValue,
                MerchantSiteId = MerchantSiteIdValue,
                ServerHost = ServerHostValue
            };
            var getMerchantPaymentMethodsRequest = new GetMerchantPaymentMethodsRequest(merchentInfo, sessionToken, "1234578");
        
            var response = request.GetMerchantPaymentMethods(getMerchantPaymentMethodsRequest).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
        }
    }
}
