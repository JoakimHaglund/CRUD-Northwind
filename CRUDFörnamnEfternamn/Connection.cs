using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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
        
        public List<string> SendCommand(string query)
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
                    Console.WriteLine(String.Format("{0}", reader[0]));
                    output.Add(reader[0].ToString());
                }
                return output;
            }
        }
        public void AddCustomer(Customer customer)
        {
            SqlConnection dbCon = new SqlConnection(Conn);
            dbCon.Open();
            using (dbCon)
            {

              /*  SqlCommand cmd = new SqlCommand(
               "INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax)" +
               "VALUES(@customerID, @companyName, @contactName, @contactTitle, @address, @city, @region, @postalCode, @country, @phone, @fax)", dbCon);
                cmd.Parameters.AddWithValue("@customerID", customerID);
                cmd.Parameters.AddWithValue("@companyName", companyName);
                cmd.Parameters.AddWithValue("@contactName", contactName);
                cmd.Parameters.AddWithValue("@contactTitle", contactTitle);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@region", region);
                cmd.Parameters.AddWithValue("@postalCode", postalCode);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@fax", fax);
                dbCon.Open();
                cmd.ExecuteNonQuery();
                dbCon.Close();*/

            }
        }
        public void DeleteCustomer() { }
        public void UpdateEmployeeAdress() { }
        public void ShowSelectedCountrySales() { }
        public void AddOrder() { }

    }
}
