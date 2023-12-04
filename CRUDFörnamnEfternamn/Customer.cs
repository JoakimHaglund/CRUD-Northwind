using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static CRUDFörnamnEfternamn.CommandBuilder;

namespace CRUDFörnamnEfternamn
{
    internal class Customer : Connection
    {
        public Dictionary<string, string> ColumNameValue = new Dictionary<string, string>();
        public string Table { get; set; }
        public Customer(string server, string database, string table) :base(server, database) 
        {
            Table = table;
            List<string> columNames = SendCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '{table}'");
            foreach (string columName in columNames)
            {
                ColumNameValue.Add(columName, null);
            }
            Console.WriteLine();
        }

        public void GetValuesFromUser()
        {
            List<string> keys = new List<string>(ColumNameValue.Keys);
            foreach (string key in keys)
            {
                Console.Write($"Please enter a {key}: ");
                string value = Console.ReadLine();
                if (value != null)
                {
                    ColumNameValue[key] = value;
                }
               
           
                
            }
        }
        public void AddCustomer(Customer customer)
        {
            SqlConnection dbCon = new SqlConnection(Conn);
            dbCon.Open();
            using (dbCon)
            {
                CommandBuilder cb = new CommandBuilder();
                SqlCommand cmd = new SqlCommand(cb.BuildCommand(CommandBuilder.Action.INSERT, Table, ColumNameValue), dbCon);
                foreach (KeyValuePair<string, string> item in ColumNameValue)
                {
                    if (item.Value != null)
                    {
                        string param = "@" + item.Key.Substring(0, 1).ToLower() + item.Key.Substring(1);
                        cmd.Parameters.AddWithValue(param, item.Value);
                    }
                }
                dbCon.Open();
                cmd.ExecuteNonQuery();
                dbCon.Close();

            }
        }

    }
}
