using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDB.models
{
    public class Electronic : Office
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="officeName"></param>
        /// <param name="type"></param>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="purchasedDate"></param>
        /// <param name="price"></param>
        /// <param name="currency"></param>
        /// <param name="localPriceToday"></param>
        public Electronic(string officeName, string type, string brand, string model, string purchasedDate, int price, string currency, string localPriceToday) : base(officeName)
        {
            Type = type;
            Brand = brand;
            Model = model;
            PurchasedDate = purchasedDate;
            Price = price;
            Currency = currency;
            LocalPriceToday = localPriceToday;
            EndOfLife = CalculateEndOfLife(purchasedDate);
        }

        [Key]
        public int Id { get; set; }
        //Properties
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PurchasedDate { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
        public string LocalPriceToday { get; set; }
        public DateTime EndOfLife { get; set; }

        /// <summary>
        /// A method that calculates end of life for an Electronic
        /// </summary>
        /// <param name="purchasedDate"></param>
        /// <returns></returns>
        private DateTime CalculateEndOfLife(string purchasedDate)
        {
            if (DateTime.TryParseExact(purchasedDate, new[] { "MM/dd/yyyy", "MM-dd-yyyy" }, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime purchaseDate))
            {
                return purchaseDate.AddYears(3);
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}