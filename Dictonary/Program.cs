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
            Dictionary<string, string> test = new Dictionary<string, string>();
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
    }
}
