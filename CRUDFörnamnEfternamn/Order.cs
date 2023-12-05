using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDFörnamnEfternamn
{
    internal class Order: Connection
    {
        struct OrderDetails
        {
            public int OrderId;
            public int ProductId;
            public short Quantity;
            /*public decimal UnitPrice;
              public Single Discount;*/
        }
        public Order(string server, string database) :base(server, database)
        {

        }
        public void CreateNewOrder(string cutomerId)
        {
            /*
            -menu addorder "choices add product / remove Product / checkout"
            -add product "show products" -> select quantitiy ->return to order menu
            -remove product "show products" -> delete seleceted from list
            -checkout -> send to server -> exit to main menu 
            *perhaps also give succes/error msg*
             */
            string createOrderQuery = $"INSERT INTO Orders(CustomerID) VALUES('{cutomerId}')";
            string retriveOrderID = $"SELECT MAX(OrderID) FROM Orders WHERE Orders.CustomerID = '{cutomerId}'";
            SendCommand(createOrderQuery);
            string orderId = SendCommand(retriveOrderID);
            List<OrderDetails> orderDetails = new List<OrderDetails>();

            Dictionary<string, string> test = this.RetriveDictonaryFormDB("ProductID", "ProductName", "Products");
            int productId = Program.ShowMenu("Choose product: ", test.Values.ToList<string>());
            Console.Write("Quantity: ");
            short quantity = short.Parse(Console.ReadLine());

            var productOrder = new OrderDetails();
            productOrder.OrderId = int.Parse(orderId);
            productOrder.ProductId = productId;
            productOrder.Quantity = quantity;
            orderDetails.Add(productOrder);

        }
    }
}
