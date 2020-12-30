using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Safecharge.Model.PaymentOptionModels;
using Safecharge.Model.PaymentOptionModels.CardModels;
using Safecharge.WebSample.Models;

namespace Safecharge.WebSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISafecharge _safecharge;

        public HomeController(ISafecharge safecharge)
        {
            _safecharge = safecharge;
        }

        public IActionResult Index()
        {
            return View(new PaymentEditModel());
        }

        public IActionResult Payment(PaymentEditModel model)
        {
            var paymentResponse = this._safecharge.Payment(
                model.Currency,
                model.Amount,
                new PaymentOption
                {
                    Card = new Card
                    {
                        CardNumber = "4000023104662535",
                        CardHolderName = "John Smith",
                        ExpirationMonth = "12",
                        ExpirationYear = "22",
                        CVV = "217"
                    }
                }).GetAwaiter().GetResult();

            return View(new PaymentViewModel { PaymentResponse = paymentResponse });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
