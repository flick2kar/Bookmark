using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entities;

namespace DALLayer
{
    public class TransRepository
    {
        public DataTable GetTransDetails(string strTransSearch="")
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "";
                if (String.IsNullOrEmpty(strTransSearch))
                    strQuery = "select top 10 BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans inner join (select TransID as transid from booktrans where booktrans.BookID like '%" + strTransSearch + "%' union select TransID as transid from booktrans where booktrans.MemberID like '%" + strTransSearch + "%') as temp on booktrans.TransID =  temp.TransID order by booktrans.TransID desc";
                else
                    strQuery = "select BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans inner join (select TransID as transid from booktrans where booktrans.BookID like '%" + strTransSearch + "%' union select TransID as transid from booktrans where booktrans.MemberID like '%" + strTransSearch + "%') as temp on booktrans.TransID =  temp.TransID order by booktrans.TransID desc";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetTransIDDetails(int transID = 0)
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select BookID,MemberID,LendDate,DueDate,ReturnDate,LendRate,Fine,LibBal,MemBal,booktrans.TransID,RenewalDays from booktrans where transid=" + transID;
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public DataTable GetTransLoanDetails(string strSearch = "")
        {
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select top 100 BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans inner join (select TransID as transid from booktrans where booktrans.BookID like '%" + strSearch + "%' union select TransID as transid from booktrans where booktrans.MemberID like '%" + strSearch + "%') as temp on booktrans.TransID =  temp.TransID where returndate is null order by booktrans.TransID desc";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public int GetMemBalAmt(string memberID = "")
        {
            int ds = 0;
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select isnull(sum(MemBal),0)-isnull(sum(LibBal),0) from booktrans where memberid='" + memberID + "' and returndate is null";
                ds = Int32.Parse(dbMgr.ExecuteScalar(CommandType.Text, strQuery).ToString()) ;
            }
            return ds;
        }

        public string GetMemLastVisited(string memberID = "")
        {
            string ds = "";
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "select MAX(LendDate) from booktrans where memberid='" + memberID + "'";
                ds = dbMgr.ExecuteScalar(CommandType.Text, strQuery).ToString();
            }
            return ds;
        }

        public DataTable GetLoanDetailsBooksMem(string bookID = "", string memberID = "", bool bOnlyLoaned=false)
        {
            if(bookID.Contains("-") && !bOnlyLoaned)
            {
                bookID = bookID.Split('-')[0];
            }
            DataTable ds = new DataTable();
            using (DBManager dbMgr = new DBManager(DataProvider.SqlClient, DBConnection.Connstring))
            {
                String strQuery = "";
                if(bOnlyLoaned)
                    strQuery = "select BookID,MemberID,LendDate,DueDate,ReturnDate,TransID from booktrans where bookid='" + bookID + "' and returndate is null";
                else
                    strQuery = "select BookID,MemberID,LendDate,DueDate,ReturnDate,TransID from booktrans where bookid like '%" + bookID + "%' and memberid='" + memberID + "'";
                ds = dbMgr.ExecuteDataTable(CommandType.Text, strQuery);
            }
            return ds;
        }

        public void InsertBookTrans(Entities.booktran tranObj)
        {         
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            dbCtx.booktrans.Add(tranObj);
            dbCtx.SaveChanges();
        }

        public void UpdateBookReturn(Entities.booktran btTran, int TransID)
        {
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            Entities.booktran bkUpdate = dbCtx.booktrans.First(b => b.TRANSID == TransID);
            bkUpdate.TRANSID = btTran.TRANSID;
            bkUpdate.ReturnDate = btTran.ReturnDate;
            dbCtx.SaveChanges();

        }
        public void UpdateBookTrans(Entities.booktran btTran, int TransID)
        {
            Entities.LibEntities dbCtx = new Entities.LibEntities();
            Entities.booktran bkUpdate = dbCtx.booktrans.First(b => b.TRANSID == TransID);
            if (String.IsNullOrEmpty(btTran.MemberID))
            {
                bkUpdate.TRANSID = btTran.TRANSID;
                bkUpdate.ReturnDate = btTran.ReturnDate;
                //bt.LendRate = Int32.Parse(ds.Rows[0]["LendRate"].ToString());
                bkUpdate.Fine = btTran.Fine;
                bkUpdate.LibBal = btTran.LibBal;
                bkUpdate.MemBal = btTran.MemBal;
            }
            else
                BaseRepository.CopyPropertyValues(btTran, bkUpdate);            
            dbCtx.SaveChanges();

        }
    }
}
