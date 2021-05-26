using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployee" in both code and config file together.
    [ServiceContract]
    public interface IEmployee
    {
        [OperationContract]
        int addEmployee(string sessionID, string fullName, bool sex, string birthday, string address);
        [OperationContract]
        bool editEmployee(string sessionID, int employeeID, string fullename, bool sex, string birthday, string address);
        [OperationContract]
        bool addAccountToEmployee(string sessionID, int employeeID, int accountID);
        [OperationContract]
        bool deleteEmployee(string sessionID, int employeeID);
    }
}
