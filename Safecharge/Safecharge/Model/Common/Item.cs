using Safecharge.Utils;

namespace Safecharge.Model.Common
{
    /// <summary>
    /// Holder for merchant's item info.
    /// </summary>
    public class Item
    {
        private string name;
        private string price;
        private string quantity;

        public string Name
        {
            get { return this.name; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.name));
                Guard.RequiresLengthBetween(value.Length, Constants.MinLengthStringDefault, Constants.MaxLengthStringDefault, nameof(this.Name));
                this.name = value;
            }
        }

        public string Price
        {
            get { return this.price; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.price));
                Guard.RequiresMaxLength(value.Length, 10, nameof(this.Price));
                this.price = value;
            }
        }

        public string Quantity
        {
            get { return this.quantity; }
            set
            {
                Guard.RequiresNotNull(value, nameof(this.quantity));
                Guard.RequiresMaxLength(value.Length, 10, nameof(this.Quantity));
                this.quantity = value;
            }
        }
    }
}
