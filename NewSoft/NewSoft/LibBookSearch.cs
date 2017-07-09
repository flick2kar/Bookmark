using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace NewSoft
{
    public class LibBookSearch : frmLibSoft
    {
        public void initializeTrans()
        {
            frmLibSoft mainForm = new frmLibSoft();
            DataSet dsLoan = new DataSet();
            bool bMemAvail = false;
            dsLoan = returnLoanDetails(TxtBookId.Text);
            //if (dsLoan.Tables[0].Rows.Count > 1)
            //{
            //}
            if (dsLoan.Tables[0].Rows.Count > 0 && dsLoan.Tables[0].Rows[0]["ReturnDate"].ToString() == "")
            {
                //dateReturn.Checked = false;
                mainForm.TxtMembers.Text = dsLoan.Tables[0].Rows[0]["MemberID"].ToString();
                setMemName();
                //mainForm.TxtFine.Text = dsLoan.Tables[0].Rows[0]["Fine"].ToString();
                //mainForm.TxtTransRate.Text = dsLoan.Tables[0].Rows[0]["LendRate"].ToString();
                //mainForm.TxtBal.Text = dsLoan.Tables[0].Rows[0]["LibBal"].ToString();
                //mainForm.TxtRenewal.Text = dsLoan.Tables[0].Rows[0]["RenewalDays"].ToString();
            }
            else
            {
                TxtMembers.Text = "";
                TxtMemberName.Text = "";
                //TxtTransRate.Text = "0";
                //TxtFine.Text = "0";
            }
        }

        public void displayBookDetails(ComboBox lstAlphas,DataGridView gridBooks)
        {
            String strQuery = "select BookID,BookName,Authorname,BookPrice,LendRate,CategoryName,Name from ((books left join authors on books.authorid=authors.authorid) inner join category on category.CategoryID=books.CategoryID) Left join Subcategory on Subcategory.ID=books.SubCategoryID where bookid like 'a%' order by bookid;";

            DataSet ds = new DataSet();
            ds = returnDS(strQuery, "books");
            lstAlphas.SelectedIndex = 0;

            gridBooks.DataSource = ds;
            gridBooks.DataMember = "books";
        }

        public void Book_Search(DataGridView gridBooks,string strBookSearch)
        {
            DataSet ds = new DataSet();
            //String strQuery = "select BookName from books where bookid in (select BookID from books where BookID='" + txtSearch.Text + "' union all select BookID from books where BookName like '%" + txtSearch.Text + "%')";
            String strQuery = "select books.BookID,BookName,authors.Authorname,BookPrice,LendRate,category.CategoryName,SubCategory.Name from (((books inner join (select BookID as bookid from books where BookID like '%" + strBookSearch + "%' union select BookID as bookid from books where BookName like '%" + strBookSearch + "%' union select BookID as bookid from books inner join authors on authors.authorid=books.authorid where authors.authorname like '%" + strBookSearch + "%') as temp on books.bookid =  temp.bookid) inner join category on category.categoryid=books.categoryid) left join authors on authors.authorid=books.authorid) Left join Subcategory on Subcategory.ID=books.SubCategoryID";
            ds = returnDS(strQuery, "books");
            gridBooks.DataSource = ds;
            gridBooks.DataMember = "books";
        }

        public DataSet returnLoanDetails(String bookId)
        {
            DataSet ds = new DataSet();
            //String strQuery = "select BookName from books where bookid in (select BookID from books where BookID='" + txtSearch.Text + "' union all select BookID from books where BookName like '%" + txtSearch.Text + "%')";
            String strQuery = "select BookID,MemberID,LendDate,DueDate,ReturnDate,LendRate,Fine,LibBal,MemBal,booktrans.TransID,RenewalDays from booktrans where BookID='" + bookId + "' and returndate is null";
            ds = returnDS(strQuery, "booktrans");
            return ds;
        }

        public DataSet returnBookDetails(String bookId)
        {
            DataSet ds = new DataSet();
            //String strQuery = "select BookName from books where bookid in (select BookID from books where BookID='" + txtSearch.Text + "' union all select BookID from books where BookName like '%" + txtSearch.Text + "%')";
            String strQuery = "select books.BookID,BookName,authors.Authorname,BookPrice,LendRate,category.CategoryName,SubCategory.Name,Series.SeriesName from (((books inner join category on category.categoryid=books.categoryid) left join authors on authors.authorid=books.authorid) Left join Subcategory on Subcategory.ID=books.SubCategoryID)Left join Series on Series.SeriesID=books.SeriesID where BookID='" + bookId + "'";
            ds = returnDS(strQuery, "books");
            return ds;
        } 

    }
}
