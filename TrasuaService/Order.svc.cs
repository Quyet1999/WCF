using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Order" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Order.svc or Order.svc.cs at the Solution Explorer and start debugging.
    public class Order : IOrder
    {
        public bool acceptOrder(string sessionID, int orderID, bool accept)
        {
            throw new NotImplementedException();
        }

        public int addOrder(string sessionID, string address, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public bool addOrderDetail(string sessionID, int orderID, int itemOrderID, int numberOrder, int price, bool typeItem)
        {
            throw new NotImplementedException();
        }

        public bool CancelOrder(string sessionID, int orderID)
        {
            throw new NotImplementedException();
        }

        public bool editOrderDetail(string sessionID, int orderID, int itemOrderID, int numberOrder, int price)
        {
            throw new NotImplementedException();
        }
    }
}
