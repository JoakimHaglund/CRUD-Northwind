using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CRUDFörnamnEfternamn
{
    internal class Connection
    {
        public string Conn { get; protected set; }

        public Connection(string server, string database)
        {
            //SQLEXPRESS
            Conn = $"Server=.\\{server}; Database={database}; Integrated Security=true; TrustServerCertificate=True";
        }

        public string SendCommand(string query)
        {
            SqlConnection dbCon = new SqlConnection(Conn);
            dbCon.Open();
            using (dbCon)
            {
                SqlCommand cmd = new SqlCommand(query, dbCon);
                string output = cmd.ExecuteScalar().ToString();
                return output;
            }
        }

        public List<string> RetriveListFormDB(string query)
        {
            SqlConnection dbCon = new SqlConnection(Conn);
            dbCon.Open();
            using (dbCon)
            {
                var output = new List<string>();
                SqlCommand cmd = new SqlCommand(query, dbCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    output.Add(reader[0].ToString());
                }
                return output;
            }
        }
        public Dictionary<string, string> RetriveDictonaryFormDB(string columA, string columB, string table)
        {
            string query = $"SELECT {columA}, {columB} FROM {table} ORDER BY {columA}";
            SqlConnection dbCon = new SqlConnection(Conn);
            dbCon.Open();
            using (dbCon)
            {
                var output = new Dictionary<string, string>();
                SqlCommand cmd = new SqlCommand(query, dbCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    output.Add(reader[0].ToString(), reader[1].ToString());
                }
                return output;
            }
        }

        public void DeleteCustomer()
        {
            Console.Write("Enter customer to remove: ");
            string customer = Console.ReadLine();

            string queryOne = $@" 
                 DELETE FROM [Order Details] FROM [Order Details] 
                 JOIN Orders ON [Order Details].OrderID = Orders.OrderID 
                 JOIN Customers ON Orders.CustomerID = Customers.CustomerID
                 WHERE Customers.CustomerID = '{customer}';";

            string queryTwo = $@"
                 DELETE FROM Orders FROM Orders
                 JOIN Customers ON Customers.CustomerID = Orders.CustomerID
                 WHERE Customers.CustomerID = '{customer}';";

            string queryThree = $@"DELETE FROM Customers WHERE CustomerID = '{customer}';";

            List<string> queryList = new List<string> { queryOne, queryTwo, queryThree };

            SendTransaction(queryList);
        }
        public void UpdateEmployeeAdress() 
        {
          string k = @"UPDATE Employee SET Adress, City, Region, PostalCode, Country = @adress, @city, @region, @postalCode, @country >
            WHERE @EmployeeName";
        }
        public void ShowSelectedCountrySales() { }
        public void AddOrder() { }

        public void SendTransaction(List<string> input) 
        {
            SqlConnection db = new SqlConnection(Conn);
            SqlTransaction transaction;

            db.Open();
            transaction = db.BeginTransaction();
            try
            {
                foreach (string item in input)
                {
                    new SqlCommand(item, db, transaction).ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (SqlException sqlError)
            {
                transaction.Rollback();
                Console.WriteLine(sqlError.Errors);
            }
            db.Close();
        }
        public List<string> GetColumNamesFromTable(string tableName)
        {
            return RetriveListFormDB($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '{tableName}'");
        }


    }
}
