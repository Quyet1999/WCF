using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILogin" in both code and config file together.
    [ServiceContract]
    public interface ILogin
    {
        [OperationContract]
        string _Login(string username, string password);
        [OperationContract]
        bool Logout(string sessionID, int accountID);
    }
}
