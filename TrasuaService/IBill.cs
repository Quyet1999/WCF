using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBiil" in both code and config file together.
    [ServiceContract]
    public interface IBill
    {
        [OperationContract]
        int addBill(string sessionID, int customerID, bool statusBill);
        [OperationContract]
        bool editBill(string sessionID, int billID, int customerID, bool statusBill);
        [OperationContract]
        bool addBillDetail(string sessionID, int billID, int itemID, int numberItem, bool typeItem);
        [OperationContract]
        bool editBillDetail(string sessionID, int billID, int itemID, int nNumberItem, bool typeItem);
    }
}
