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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Employee" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Employee.svc or Employee.svc.cs at the Solution Explorer and start debugging.
    public class Employee : IEmployee
    {
        Models db = new Models();
        public bool addAccountToEmployee(string sessionID, int employeeID, int accountID)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(employeeID.ToString()) || !CheckInput.CheckInputSqli(accountID.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return false;
            Model.Employee emp = db.Employees.Where(x => x.ID == employeeID).FirstOrDefault();
            Model.Account acc = db.Accounts.Where(x => x.ID == accountID).FirstOrDefault();
            if (emp == null || acc == null || emp.AccountID == -1)
            {
                return false;
            }
            emp.AccountID = accountID;
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

        public int addEmployee(string sessionID, string fullName, bool sex, string birthday, string address)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(fullName) || !CheckInput.CheckInputSqli(sex.ToString())
                || !CheckInput.CheckInputSqli(birthday) || !CheckInput.CheckInputSqli(address))
                return -1;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return -1;
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID).First();

            Model.Employee emp = new Model.Employee();
            if (db.Employees.ToList().Count == 0)
                emp.ID = 1;
            else
                emp.ID = db.Employees.OrderByDescending(x => x.ID).First().ID + 1;
            emp.FullName = fullName;
            emp.Sex = sex;
            DateTime dt = new DateTime();
            if (!DateTime.TryParse(birthday, out dt))
            {
                return -1;
            }
            emp.Birthday = dt;
            emp.Address = address;
            emp.AccountID = -1;
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return emp.ID;
            }
            catch
            {
                return -1;
            }
        }

        public bool deleteEmployee(string sessionID, int employeeID)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(employeeID.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return false;
            Model.Employee emp = db.Employees.Where(x => x.ID == employeeID).FirstOrDefault();
            if (emp == null)
                return false;
            if (emp.AccountID != -1)
            {
                Model.Account acc = db.Accounts.Where(x => x.ID == emp.AccountID).FirstOrDefault();
                if (acc != null)
                {
                    try
                    {
                        db.Accounts.Remove(acc);
                        db.SaveChanges();
                    }
                    catch
                    {

                    }
                }
            }
            try
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool editEmployee(string sessionID, int employeeID, string fullname, bool sex, string birthday, string address)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(employeeID.ToString()) || !CheckInput.CheckInputSqli(fullname) || !CheckInput.CheckInputSqli(sex.ToString())
                || !CheckInput.CheckInputSqli(birthday) || !CheckInput.CheckInputSqli(address))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 3)
                return false;
            Model.Employee emp = db.Employees.Where(x => x.ID == employeeID).FirstOrDefault();
            if (emp == null)
                return false;
            emp.FullName = fullname;
            emp.Sex = sex;
            DateTime dt = new DateTime();
            if (!DateTime.TryParse(birthday, out dt))
            {
                return false;
            }
            emp.Birthday = dt;
            emp.Address = address;
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
