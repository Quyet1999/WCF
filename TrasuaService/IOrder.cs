using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrder" in both code and config file together.
    [ServiceContract]
    public interface IOrder
    {
        [OperationContract]
        int addOrder(string sessionID, string address, string phoneNumber);
        [OperationContract]
        bool acceptOrder(string sessionID, int orderID, bool accept);
        [OperationContract]
        bool CancelOrder(string sessionID, int orderID);
        [OperationContract]
        bool addOrderDetail(string sessionID, int orderID, int itemOrderID, int numberOrder
            , int price, bool typeItem);
        [OperationContract]
        bool editOrderDetail(string sessionID, int orderID, int itemOrderID, int numberOrder
            , int price);
    }
}
