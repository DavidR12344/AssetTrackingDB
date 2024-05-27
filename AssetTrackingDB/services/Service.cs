using AssetTrackingDB.interfaces;
using AssetTrackingDB.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssetTrackingDB.services
{
    public class Order : IOrder
    {
        private List<Electronic> electronics;
        private readonly MyDBContext _dbContext;

        /// <summary>
        /// Constructor and initialize list
        /// </summary>
        public Order(MyDBContext dbContext)
        {
            electronics = new List<Electronic>();
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add a new electronic to list 
        /// </summary>
        /// <param name="officeName"></param>
        /// <param name="type"></param>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="purchasedDate"></param>
        /// <param name="price"></param>
        /// <param name="currency"></param>
        /// <param name="localPriceToday"></param>
        public void Create(string officeName, string type, string brand, string model, string purchasedDate, int price, string currency, string localPriceToday)
        {
            if (!IsValidCurrency(currency))
            {
                Console.WriteLine("Invalid currency. Please provide a valid currency (EUR, SEK, USD).");
                return;
            }

            localPriceToday = price.ToString();

            if (type.ToLower() == "computer")
            {
                Computer computer = new Computer(officeName, type, brand, model, purchasedDate, price, currency, localPriceToday);
                electronics.Add(computer);
                _dbContext.electronicsDB.Add(computer);
                _dbContext.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Computer added to your order");
                Console.ResetColor();
            }
            else if (type.ToLower() == "phone")
            {
                Mobile mobile = new Mobile(officeName, type, brand, model, purchasedDate, price, currency, localPriceToday);
                electronics.Add(mobile);
                _dbContext.electronicsDB.Add(mobile);
                _dbContext.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Phone added to your order");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Checks if the currency is valid
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>

        private bool IsValidCurrency(string currency)
        {
            return currency.ToUpper() == "EUR" || currency.ToUpper() == "SEK" || currency.ToUpper() == "USD";
        }

        /// <summary>
        /// Displays all electronic in list and Mark an electronic as red or yellow if end of life is near. 
        /// </summary>
        public void Read()
        {
            var electronics = _dbContext.electronicsDB.ToList();
            if (electronics.Count == 0)
            {
                Console.WriteLine("No Electronics in your order");
            }
            else
            {
                Console.WriteLine($"{"ID",-10}{"Type",-10}{"Brand",-15}{"Model",-15}{"Office",-15}{"Purchase Date",-15}{"Price (USD)",-12}{"Currency",-10}{"Local price today",10}");
                foreach (var electronic in electronics)
                {
                    DateTime endOfLife = electronic.EndOfLife;

                    // Check if the end of life is within 6 months
                    if (endOfLife <= DateTime.Now.AddMonths(6))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    // Check if the end of life is within 3 months
                    else if (endOfLife <= DateTime.Now.AddMonths(3))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    // Print the electronic item details
                    Console.WriteLine($"{electronic.Id,-10}{electronic.Type,-10}{electronic.Brand,-15}{electronic.Model,-15}{electronic.OfficeName,-15}{electronic.PurchasedDate,-15}{electronic.Price,-12}{electronic.Currency,-10}{electronic.LocalPriceToday,10}");

                    // Reset console foreground color to default
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Sort by office
        /// </summary>
        public void SortByOffice()
        {
            electronics.Sort((e1, e2) => e1.OfficeName.CompareTo(e2.OfficeName));
            Console.WriteLine("Electronics sorted by office.");
            Read();
        }

        /// <summary>
        /// Sort by computer first and then phones. If it is same type leave them as they ARE
        /// </summary>

        public void SortByPrimary()
        {
            electronics.Sort((e1, e2) =>
            {
                if (e1.Type.ToLower() == "computer" && e2.Type.ToLower() != "computer")
                {
                    return -1;
                }
                else if (e1.Type.ToLower() != "computer" && e2.Type.ToLower() == "computer")
                {
                    return 1;
                }
                else
                {
                    return 0; // e1 and e2 are either both computers or both phones, their order remains unchanged
                }
            });

            Console.WriteLine("Electronics sorted by primary.");
            Read();
        }

        //Sort by purchased date
        public void SortByPurchasedDate()
        {
            electronics.Sort((e1, e2) =>
            {
                DateTime purchaseDate1, purchaseDate2;
                bool parse1 = DateTime.TryParseExact(e1.PurchasedDate, new[] { "MM/dd/yyyy", "MM-dd-yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out purchaseDate1);
                bool parse2 = DateTime.TryParseExact(e2.PurchasedDate, new[] { "MM/dd/yyyy", "MM-dd-yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out purchaseDate2);

                if (parse1 && parse2)
                {
                    return purchaseDate1.CompareTo(purchaseDate2);
                }
                return 0;
            });

            Console.WriteLine("Electronics sorted by purchased date.");
            Read();
        }
        public void Update()
        {
            Console.Write("Enter order ID to update: ");
            var id = int.Parse(Console.ReadLine());
            var electronic = _dbContext.electronicsDB.Find(id);

            if (electronic != null)
            {
                while (true)
                {
                    Console.Write("Office Name: ");
                    electronic.OfficeName = Console.ReadLine();

                    if (string.IsNullOrEmpty(electronic.OfficeName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Office cannot be empty. Please enter a valid office.");
                        Console.ResetColor();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.Write("Type (computer/phone): ");
                    electronic.Type = Console.ReadLine();
                    if (string.IsNullOrEmpty(electronic.Type))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Type cannot be empty. Please enter a valid type.");
                        Console.ResetColor();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    Console.Write("Brand: ");
                    electronic.Brand = Console.ReadLine();
                    if (string.IsNullOrEmpty(electronic.Brand))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Brand cannot be empty. Please enter a valid brand.");
                        Console.ResetColor();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    Console.Write("Model: ");
                    electronic.Model = Console.ReadLine();
                    if (string.IsNullOrEmpty(electronic.Model))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Model cannot be empty. Please enter a valid model.");
                        Console.ResetColor();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    Console.Write("Purchase Date (MM/dd/yyyy): ");
                    electronic.PurchasedDate = Console.ReadLine();
                    if (string.IsNullOrEmpty(electronic.PurchasedDate))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Date cannot be empty. Please enter a valid date.");
                        Console.ResetColor();
                        continue;
                    }


                    else
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.Write("Price: ");
                    electronic.Price = int.Parse(Console.ReadLine());
                    if (string.IsNullOrEmpty(electronic.Price.ToString()))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Price must be a valid number. Please enter a valid price.");
                        Console.ResetColor();
                        continue;
                    }

                    else
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.Write("Currency: ");
                    var currency = Console.ReadLine();
                    if (IsValidCurrency(currency))
                    {
                        electronic.Currency = currency;
                    }
                    else
                    {
                        Console.WriteLine("Invalid currency. Please provide a valid currency (EUR, SEK, USD).");
                        break;
                    }
                }

                electronic.LocalPriceToday = electronic.Price.ToString();
                _dbContext.SaveChanges();
                Console.WriteLine("Electronic updated.");
            }
            else
            {
                Console.WriteLine("Electronic not found.");
            }
        }

        /// <summary>
        /// A method to delete an electronic by id
        /// </summary>

        public void Delete()
        {
            Console.Write("Enter order ID to delete: ");
            var id = int.Parse(Console.ReadLine());
            var electronic = _dbContext.electronicsDB.Find(id);

            if (electronic != null)
            {
                _dbContext.electronicsDB.Remove(electronic);
                _dbContext.SaveChanges();
                Console.WriteLine("Electronic deleted.");
            }
            else
            {
                Console.WriteLine("Electronic not found.");
            }

        }
    }
}
