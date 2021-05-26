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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Login" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Login.svc or Login.svc.cs at the Solution Explorer and start debugging.
    public class Login : ILogin
    {
        Models db = new Models();
        public bool Logout(string sessionID, int accountID)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(accountID.ToString()))
                return false;
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID).FirstOrDefault();
            if (sess == null)
                return false;
            sess.Status = false;
            sess.TimeEnd = DateTime.Now;
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

        public string _Login(string username, string password)
        {
            if (!CheckInput.CheckInputSqli(username) || !CheckInput.CheckInputSqli(password))
                return "ERROR";
            Model.Account acc = db.Accounts.Where(x => x.Username == username || x.Password == password).ToList().FirstOrDefault();
            if (acc == null)
                return "ERROR";
            List<string> listSess = db.Sessions.Select(x => x.SessionID).ToList();
            bool check = false;
            string newSess = "";
            while (!check)
            {
                newSess = SessionProcessing.RandomString(10);
                if (listSess.Where(x => x.Equals(newSess)).ToList().Count > 0)
                    continue;
                else
                    check = true;
            }
            Session sess = new Session();
            sess.SessionID = newSess;
            sess.AccountID = acc.ID;
            sess.Timecreate = DateTime.Now;
            sess.Status = true;
            try
            {
                db.Sessions.Add(sess);
                db.SaveChanges();
                return sess.SessionID;
            }
            catch
            {
                return "ERRPR";
            }
        }
    }
}
