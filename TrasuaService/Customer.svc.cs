using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TrasuaService.Model;
using TrasuaService.Process;

namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Customer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Customer.svc or Customer.svc.cs at the Solution Explorer and start debugging.
    public class Customer : ICustomer
    {
        Models db = new Models();
        public bool acceptCustomer(string sessionID, int CustomerID)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(CustomerID.ToString()))
                return false;
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID && x.Status == true).FirstOrDefault();
            if (sess == null)
                return false;
            Model.Account acc = db.Accounts.Where(x => x.ID == sess.AccountID).First();
            if (acc.GroupID < 2)
                return false;
            Model.Customer cus = db.Customers.Where(x => x.ID == CustomerID).FirstOrDefault();
            if (cus == null)
                return false;
            cus.EmployeeAccept = acc.ID;
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

        public int addCustomer(string fullname, bool sex, string birthday, string address)
        {
            if (!CheckInput.CheckInputSqli(fullname) || !CheckInput.CheckInputSqli(sex.ToString()) || !CheckInput.CheckInputSqli(birthday)
                || !CheckInput.CheckInputSqli(address))
                return -1;
            Model.Customer cus = new Model.Customer();
            cus.FullName = fullname;
            cus.Sex = sex;
            cus.Birthday = Convert.ToDateTime(birthday);
            cus.Address = address;
            cus.AccountID = -1;
            cus.EmployeeAccept = -1;
            try
            {
                db.Customers.Add(cus);
                db.SaveChanges();
                return cus.ID;
            }
            catch 
            {
                return -1;
            }
        }

        public bool editCustomer(string sessionID, string fullname, bool sex, string birthday, string address)
        {
            if (!CheckInput.CheckInputSqli(fullname) || !CheckInput.CheckInputSqli(sex.ToString()) || !CheckInput.CheckInputSqli(birthday)
                || !CheckInput.CheckInputSqli(address))
                return false;
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID && x.Status == true).FirstOrDefault();
            if (sess == null)
                return false;
            Model.Account acc = db.Accounts.Where(x => x.ID == sess.AccountID).First();
            Model.Customer cus = db.Customers.Where(x => x.AccountID == acc.ID).First();
            cus.FullName = fullname;
            cus.Sex = sex;
            cus.Birthday = Convert.ToDateTime(birthday);
            cus.Address = address;
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
        public bool addAccountToCustomer(int customerID, int accountID)
        {
            if (!CheckInput.CheckInputSqli(customerID.ToString()) || !CheckInput.CheckInputSqli(accountID.ToString()))
                return false;
            Model.Customer cus = db.Customers.Where(x => x.ID == customerID).FirstOrDefault();
            Model.Account acc = db.Accounts.Where(x => x.ID == accountID).FirstOrDefault();
            if (cus == null || acc == null || cus.AccountID != -1) 
                return false;
            cus.AccountID = accountID;
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
    }
}
