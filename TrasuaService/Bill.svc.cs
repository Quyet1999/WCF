using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TrasuaService.Process;
using TrasuaService.Model;
namespace TrasuaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Biil" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Biil.svc or Biil.svc.cs at the Solution Explorer and start debugging.
    public class Biil : IBill
    {
        Models db = new Models();
        public int addBill(string sessionID, int customerID, bool statusBill)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(customerID.ToString()) || !CheckInput.CheckInputSqli(statusBill.ToString()))
                return -1;
            Session sess = db.Sessions.Where(x => x.SessionID == sessionID).FirstOrDefault();
            if (sess == null)
                return -1;
            Model.Account acc = db.Accounts.Where(x => x.ID == sess.AccountID).FirstOrDefault();
            if (acc.GroupID != 2)
                return -1;
            Bill bill = new Bill();
            if (customerID > 0)
            {
                Model.Customer cus = db.Customers.Where(x => x.ID == customerID).ToList().FirstOrDefault();
                if (cus == null)
                    return -1;
                bill.CustomerID = customerID;
            }
            else
            {
                bill.CustomerID = -1;
            }
            bill.StatusBill = statusBill;
            bill.EmployeeCreate = acc.ID;
            bill.TimeCreate = DateTime.Now;
            try
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return bill.ID;
            }
            catch
            {
                return -1;
            }
        }

        public bool addBillDetail(string sessionID, int billID, int itemID, int numberItem, bool typeItem)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(billID.ToString()) || !CheckInput.CheckInputSqli(itemID.ToString())
                || !CheckInput.CheckInputSqli(numberItem.ToString()) || !CheckInput.CheckInputSqli(typeItem.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 2)
                return false;
            Bill bill = db.Bills.Where(x => x.ID == billID).FirstOrDefault();
            if (bill == null)
                return false;
            BillDetail billDetail = db.BillDetails.Where(x => x.BillID == billID && x.ItemID == itemID && x.TypeItem == typeItem).FirstOrDefault();
            if (billDetail != null)
                return false;
            if (typeItem == true)
            {
                Model.Drink dr = db.Drinks.Where(x => x.ID == itemID).ToList().FirstOrDefault();
                BillDetail newDetail = new BillDetail();
                newDetail.BillID = bill.ID;
                newDetail.ItemID = itemID;
                newDetail.NumberItem = numberItem;
                newDetail.Price = dr.Price;
                newDetail.TotalMoneyDetail = numberItem * dr.Price;
                newDetail.TypeItem = typeItem;
                bill.TotalMoney += newDetail.TotalMoneyDetail;
                try
                {
                    db.BillDetails.Add(newDetail);
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
                Model.Topping topp = db.Toppings.Where(x => x.ID == itemID).ToList().FirstOrDefault();
                BillDetail newDetail = new BillDetail();
                newDetail.BillID = bill.ID;
                newDetail.ItemID = itemID;
                newDetail.NumberItem = numberItem;
                newDetail.Price = topp.Price;
                newDetail.TotalMoneyDetail = numberItem * topp.Price;
                newDetail.TypeItem = typeItem;
                bill.TotalMoney += newDetail.TotalMoneyDetail;
                try
                {
                    db.BillDetails.Add(newDetail);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        public bool editBill(string sessionID, int billID, int customerID, bool statusBill)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(billID.ToString()) || !CheckInput.CheckInputSqli(customerID.ToString())
               || !CheckInput.CheckInputSqli(statusBill.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 2)
                return false;
            Bill bill = db.Bills.Where(x => x.ID == billID).FirstOrDefault();
            if (bill == null)
                return false;
            if (customerID > 0)
            {
                Model.Customer cus = db.Customers.Where(x => x.ID == customerID).ToList().FirstOrDefault();
                if (cus == null)
                    return false;
                bill.CustomerID = customerID;
            }
            else
            {
                bill.CustomerID = -1;
            }
            bill.StatusBill = statusBill;
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

        public bool editBillDetail(string sessionID, int billID, int itemID, int numberItem, bool typeItem)
        {
            if (!CheckInput.CheckInputSqli(sessionID) || !CheckInput.CheckInputSqli(billID.ToString()) || !CheckInput.CheckInputSqli(itemID.ToString())
               || !CheckInput.CheckInputSqli(numberItem.ToString()) || !CheckInput.CheckInputSqli(typeItem.ToString()))
                return false;
            if (SessionProcessing.CheckSessionID(sessionID) != 2)
                return false;
            Bill bill = db.Bills.Where(x => x.ID == billID).FirstOrDefault();
            if (bill == null)
                return false;
            BillDetail billDetail = db.BillDetails.Where(x => x.BillID == billID && x.ItemID == itemID && x.TypeItem == typeItem).FirstOrDefault();
            if (billDetail == null)
                return false;
            if (numberItem == 0)
            {
                try
                {
                    db.BillDetails.Remove(billDetail);
                    db.SaveChanges();
                    updateTotalMoney(bill.ID);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                billDetail.NumberItem = numberItem;
                billDetail.TotalMoneyDetail = billDetail.Price * numberItem;
                updateTotalMoney(bill.ID);
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
        private void updateTotalMoney(int billID)
        {
            Bill bill = db.Bills.Where(x => x.ID == billID).First();
            List<BillDetail> listDetail = db.BillDetails.Where(x => x.BillID == billID).ToList();
            bill.TotalMoney = 0;
            foreach (BillDetail item in listDetail)
            {
                bill.TotalMoney += item.TotalMoneyDetail;
            }
            db.SaveChanges();
        }
    }
}
