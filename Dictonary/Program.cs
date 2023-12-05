using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dictonary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*  Dictionary<string, string> test = new Dictionary<string, string>();
              test.Add("@hello", "World");
              test.Add("hellWorld", "Worl");
              test.Add("hel", "Wor");
              test.Add("he", "Wo");
              foreach (var item in test) 
              { 
                  Console.WriteLine(item.Key +" "+item.Value);
                  //Console.WriteLine(item.Key.);
              }
              Console.WriteLine(useRegex("DeleteCustomer"));
              Console.WriteLine();
              Console.WriteLine(GetColums(test));
            string str = "HelloWorld";
            str = "@" + str.Substring(0, 1).ToLower() + str.Substring(1);
            Console.WriteLine(  str);*/
            var str = new List<string>
                    {
                    "•AddCustomer",
                    "•DeleteCustomer",
                    "•UpdateEmployee",
                    "•ShowCountrySales",
                    "•ny order",
                    "•avsluta"
                    };
            int choice = ShowMenu("Sup", str);
            Console.WriteLine(str[choice]);

        }
        private static string GetColums(Dictionary<string, string> columNameValue)
        {
            string output = string.Empty;
            foreach (KeyValuePair<string, string> item in columNameValue)
            {
                output += item.Key + ", ";
            }
            output = output.Trim();
            output = output.Remove(output.Length - 1, 1);
            return output;
        }
        public static bool useRegex(String input)
        {
            Regex regex = new Regex("[A-Z]", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
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
