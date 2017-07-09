using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace NewSoft
{
    public class LibBookTrans : frmLibSoft
    {
        public DataSet returnLoanDetails(int transID)
        {
            DataSet ds = new DataSet();
            //String strQuery = "select BookName from books where bookid in (select BookID from books where BookID='" + txtSearch.Text + "' union all select BookID from books where BookName like '%" + txtSearch.Text + "%')";
            String strQuery = "select BookID,MemberID,LendDate,DueDate,ReturnDate,LendRate,Fine,LibBal,MemBal,booktrans.TransID,RenewalDays from booktrans where transid=" + transID;
            ds = returnDS(strQuery, "booktrans");
            return ds;
        }

        public DataSet displayTrans(bookTrans bt, DataGridView gridTrans)
        {
            DataSet ds = new DataSet(); 
            String strQuery = "";

            if (!(bt.dateFrom.ToString().Equals("")) && !(bt.dateTo.ToString().Equals("")))
            {
                strQuery = "select top 10 BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans inner join (select TransID as transid from booktrans where booktrans.BookID like '%" + TxtSearch_trans.Text + "%' union select TransID as transid from booktrans where booktrans.MemberID like '%" + TxtSearch_trans.Text + "%') as temp on booktrans.TransID =  temp.TransID where duedate Between @fromDate And @toDate";
                ds = returnDisplayTransDS(strQuery, "booktrans",bt);
                
            }
            else if (!(bt.memberId.ToString().Equals("")))
            {
                TxtSearch_trans.Text = bt.memberId;
                strQuery = "select top 10 BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans where memberid='" + bt.memberId + "' and returndate is null";
                ds = returnDS(strQuery, "booktrans");
            }
            else
            {
                strQuery = "select top 10 BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans inner join (select TransID as transid from booktrans where booktrans.BookID like '%" + TxtSearch_trans.Text + "%' union select TransID as transid from booktrans where booktrans.MemberID like '%" + TxtSearch_trans.Text + "%') as temp on booktrans.TransID =  temp.TransID";


                ds = returnDS(strQuery, "booktrans");
            }
            gridTrans.DataSource = ds;
            gridTrans.DataMember = "booktrans";
            return ds;
        }

        public DataSet transSearch(DataGridView gridTrans,string strSearchtrans)
        {
            String strQuery = "select BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans inner join (select TransID as transid from booktrans where booktrans.BookID like '%" + strSearchtrans + "%' union select TransID as transid from booktrans where booktrans.MemberID like '%" + strSearchtrans + "%') as temp on booktrans.TransID =  temp.TransID";
            DataSet ds = new DataSet();
            ds = returnDS(strQuery, "booktrans");
            gridTrans.DataSource = ds;
            gridTrans.DataMember = "booktrans";
            return ds;
        }
        
    }
}
