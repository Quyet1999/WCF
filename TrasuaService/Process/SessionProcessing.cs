using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TrasuaService.Model;

namespace TrasuaService.Process
{
    public static class SessionProcessing
    {
        public static int CheckSessionID(string sessionID)
        {
            if (!CheckInput.CheckInputSqli(sessionID))
                return -1;
            Models db = new Models();
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID).ToList().FirstOrDefault();
            if (sess == null)
                return -1;
            if (sess.Status == false)
                return -1;
            Model.Account acc = db.Accounts.Where(y => y.ID == sess.AccountID).ToList().FirstOrDefault();
            return acc.GroupID;
        }
        public static string RandomString(int size)
        {
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}