# SafeCharge REST API SDK for .NET.

SafeCharge’s REST API SDK for .NET provides developer tools for accessing Safecharge's REST API. SafeCharge’s REST API is a simple, easy to use, secure and stateless API, which enables online merchants and service providers to process consumer payments through SafeCharge’s payment gateway. For SafeCharge REST API documentation, please see: https://www.safecharge.com/docs/api/


## Requirements

You have the following options to choose from:
- .NET Core 2.0 or later.
- .NET Framework 4.6.1 and later 
- Mono 5.4+
- Xamarin.iOS 10.14+
- Xamarin.Mac 3.8+
- Xamarin.Android 8.0+
- Universal Windows Platform 10.0.16299+
- Unity 2018.1+

### NuGet
*To be added upon NuGet upload...*

## Running your first request

You only need to setup a HTTP Client and to provide the SafeCharge API host to the request executor and then you can start building requests and send them to the SafeCharge API. 

Check how simple it is trough this sample:

```c#
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Safecharge.Model;
using Safecharge.Request;
using Safecharge.Utils;
using Safecharge.Utils.Enum;

namespace Safecharge.Sample
{
    class Program
    {
        static void Main()
        {
            var reqExecutor = new SafechargeRequestExecutor();

            var merchantInfo = new MerchantInfo(
                "MERCHANT_KEY_PROVIDED_BY_SAFECHARGE",
                "MERCHANT_ID_PROVIDED_BY_SAFECHARGE",
                "MERCHANT_SITE_ID_PROVIDED_BY_SAFECHARGE",
                ApiConstants.IntegrationHost,
                HashAlgorithmType.SHA256);

            var getSessionTokenRequest = new GetSessionTokenRequest(merchantInfo);

            var response = reqExecutor.GetSessionToken(getSessionTokenRequest).GetAwaiter().GetResult();

            Console.WriteLine("Received session token: " + response.SessionToken);
            Console.WriteLine("Reason: " + response.Reason);
        }
    }
}

```

## Sample applications configuration

***Safecharge.Sample*** is a simple console application, which uses the configuration from `Safecharge.Sample/bin/Debug/netcoreapp3.1/appsettings.json` to get the merchant information and requests data.

***Safecharge.WebSample***  is a sample web project as an example for referencing and using the Safecharge wrapper from a MVC application. Merchant configuration is in `Safecharge.WebSample/appsettings.json`.

***Safecharge.Test.Core*** is the unit tests project targeting .NET Core 3.1. Merchant configuration is in `Safecharge.Test.Core/bin/Debug/netcoreapp3.1/testhost.dll.config`.