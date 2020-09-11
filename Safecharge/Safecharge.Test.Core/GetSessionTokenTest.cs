using System;
using System.Net.Http;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Safecharge.Model.Common;
using Safecharge.Request;
using Safecharge.Test.Core.Common;
using Safecharge.Utils;
using Safecharge.Utils.Enum;
using Safecharge.Utils.Exceptions;

namespace Safecharge.Test.Core
{
    public class GetSessionTokenTest : SafechargeRequestExecutorBaseTest
    {
        private readonly MerchantInfo validMerchantInfoShaAlgo = new MerchantInfo(
            MerchantKeyValue,
            MerchantIdValue,
            MerchantSiteIdValue,
            ServerHostValue,
            HashAlgorithmType.SHA256);

        private readonly MerchantInfo validMerchantInfoMd5Algo = new MerchantInfo(
            MerchantKeyValue,
            MerchantIdValue,
            MerchantSiteIdValue,
            ServerHostValue,
            HashAlgorithmType.MD5);

        [Test]
        public void TestCreatingValidMerchant()
        {
            var getSessionTokenRequest = new GetSessionTokenRequest(validMerchantInfoMd5Algo);

            Assert.IsNotNull(getSessionTokenRequest);
        }

        [Test]
        public void TestCreatingNullMerchantKey()
        {
            ActualValueDelegate<object> getSessionTokenRequestDelegate = () => new MerchantInfo(
                null,
                MerchantIdValue,
                MerchantSiteIdValue,
                ServerHostValue,
                HashAlgorithmType.MD5);

            Assert.That(getSessionTokenRequestDelegate, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestCreatingNullMerchantId()
        {
            ActualValueDelegate<object> getSessionTokenRequestDelegate = () => new MerchantInfo(
                MerchantKeyValue,
                null, 
                MerchantSiteIdValue,
                ServerHostValue,
                HashAlgorithmType.MD5);

            Assert.That(getSessionTokenRequestDelegate, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestCreatingNullMerchantSiteId()
        {
            ActualValueDelegate<object> getSessionTokenRequestDelegate = () => new MerchantInfo(
                MerchantKeyValue,
                MerchantIdValue,
                null,
                ServerHostValue,
                HashAlgorithmType.MD5);

            Assert.That(getSessionTokenRequestDelegate, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestCreatingNullServerHost()
        {
            ActualValueDelegate<object> getSessionTokenRequestDelegate = () => new MerchantInfo(
                MerchantKeyValue,
                MerchantIdValue,
                MerchantSiteIdValue,
                null,
                HashAlgorithmType.MD5);

            Assert.That(getSessionTokenRequestDelegate, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestCreatingInvalidServerHost()
        {
            var merchantInfo = new MerchantInfo(
                MerchantKeyValue,
                MerchantIdValue,
                MerchantSiteIdValue,
                ApiConstants.IntegrationHost + "invalid",
                HashAlgorithmType.SHA256);
            var getSessionTokenRequest = new GetSessionTokenRequest(merchantInfo);

            ActualValueDelegate<object> getSessionTokenRequestDelegate = 
                () => requestExecutor.GetSessionToken(getSessionTokenRequest).GetAwaiter().GetResult();

            Assert.That(getSessionTokenRequestDelegate, Throws.TypeOf<SafechargeException>());
        }

        [Test]
        public void TestChecksumWithShaAlgorithm()
        {
            var getSessionTokenRequest = new GetSessionTokenRequest(validMerchantInfoShaAlgo);

            var response = requestExecutor.GetSessionToken(getSessionTokenRequest).GetAwaiter().GetResult();

            Assert.IsTrue(response.ErrorType != ErrorType.InvalidChecksum);
        }

        [Test]
        public void TestChecksumWithMd5Algorithm()
        {
            var getSessionTokenRequest = new GetSessionTokenRequest(validMerchantInfoMd5Algo);

            var response = requestExecutor.GetSessionToken(getSessionTokenRequest).GetAwaiter().GetResult();

            Assert.IsTrue(response.ErrorType != ErrorType.InvalidChecksum);
        }

        [Test]
        public void TestValidRequest()
        {
            var getSessionTokenRequest = new GetSessionTokenRequest(validMerchantInfoShaAlgo);

            var response = requestExecutor.GetSessionToken(getSessionTokenRequest).GetAwaiter().GetResult();

            Assert.IsTrue(response.ApiType == ApiType.Payment);
            Assert.IsNull(response.ClientUniqueId);
            Assert.IsTrue(response.ErrCode == default);
            Assert.IsNull(response.ErrorType);
            Assert.IsNull(response.Hint);
            Assert.IsNotNull(response.InternalRequestId);
            Assert.IsNotNull(response.MerchantId);
            Assert.IsNotNull(response.MerchantSiteId);
            Assert.IsEmpty(response.Reason);
            Assert.IsNotNull(response.SessionToken);
            Assert.IsNotEmpty(response.SessionToken);
            Assert.IsTrue(response.Status == ResponseStatus.Success);
            Assert.IsNotEmpty(response.Version);
        }

        [Test]
        public void TestInitializingSafecharge()
        {
            var safeCharge = new Safecharge(
                MerchantKeyValue,
                MerchantIdValue,
                MerchantSiteIdValue,
                ServerHostValue,
                HashAlgorithmType.SHA256);

            Assert.IsNotNull(safeCharge);
        }

        [Test]
        public void TestInitializingSafechargeWithHttpClient()
        {
            var safeCharge = new Safecharge(
                new HttpClient(),
                new MerchantInfo(
                    MerchantKeyValue,
                    MerchantIdValue,
                    MerchantSiteIdValue,
                    ServerHostValue,
                    HashAlgorithmType.SHA256));

            Assert.IsNotNull(safeCharge);
        }

        [Test]
        public void TestInitializingSafechargeWithWrongMerchantId()
        {
            ActualValueDelegate<object> safeChargeDelegate = () => new Safecharge(
                MerchantKeyValue,
                "invalid",
                MerchantSiteIdValue,
                ServerHostValue,
                HashAlgorithmType.SHA256);

            Assert.That(safeChargeDelegate, Throws.TypeOf<SafechargeConfigurationException>());
        }
    }
}