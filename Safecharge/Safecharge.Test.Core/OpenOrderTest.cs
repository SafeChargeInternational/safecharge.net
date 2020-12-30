using NUnit.Framework;
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
            var request = new OpenOrderRequest(merchantInfo, sessionToken, currency, amount);

            var response = requestExecutor.OpenOrder(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsEmpty(response.Reason);
            Assert.AreEqual(ResponseStatus.Success, response.Status);
        }
    }
}
