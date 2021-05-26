using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDrink" in both code and config file together.
    [ServiceContract]
    public interface IDrink
    {
        [OperationContract]
        DataTable getAllDrink();
        [OperationContract]
        DataTable getDrinkByType(int type);
        [OperationContract]
        int addNewDrink(string sessionID, string name, string description, int typeDrink, bool status
            , int price);
        [OperationContract]
        int addNewTypeDrink(string sessionID, string name, string description);
        [OperationContract]
        bool editDrink(string sessionID, int drinkID, string name, string description, int typeDrink, bool status
            , int price);
        [OperationContract]
        bool editTypeDrink(string sessionID, int typeDrinkID, string name, string description);
        [OperationContract]
        bool deleteDrink(string sessionID, int drinkID);
        [OperationContract]
        bool deleteTypeDrink(string sessionID, int typeDrinkID);
    }
}
