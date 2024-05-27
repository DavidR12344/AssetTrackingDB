using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDB.models
{
    public class Computer : Electronic
    {

        /// <summary>
        /// Contstructor
        /// </summary>
        /// <param name="officeName"></param>
        /// <param name="type"></param>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="purchasedDate"></param>
        /// <param name="price"></param>
        /// <param name="currency"></param>
        /// <param name="localPriceToday"></param>
        public Computer(string officeName, string type, string brand, string model, string purchasedDate, int price, string currency, string localPriceToday) : base(officeName, type, brand, model, purchasedDate, price, currency, localPriceToday)
        {
        }
    }
}
