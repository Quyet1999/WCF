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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Account" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Account.svc or Account.svc.cs at the Solution Explorer and start debugging.
    public class Account : IAccount
    {
        Models db = new Models();
        public bool AddAccount(string sessionID, string username, string password, int groupID)
        {
            if (!CheckInput.CheckInputSqli(username) || !CheckInput.CheckInputSqli(password) || !CheckInput.CheckInputSqli(groupID.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID)==-1)
                return false;
            
            Model.Account acc = new Model.Account();
            acc.Username = username;
            acc.Password = password;
            acc.GroupID = groupID;
            try
            {
                db.Accounts.Add(acc);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAccount(string sessionID, int accountID)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(accountID.ToString()))
                return false;
            int groupID = SessionProcessing.CheckSessionID(sessionID);
            if (groupID == -1)
                return false;
            
            Model.Account acc = db.Accounts.Where(x => x.ID == accountID).ToList().FirstOrDefault();
            if (acc == null)
                return false;
            if (acc.GroupID > groupID)
                return false;
            try
            {
                db.Accounts.Remove(acc);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditPasswordAccount(string sessionID, int accountID, string newPassword)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(accountID.ToString()) || !CheckInput.CheckInputSqli(newPassword))
                return false;
            int groupID = SessionProcessing.CheckSessionID(sessionID);
            if (groupID == -1)
                return false;
            
            if (groupID == 3)
            {
                Model.Account acc = db.Accounts.Where(x => x.ID == accountID).ToList().FirstOrDefault();
                if (acc == null)
                    return false;
                acc.Password = newPassword;
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
            else
            {
                Model.Account acc = db.Accounts.Where(x => x.ID == accountID).ToList().FirstOrDefault();
                if (acc == null)
                    return false;
                Model.Session sess = db.Sessions.Where(x => x.SessionID == sessionID).ToList().First();
                if (sess.AccountID != acc.ID)
                    return false;
                acc.Password = newPassword;
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

        public bool EditPermissionAccount(string sessionID, int accountID, int newGroupID)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(newGroupID.ToString()))
                return false;
            int groupID = SessionProcessing.CheckSessionID(sessionID);
            if (groupID != 3)
                return false;
            
            Model.Account acc = db.Accounts.Where(x => x.ID == accountID).ToList().FirstOrDefault();
            if (acc == null)
                return false;
            acc.GroupID = newGroupID;
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

        public bool EditStatusAccount(string sessionID, int accountID, bool newStatus)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(accountID.ToString()) || !CheckInput.CheckInputSqli(newStatus.ToString()))
                return false;
            int groupID = SessionProcessing.CheckSessionID(sessionID);
            if (groupID != 3)
                return false;
            
            Model.Account acc = db.Accounts.Where(x => x.ID == accountID).ToList().FirstOrDefault();
            if (acc == null)
                return false;
            acc.StatusAccount = newStatus;
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
