using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using static CRUDFörnamnEfternamn.Connection;
namespace CRUDFörnamnEfternamn
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Connection conn = new Connection("SQLEXPRESS","Northwind2023_Joakim_Haglund_Malm");
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var customer = new Customer("SQLEXPRESS", "Northwind2023_Joakim_Haglund_Malm");
            while (true)
            {

                int choice = ShowMenu("Sup", new List<string>
                    {
                    "•AddCustomer",
                    "•DeleteCustomer",
                    "•UpdateEmployee",
                    "•ShowCountrySales",
                    "•ny order",
                    "•avsluta"
                    }
                );
               // Console.Clear();
                if ( choice == 0 )
                {
                    Dictionary<string, string> ColumNameValue = new Dictionary<string, string> 
                    {
                        {"CustomerID"   , "fucks"},
                        {"CompanyName"  , "this shits"},
                        { "ContactName" , null},
                        {"ContactTitle" , null},
                        {"Address"      , null},
                        {"City"         , "hell"},
                        {"Region"       , null},
                        {"PostalCode"   , null},
                        {"Country"      , null},
                        {"Phone"        , null},
                        {"Fax"          , null}
                    };

                    Console.WriteLine("•AddCustomer");
                    conn.SendCommand("SELECT COUNT(*) FROM Employees");
                    var cmdb = new CommandBuilder();
                
                    Console.WriteLine(cmdb.BuildCommand(CommandBuilder.Action.SELECT, "Customers", ColumNameValue));
                    string str = cmdb.BuildCommand(CommandBuilder.Action.INSERT, "Customers", ColumNameValue);
                    Console.WriteLine();
                }
                else if ( choice == 1 )
                {
                    Console.WriteLine("•DeleteCustomer");
                }
                else if( choice == 2 )
                {
                    Console.WriteLine("•UpdateEmployee");
                }
                else if (choice == 3)
                {
                    Console.WriteLine("•ShowCountrySales");
                }
                else if (choice == 4)
                {
                    Console.WriteLine("•ny order");
                }
                else
                {
                    break;
                }

            }
        }
        public static int ShowMenu(string prompt, IEnumerable<string> options)
        {
            if (options == null || !options.Any())
            {
                throw new ArgumentException("Cannot show a menu for an empty list of options.");
            }

            Console.WriteLine(prompt);

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            // Calculate the width of the widest option so we can make them all the same width later.
            int width = options.Max(option => option.Length);

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count(); i++)
            {
                // Start by highlighting the first option.
                if (i == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options.ElementAt(i);
                // Pad every option to make them the same width, so the highlight is equally wide everywhere.
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                // First restore the previously selected option so it's not highlighted anymore.
                Console.CursorTop = top + selected;
                string oldOption = options.ElementAt(selected);
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                // Then find the new selected option.
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count() - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                // Finally highlight the new selected option.
                Console.CursorTop = top + selected;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options.ElementAt(selected);
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                // Place the cursor one step above the new selected option so that we can scroll and also see the option above.
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            // Afterwards, place the cursor below the menu so we can see whatever comes next.
            Console.CursorTop = top + options.Count();

            // Show the cursor again and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }
    }


}
