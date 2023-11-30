using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFörnamnEfternamn
{
    internal class Connection
    {
        public string Conn { get; set; }

        public Connection(string server, string database)
        {
            //SQLEXPRESS
            Conn = $"Server=.\\{server}; Database={database}; Integrated Security=true; TrustServerCertificate=True";
        }
        public void SendCommand()
        {
            SqlConnection dbCon = new SqlConnection(Conn);
            dbCon.Open();
            using (dbCon)
            {
                SqlCommand command = new SqlCommand(
                "SELECT COUNT(*) FROM Employees", dbCon);
                int employeesCount = (int)command.ExecuteScalar();
                Console.WriteLine(
                "Employees count: {0} ", employeesCount);
            }
        }
        public void AddCustomer()
        {

        }
        public void DeleteCustomer() { }
        public void UpdateEmployeeAdress() { }
        public void ShowSelectedCountrySales() { }
        public void AddOrder() { }

    }
}
