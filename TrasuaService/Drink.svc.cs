using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TrasuaService.Model;
using TrasuaService.Process;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Drink" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Drink.svc or Drink.svc.cs at the Solution Explorer and start debugging.
    public class Drink : IDrink
    {
        Models db = new Models();
        public int addNewDrink(string sessionID, string name, string description, int typeDrink, bool status, int price)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(name) || !CheckInput.CheckInputSqli(description)
                || !CheckInput.CheckInputSqli(typeDrink.ToString()) || !CheckInput.CheckInputSqli(status.ToString()) || !CheckInput.CheckInputSqli(price.ToString())) 
                return -1;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return -1;
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID && x.Status == true).FirstOrDefault();

            Model.Drink dr = new Model.Drink();
            if (db.Drinks.ToList().Count == 0)
                dr.ID = 1;
            else
                dr.ID = db.Drinks.OrderByDescending(x => x.ID).First().ID;
            dr.Name = name;
            dr.Description = description;
            dr.Type = typeDrink;
            dr.Status = status;
            dr.Price = price;
            dr.TimeCreate = DateTime.Now;
            dr.AccountCreate = sess.AccountID;
            try
            {
                db.Drinks.Add(dr);
                db.SaveChanges();
                return dr.ID;
            }
            catch
            {
                return -1;
            }
        }

        public int addNewTypeDrink(string sessionID, string name, string description)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(name) || !CheckInput.CheckInputSqli(description))
                return -1;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return -1;
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID && x.Status == true).FirstOrDefault();

            TypeDrink type = new TypeDrink();
            if (db.TypeDrinks.ToList().Count == 0)
                type.ID = 1;
            else
                type.ID = db.TypeDrinks.OrderByDescending(x => x.ID).First().ID + 1;
            type.Name = name;
            type.Description = description;
            type.AccountCreate = sess.AccountID;
            type.TimeCreate = DateTime.Now;
            try
            {
                db.TypeDrinks.Add(type);
                db.SaveChanges();
                return type.ID;
            }
            catch
            {
                return -1;
            }
        }

        public bool deleteDrink(string sessionID, int drinkID)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(drinkID.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return false;
            Model.Drink dr = db.Drinks.Where(x => x.ID == drinkID).FirstOrDefault();
            if (dr == null)
                return false;
            try
            {
                db.Drinks.Remove(dr);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteTypeDrink(string sessionID, int typeDrinkID)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(typeDrinkID.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return false;
            TypeDrink type = db.TypeDrinks.Where(x => x.ID == typeDrinkID).FirstOrDefault();
            if (type == null)
                return false;
            try
            {
                db.TypeDrinks.Remove(type);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool editDrink(string sessionID, int drinkID, string name, string description, int typeDrink, bool status, int price)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(name) || !CheckInput.CheckInputSqli(description) ||
                !CheckInput.CheckInputSqli(typeDrink.ToString()) || !CheckInput.CheckInputSqli(status.ToString()) || !CheckInput.CheckInputSqli(price.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return false;
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID).First();
            Model.Drink dr = db.Drinks.Where(x => x.ID == drinkID).FirstOrDefault();
            if (dr == null)
                return false;
            dr.Name = name;
            dr.Description = description;
            dr.Type = typeDrink;
            dr.Status = status;
            dr.Price = price;
            dr.AccountChange = sess.AccountID;
            dr.TimeChange = DateTime.Now;
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool editTypeDrink(string sessionID, int typeDrinkID, string name, string description)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(name) || !CheckInput.CheckInputSqli(description) ||
                !CheckInput.CheckInputSqli(typeDrinkID.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return false;
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID).First();
            TypeDrink type = db.TypeDrinks.Where(x => x.ID == typeDrinkID).FirstOrDefault();
            if (type == null)
                return false;
            type.Name = name;
            type.Description = description;
            type.AccountChange = sess.AccountID;
            type.TimeChange = DateTime.Now;
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable getAllDrink()
        {
            DataTable result = new DataTable();
            result.Columns.Add("ID");
            result.Columns.Add("NAME");
            result.Columns.Add("DESCRIPTION");
            result.Columns.Add("TYPE");
            result.Columns.Add("STATUS");
            result.Columns.Add("PRICE");
            List<Model.Drink> list = db.Drinks.ToList();
            if (list == null)
                return result;
            foreach(Model.Drink item in list)
            {
                result.Rows.Add(item.ID, item.Name, item.Description, item.TypeDrink.Name, item.Status, item.Price);
            }
            return result;
        }

        public DataTable getDrinkByType(int type)
        {
            DataTable result = new DataTable();
            result.Columns.Add("ID");
            result.Columns.Add("NAME");
            result.Columns.Add("DESCRIPTION");
            result.Columns.Add("TYPE");
            result.Columns.Add("STATUS");
            result.Columns.Add("PRICE");
            if (!CheckInput.CheckInputSqli(type.ToString()))
                return result;
            List<Model.Drink> list = db.Drinks.Where(x=>x.Type==type).ToList();
            if (list == null)
                return result;
            foreach (Model.Drink item in list)
            {
                result.Rows.Add(item.ID, item.Name, item.Description, item.TypeDrink.Name, item.Status, item.Price);
            }
            return result;
        }
    }
}
