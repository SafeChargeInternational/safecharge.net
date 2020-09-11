using System.ComponentModel.DataAnnotations;

namespace Safecharge.WebSample.Models
{
    public class PaymentEditModel
    {
        [Display(Name = nameof(Currency))]
        public string Currency { get; set; }

        [Display(Name = nameof(Amount))]
        public string Amount { get; set; }
    }
}
