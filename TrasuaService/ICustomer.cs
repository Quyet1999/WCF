using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomer" in both code and config file together.
    [ServiceContract]
    public interface ICustomer
    {
        [OperationContract]
        int addCustomer(string fullname, bool sex, string birthday, string address);
        [OperationContract]
        bool editCustomer(string sessionID, string fullname, bool sex, string birthday, string address);
        [OperationContract]
        bool acceptCustomer(string sessionID, int CustomerID);
        [OperationContract]
        bool addAccountToCustomer(int CustomerID, int AccountID);
        
    }
}
