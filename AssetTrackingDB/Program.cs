using AssetTrackingDB.interfaces;
using AssetTrackingDB.services;
using System;
using System.ComponentModel.Design;

namespace AssetTrackingDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MyDBContext())
            {
                db.Database.EnsureCreated();
                Console.WriteLine("Database created.");
                IOrder orderList = new Order(db);
                bool exit = false;

                while (!exit)
                {
                    Console.WriteLine(">> Welcome to your new local office");
                    Console.WriteLine(">> You have to get new electronics today!");
                    Console.WriteLine(">> Pick an option: ");
                    Console.WriteLine(">> (1) Add new electronics");
                    Console.WriteLine(">> (2) View all electronics");
                    Console.WriteLine(">> (3) Update an electronic");
                    Console.WriteLine(">> (4) Delete an electronic");
                    Console.WriteLine(">> (5) Level 2 (sorted by primary and sorted by purchased date)");
                    Console.WriteLine(">> (6) Level 3 (sorted by office and then by purchased date)");
                    Console.WriteLine(">> (7) Exit");
                    Console.WriteLine("Please enter a choice: ");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            string name;
                            string type;
                            string brand;
                            string model;
                            string date;
                            string priceStr;
                            string currency;
                            string localPriceToday;
                            int price;

                            while (true)
                            {

                                Console.WriteLine("Enter office: ");
                                name = Console.ReadLine();
                                if (string.IsNullOrEmpty(name))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Office cannot be empty. Please enter a valid office.");
                                    Console.ResetColor();
                                    continue;
                                }


                                while (true)
                                {
                                    Console.WriteLine("Enter type of the electronic (Computer/Phone): ");
                                    type = Console.ReadLine();
                                    if (string.IsNullOrEmpty(type))
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
                                    Console.WriteLine("Enter brand of the electronic: ");
                                    brand = Console.ReadLine();
                                    if (string.IsNullOrEmpty(brand))
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
                                    Console.WriteLine("Enter model of the electronic: ");
                                    model = Console.ReadLine();
                                    if (string.IsNullOrEmpty(model))
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
                                    Console.WriteLine("Enter date of purchase (MM/dd/yyyy): ");
                                    date = Console.ReadLine();
                                    if (string.IsNullOrEmpty(date))
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
                                    Console.WriteLine("Enter price of the electronic: ");
                                    priceStr = Console.ReadLine();
                                    if (!int.TryParse(priceStr, out price))
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
                                    Console.WriteLine("Enter currency of the electronic (EUR, SEK, USD): ");
                                    currency = Console.ReadLine().ToUpper();
                                    if (string.IsNullOrEmpty(currency) || (currency != "EUR" && currency != "SEK" && currency != "USD"))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid currency. Please enter a valid currency (EUR, SEK, USD).");
                                        Console.ResetColor();
                                        continue;
                                    }

                                    else
                                    {
                                        break;
                                    }
                                }

                                localPriceToday = price.ToString();
                                orderList.Create(name, type, brand, model, date, price, currency, localPriceToday);
                                break;
                            }
                            break;
                        case "2":
                            orderList.Read();
                            break;
                        case "3":
                            orderList.Update();
                            break;
                        case "4":
                            orderList.Delete();
                            break;
                        case "5":
                            orderList.SortByPrimary();
                            orderList.SortByPurchasedDate();
                            break;
                        case "6":
                            orderList.SortByOffice();
                            orderList.SortByPurchasedDate();
                            break;
                        case "7":
                            exit = true;
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again");
                            break;
                    }
                }
            }
        }
    }
}
