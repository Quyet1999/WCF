using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccount" in both code and config file together.
    [ServiceContract]
    public interface IAccount
    {
        [OperationContract]
        bool AddAccount(string sessionID, string username, string password, int groupID);
        [OperationContract]
        bool EditPasswordAccount(string sessionID, int accountID, string newPassword);
        [OperationContract]
        bool EditPermissionAccount(string sessionID, int accountID, int newGroupID);
        [OperationContract]
        bool DeleteAccount(string sessionID, int accountID);
        [OperationContract]
        bool EditStatusAccount(string sessionID, int accountID, bool newStatus);
    }
}
