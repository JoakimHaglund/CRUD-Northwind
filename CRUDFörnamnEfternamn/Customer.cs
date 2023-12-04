using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFörnamnEfternamn
{
    internal class Customer : Connection
    {
        public Dictionary<string, string> ColumNameValue = new Dictionary<string, string>();
       
        public Customer(string server, string database) :base(server, database) 
        {
            List<string> columNames = SendCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'Employees'");
            foreach (string columName in columNames)
            {
                ColumNameValue.Add(columName, null);
            }
            Console.WriteLine();
        }

        public void GetValuesFromUser()
        {
            foreach(KeyValuePair<string, string> colum in ColumNameValue)
            {
                Console.Write($"Please enter a {colum.Key}: ");
                string value = Console.ReadLine();
                if (value != null)
                {
                    ColumNameValue[colum.Key] = value;
                }
            }
        } 

    }
}
