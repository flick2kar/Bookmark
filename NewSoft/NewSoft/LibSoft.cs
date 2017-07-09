using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NewSoft
{
    public partial class frmLibSoft : Form
    {
        String tempBookid = "";
        public DataSet dsMemberList = new DataSet();
        DataSet dsAuthorList = new DataSet();
        DataSet dsSubCatList = new DataSet();
        DataSet dsSeriesList = new DataSet();
        AutoCompleteStringCollection autoAuthors = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoSubCategory = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoSeries = new AutoCompleteStringCollection();
        public frmLibSoft()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializePage();
        }

        #region Main Book Page
        #region Initialize page
        private void InitializePage()
        {
            Rectangle r = Screen.GetBounds(this);
            this.Location = new Point(0, 0);
            this.Size = r.Size;
            this.Height = 730;
            this.Width = 1020;
            double PercWidthChng = r.Width - 1024;
            PercWidthChng /= 1024;
            double PercHeightChng = r.Height - 768;
            PercHeightChng /= 768;

            foreach (Control c in this.Controls)
            {
                double NewWidth = this.Width;//c.Width + (c.Width * PercWidthChng);
                double NewHeight = this.Height;//c.Height + (c.Height * PercHeightChng);
                double NewX = c.Location.X + (c.Location.X * PercWidthChng);
                double NewY = c.Location.Y + (c.Location.Y * PercHeightChng);
                double fontsize = 10 + (c.Font.Size * PercHeightChng);
                c.Width = (int)Math.Floor(NewWidth);
                c.Height = (int)Math.Floor(NewHeight);
                c.Location = new Point((int)NewX, (int)NewY);
                c.Font = new Font("Bookman Old Style", (int)fontsize, c.Font.Style);
            }
            ////this.ClientSize = new System.Drawing.Size(1400, 700);
            ////panel6.Size = gridBooks.Size;
            ////panel6.AutoSize = true;
            //gridBooks.Height = r.Height / 2+100;
            //gridBooks.Width = r.Height/2+150;
            ////mainTab.Size = this.ClientSize;
            //tableLayoutPanel1.Size = this.ClientSize;
            //tableLayoutPanel1.Height = gridBooks.Height;
            //tableLayoutPanel1.Width = gridBooks.Width * 2-200;
            
            //panel6.Height = gridBooks.Height+100;// gridBooks.Height / 2 - 100;
            //panel6.Width = gridBooks.Width;
            //panel1.Height = gridBooks.Height / 2;
            //panel1.Width = gridBooks.Width/2;
            //panel1.Dock = DockStyle.Left;
            
            ////panel5.Height = 150;// gridBooks.Height / 2 - 50;
            ////panel5.AutoSize = true;
            //panel5.Dock = DockStyle.Left;
            ////gridBooks.AutoSize = tr
            
            //gridTrans.Height = gridBooks.Height;
            //gridTrans.Width = gridBooks.Width;
            //gridMembers.Height = gridBooks.Height;
            //gridMembers.Width = gridBooks.Width;
            //transPanel.Height = tableLayoutPanel1.Height / 2+250;
            //transPanel.Width = tableLayoutPanel1.Width / 2-150;            
            //memPanel.Height = transPanel.Height;
            //memPanel.Width = transPanel.Width+50;
            //layoutTrans.Size = this.ClientSize;
            //memLayout.Size = this.ClientSize;
            mainTab.TabPages[mainTab.SelectedIndex].Focus();
            mainTab.TabPages[mainTab.SelectedIndex].CausesValidation = true;            
            initializeMain();
        }

        private void initializeMain()
        {
            LibBookSearch cLBS = new LibBookSearch();
            cLBS.displayBookDetails(lstAlphas, gridBooks);
            initializeAuthorList();
            initializeSeriesList();
            initializeCategoryList("Main");
            initializeData();
            initializeMemberList("Main");
            cLBS.initializeTrans();
        }

       //Common
        private void initializeMemberList(String srcPage)
        {
            String strQuery = "select memberid,membername from members";
            dsMemberList = returnDS(strQuery, "members");
            DataRow rw = dsMemberList.Tables[0].NewRow();
            rw[0] = "--Select--";
            dsMemberList.Tables[0].Rows.InsertAt(rw, 0);
            if (srcPage.Equals("Main"))
            {
                //lstMembers.DataSource = dsMemberList.Tables[0];
                //lstMembers.DisplayMember = "memberid";
                //lstMembers.ValueMember = "memberid";
                txtMembers.Text = "";
            }
            else if (srcPage.Equals("Trans"))
            {
                //lstTransMemId.DataSource = dsMemberList.Tables[0];
                //lstTransMemId.DisplayMember = "memberid";
                //lstTransMemId.ValueMember = "memberid";
                txtTransMemId.Text = "";
            }


        }

        private void initializeCategoryList(String srcPage)
        {
            //Load Category
            String strQuery = "select CategoryID,CategoryName from Category";
            DataSet dsCategoryList = new DataSet();
            dsCategoryList = returnDS(strQuery, "Category");            
            DataRow rw = dsCategoryList.Tables[0].NewRow();
            rw[0] = 0;
            rw[1] = "--Select--";
            dsCategoryList.Tables[0].Rows.InsertAt(rw, 0);
            //Load Sub-Category
            strQuery = "select ID,Name from SubCategory";
            DataSet dsSubCategoryList = new DataSet();
            dsSubCategoryList = returnDS(strQuery, "SubCategory");
            rw = dsSubCategoryList.Tables[0].NewRow();
            rw[0] = 0;
            rw[1] = "--Select--";
            dsSubCategoryList.Tables[0].Rows.InsertAt(rw, 0);
            if (srcPage.Equals("Main"))
            {
                lstCategory.DataSource = dsCategoryList.Tables[0];
                lstCategory.DisplayMember = "CategoryName";
                lstCategory.ValueMember = "CategoryID";
                lstSubcat.DataSource = dsSubCategoryList.Tables[0];
                lstSubcat.DisplayMember = "Name";
                lstSubcat.ValueMember = "Id";

            }
        }

        private void initializeData()
        {
            if (gridBooks.RowCount > 0)
            {
                lstAuthor.SelectedIndex = 0;
                lstCategory.SelectedIndex = 0;
                lstSeries.SelectedIndex = 0;
                txtBookId.Text = gridBooks[0, 0].Value.ToString();
                tempBookid = txtBookId.Text;
                LibBookSearch lb = new LibBookSearch();
                DataSet ds = new DataSet();
                ds = lb.returnBookDetails(tempBookid);
                txtBookName.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                setLstValue(lstAuthor, ds.Tables[0].Rows[0].ItemArray[2].ToString());
                txtBookPrice.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                txtLendrate.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                setLstValue(lstCategory, ds.Tables[0].Rows[0].ItemArray[5].ToString());
                setLstValue(lstSubcat, ds.Tables[0].Rows[0].ItemArray[6].ToString());
                setLstValue(lstSeries, ds.Tables[0].Rows[0].ItemArray[7].ToString());
                autoAuthors.Clear();
                autoSubCategory.Clear();
                autoSeries.Clear();
                foreach (DataRowView dr in lstAuthor.Items)
                    autoAuthors.Add(dr.Row[1].ToString());

                foreach (DataRowView dr in lstSubcat.Items)
                    autoSubCategory.Add(dr.Row[1].ToString());

                foreach (DataRowView dr in lstSeries.Items)
                    autoSeries.Add(dr.Row[1].ToString());
                lstAuthor.AutoCompleteSource = AutoCompleteSource.CustomSource;
                lstAuthor.AutoCompleteCustomSource = autoAuthors;
                lstSubcat.AutoCompleteSource = AutoCompleteSource.CustomSource;
                lstSubcat.AutoCompleteCustomSource = autoSubCategory;
                lstSeries.AutoCompleteSource = AutoCompleteSource.CustomSource;
                lstSeries.AutoCompleteCustomSource = autoSeries;
            }
        }

        private void initializeAuthorList()
        {
            String strQuery = "select AuthorId,Authorname from authors";
            dsAuthorList = returnDS(strQuery, "authors");

           DataRow rw = dsAuthorList.Tables[0].NewRow();
           rw[0] = 0;
           rw[1] = "--Select--";

           dsAuthorList.Tables[0].Rows.InsertAt(rw, 0);
            
            lstAuthor.DataSource = dsAuthorList.Tables[0];
            lstAuthor.DisplayMember = "Authorname";
            lstAuthor.ValueMember = "AuthorId";
            
        }

        private void initializeSeriesList()
        {
            String strQuery = "select SeriesId,Seriesname from Series";
            DataSet dsSeriesList = new DataSet();
            dsSeriesList = returnDS(strQuery, "Series");

            DataRow rw = dsSeriesList.Tables[0].NewRow();
            rw[0] = 0;
            rw[1] = "--Select--";

            dsSeriesList.Tables[0].Rows.InsertAt(rw, 0);
            lstSeries.DataSource = dsSeriesList.Tables[0];
            lstSeries.DisplayMember = "Seriesname";
            lstSeries.ValueMember = "SeriesId";
        }


        
        #endregion Initialize page
        
        #region Books Page functions     
       


        private void gridBooks_RowContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LibBookSearch libBS = new LibBookSearch();
            if (e.RowIndex != -1)
            {
                txtBookId.Text = gridBooks[0, e.RowIndex].Value.ToString();
                tempBookid = txtBookId.Text;                
                DataSet ds = new DataSet();
                ds = libBS.returnBookDetails(tempBookid);
                txtBookName.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                setLstValue(lstAuthor, ds.Tables[0].Rows[0].ItemArray[2].ToString());
                txtBookPrice.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                txtLendrate.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                setLstValue(lstCategory, ds.Tables[0].Rows[0].ItemArray[5].ToString());
                setLstValue(lstSubcat, ds.Tables[0].Rows[0].ItemArray[6].ToString());
                setLstValue(lstSeries, ds.Tables[0].Rows[0].ItemArray[7].ToString());

                //txtBookName.Text = gridBooks[1, e.RowIndex].Value.ToString();
                //setLstValue(lstAuthor, gridBooks[2, e.RowIndex].Value.ToString());
                //txtBookPrice.Text = gridBooks[3, e.RowIndex].Value.ToString();
                //txtLendrate.Text = gridBooks[4, e.RowIndex].Value.ToString();
                //setLstValue(lstCategory, gridBooks[5, e.RowIndex].Value.ToString());
                //setLstValue(lstSubcat, gridBooks[6, e.RowIndex].Value.ToString());
                libBS.initializeTrans();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtBookId.Text == "" || txtBookName.Text == "" || lstCategory.SelectedIndex == 0 || txtBookPrice.Text == "" || txtLendrate.Text == "")
            {
                MessageBox.Show("BookId/BookName/Category/Book Price/Lend Rate cannot be empty");
                return;
            }
            String strValBook = "select count(*) from books where bookid='" +
                txtBookId.Text + "'";
            int intCnt = returnInt(strValBook, "books");
            if (intCnt <= 0)
            {
                MessageBox.Show("BookId doesn't exists");
                return;
            }
            else
            {
                lstAuthor.SelectedText = lstAuthor.Text;
                String strQuery = "update books set BookID='" + txtBookId.Text + "',BookName='" + txtBookName.Text + "',BookPrice=" + txtBookPrice.Text + ",LendRate=" + txtLendrate.Text + ",AuthorID=" + lstAuthor.SelectedValue + ",CategoryID=" + lstCategory.SelectedValue + ",SubCategoryID=" + lstSubcat.SelectedValue + ",SeriesID=" + lstSeries.SelectedValue + " where bookid='" + tempBookid + "' ";
                InsertUpdateQuery(strQuery);
                LibBookSearch cLBS = new LibBookSearch();
                cLBS.displayBookDetails(lstAlphas, gridBooks);
                tempBookid = txtBookId.Text;
                //OleDbDataAdapter dataAdap = new OleDbDataAdapter(strQuery, conn);            
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            LibBookSearch lbs = new LibBookSearch();
            lbs.Book_Search(gridBooks,txtSearch.Text);
        }

        
        private void Book_Search(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                LibBookSearch lbs = new LibBookSearch();
                lbs.Book_Search(gridBooks,txtSearch.Text);
            }

        }

        private void btnGetall_Click(object sender, EventArgs e)
        {
            LibBookSearch cLBS = new LibBookSearch();
            cLBS.displayBookDetails(lstAlphas, gridBooks);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBookId.Text = "";
            txtBookName.Text = "";
            txtBookPrice.Text = "";
            txtLendrate.Text = "";
            //txtTransRate.Text = "";
            //txtFine.Text = "";

            lstAuthor.SelectedIndex = 0;
            lstCategory.SelectedIndex = 0;
            lstSubcat.SelectedIndex = 0;
            lstSeries.SelectedIndex = 0;
            //lstMembers.SelectedIndex = 0;
            txtMembers.Text = "";

        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            if (txtBookId.Text == "" || txtBookName.Text == "" || lstCategory.SelectedIndex == 0 || txtBookPrice.Text == "" || txtLendrate.Text == "")
            {
                MessageBox.Show("BookId/BookName/Category/Book Price/Lend Rate cannot be empty");
                return;
            }
            String strValBook = "select count(*) from books where bookid='" + txtBookId.Text + "'";
            int intCnt = returnInt(strValBook, "books");
            if (intCnt > 0)
            {
                MessageBox.Show("BookId already exists");
                return;
            }
            else
            {
                String strQuery = "insert into books values('" + txtBookId.Text + "','" + txtBookName.Text + "'," + txtLendrate.Text + "," + txtBookPrice.Text + "," + lstAuthor.SelectedValue + "," + lstCategory.SelectedValue + "," + lstSubcat.SelectedValue + ",'" + DateTime.Now.ToString() + "'," + lstSeries.SelectedValue+")";
                InsertUpdateQuery(strQuery);
                LibBookSearch cLBS = new LibBookSearch();
                cLBS.displayBookDetails(lstAlphas, gridBooks);
                tempBookid = txtBookId.Text;
            }

        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (txtBookId.Text.Equals(""))
            {
                MessageBox.Show("BookId cannot be empty");
            }

            String strValBook = "select count(*) from books where bookid='" + txtBookId.Text + "'";
            int intCnt = returnInt(strValBook, "books");
            if (intCnt > 0)
            {
                if (MessageBox.Show("Are you sure do you want to delete this book?", "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    String strQuery = "delete from books where bookid='" + txtBookId.Text + "'";
                    InsertUpdateQuery(strQuery);
                    LibBookSearch cLBS = new LibBookSearch();
                    cLBS.displayBookDetails(lstAlphas, gridBooks);
                    tempBookid = txtBookId.Text;
                }


            }
            else
            {
                MessageBox.Show("BookId doesn't exists");
                return;
            }

        }

        

        

        private void btnAddAuth_Click(object sender, EventArgs e)
        {
            //lstAuthor.DataBindings.Clear();
            //lstAuthor.DataSource = null;
            String strAuthor = lstAuthor.Text;
            String strValAuthor = "select count(*) from authors where authorname='" + strAuthor + "'";
            int intCnt = returnInt(strValAuthor, "authors");
            if (intCnt > 0)
            {
                MessageBox.Show("Author already present");
                return;
            }
            String strQuery = "declare @authid int;select @authid=max(authorid)+1 from authors;insert into authors(AuthorId,AuthorName) values(@authid,'" + lstAuthor.Text + "')";
            InsertUpdateQuery(strQuery);
            
            String strAuthQuery = "select AuthorId,Authorname from authors order by Authorname asc";
            dsAuthorList = returnDS(strAuthQuery, "authors");

            lstAuthor.DataSource = dsAuthorList.Tables[0];
            lstAuthor.DisplayMember = "Authorname";
            lstAuthor.ValueMember = "AuthorId";            
            setLstValue(lstAuthor, strAuthor);
            autoAuthors.Clear();
            foreach (DataRowView dr in lstAuthor.Items)
                autoAuthors.Add(dr.Row[1].ToString());
            lstAuthor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lstAuthor.AutoCompleteCustomSource = autoAuthors;
        }

        private void btnSubCatAdd_Click(object sender, EventArgs e)
        {
            String strSubcat = lstSubcat.Text;
            String strValSubcat = "select count(*) from SubCategory where Name='" + strSubcat + "'";
            int intCnt = returnInt(strValSubcat, "SubCategory");
            if (intCnt > 0)
            {
                MessageBox.Show("Sub-Category already present");
                return;
            }
            
            String strQuery = "declare @SubCategoryid int;select @SubCategoryid=max(id)+1 from SubCategory;insert into SubCategory(Id,Name,CategoryID) values(@SubCategoryid,'" + lstSubcat.Text + "',"+lstCategory.SelectedValue+")";
            InsertUpdateQuery(strQuery);

            String strSubQuery = "select Id,Name from SubCategory order by name asc";
            dsSubCatList = returnDS(strSubQuery, "SubCategory");
            DataRow rw = dsSubCatList.Tables[0].NewRow();
            rw[1] = "--Select--";
            dsSubCatList.Tables[0].Rows.InsertAt(rw, 0);
            lstSubcat.DataSource = dsSubCatList.Tables[0];
            lstSubcat.DisplayMember = "Name";
            lstSubcat.ValueMember = "Id";
            setLstValue(lstSubcat, strSubcat);
            autoSubCategory.Clear();
            
            foreach (DataRowView dr in lstSubcat.Items)
                autoSubCategory.Add(dr.Row[1].ToString());
            
            lstSubcat.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lstSubcat.AutoCompleteCustomSource = autoSubCategory;

        }

        private void btnSeriesAdd_Click(object sender, EventArgs e)
        {
            String strSeries = lstSeries.Text;
            String strValstrSeries = "select count(*) from Series where SeriesName='" + strSeries + "'";
            int intCnt = returnInt(strValstrSeries, "Series");
            if (intCnt > 0)
            {
                MessageBox.Show("Series already present");
                return;
            }
            String strQuery = "insert into Series values('" + lstSeries.Text + "')";
            InsertUpdateQuery(strQuery);

            String strSeriesQuery = "select SeriesId,SeriesName from Series order by SeriesName asc";
            dsSeriesList = returnDS(strSeriesQuery, "Series");
            DataRow rw = dsSeriesList.Tables[0].NewRow();
            rw[1] = "--Select--";
            dsSeriesList.Tables[0].Rows.InsertAt(rw, 0);
            lstSeries.DataSource = dsSeriesList.Tables[0];
            lstSeries.DisplayMember = "Seriesname";
            lstSeries.ValueMember = "SeriesId";
            setLstValue(lstSeries, strSeries);
            autoSeries.Clear();


            foreach (DataRowView dr in lstSeries.Items)
                autoSeries.Add(dr.Row[1].ToString());

            lstSeries.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lstSeries.AutoCompleteCustomSource = autoSubCategory;
        }

        private void btnSeriesDel_Click(object sender, EventArgs e)
        {
            String strQuery = "delete from Series where Seriesname=('" + lstSeries.Text + "')";
            InsertUpdateQuery(strQuery);
            String strSeriesQuery = "select SeriesId,SeriesName from Series";
            dsSubCatList = returnDS(strSeriesQuery, "SubCategory");

            DataRow rw = dsSeriesList.Tables[0].NewRow();
            rw[1] = "--Select--";
            dsSeriesList.Tables[0].Rows.InsertAt(rw, 0);
            lstSeries.DataSource = dsSeriesList.Tables[0];
            lstSeries.DisplayMember = "SeriesName";
            lstSeries.ValueMember = "SeriesId";
            autoSubCategory.Clear();

            foreach (DataRowView dr in lstSeries.Items)
                autoSeries.Add(dr.Row[1].ToString());

            lstSeries.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lstSeries.AutoCompleteCustomSource = autoSubCategory;
        }
        private void btnDelAuth_Click(object sender, EventArgs e)
        {
            String strQuery = "delete from authors where authorname=('" + lstAuthor.Text + "')";
            InsertUpdateQuery(strQuery);
            String strAuthQuery = "select AuthorId,Authorname from authors";
            dsAuthorList = returnDS(strAuthQuery, "authors");

            lstAuthor.DataSource = dsAuthorList.Tables[0];
            lstAuthor.DisplayMember = "Authorname";
            lstAuthor.ValueMember = "AuthorId";
            autoAuthors.Clear();
            foreach (DataRowView dr in lstAuthor.Items)
                autoAuthors.Add(dr.Row[1].ToString());
            lstAuthor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lstAuthor.AutoCompleteCustomSource = autoAuthors;
        }
        private void btnSubCatDel_Click(object sender, EventArgs e)
        {
            String strQuery = "delete from SubCategory where Name=('" + lstSubcat.Text + "')";
            InsertUpdateQuery(strQuery);
            String strSubQuery = "select Id,Name from SubCategory";
            dsSubCatList = returnDS(strSubQuery, "SubCategory");

            DataRow rw = dsSubCatList.Tables[0].NewRow();
            rw[1] = "--Select--";
            dsSubCatList.Tables[0].Rows.InsertAt(rw, 0);
            lstSubcat.DataSource = dsSubCatList.Tables[0];
            lstSubcat.DisplayMember = "Name";
            lstSubcat.ValueMember = "Id";
            autoSubCategory.Clear();

            foreach (DataRowView dr in lstSubcat.Items)
                autoSubCategory.Add(dr.Row[1].ToString());

            lstSubcat.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lstSubcat.AutoCompleteCustomSource = autoSubCategory;
        }      

        private void lstAlphas_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strQuery = "select BookID,BookName,Authorname,BookPrice,LendRate,CategoryName,SubCategory.Name from ((books left join authors on books.authorid=authors.authorid) inner join category on category.CategoryID=books.CategoryID) Left join Subcategory on Subcategory.ID=books.SubCategoryID where bookid like '" + lstAlphas.SelectedItem + "%' order by bookid asc";

            DataSet ds = new DataSet();
            ds = returnDS(strQuery, "books");

            gridBooks.DataSource = ds;
            gridBooks.DataMember = "books";
        }

        #endregion Books Page functions
        #endregion Main Book Page

        #region Transaction Page
        private void initializeTransPage()
        {
            bookTrans bt = new bookTrans();
            LibBookTrans lbt = new LibBookTrans();
            DataSet dsTrans = new DataSet();
            dsTrans=lbt.displayTrans(bt, gridTrans);
            initializeMemberList("Trans");
            initializeTransData(dsTrans);     
            
        }

        public void initializeTransData(DataSet dsTrans)
        {
            if (gridTrans.Rows.Count > 0)
            {
                LibBookTrans lbt = new LibBookTrans();
                DataSet ds = new DataSet();
                TxtTransBookid.Text = dsTrans.Tables[0].Rows[0].ItemArray[0].ToString();
                TxtTransMemId.Text = dsTrans.Tables[0].Rows[0].ItemArray[1].ToString();
                LblTransId.Text = dsTrans.Tables[0].Rows[0].ItemArray[5].ToString();
                setTransMemName();
                setBookLabel();
                ds = lbt.returnLoanDetails(Int32.Parse(LblTransId.Text.ToString()));
                DateTransLoan.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                DateTransDue.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                DateTransReturn.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                TxtTransLendRate.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                TxtTransFine.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                txtLibBal.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();
                txtMemBal.Text = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                TxtRenewal1.Text = ds.Tables[0].Rows[0].ItemArray[10].ToString();
            }
        }

        private void btnSearch_trans_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            LibBookTrans lbt = new LibBookTrans();
            ds=lbt.transSearch(gridTrans, txtSearch_trans.Text);
            initializeTransData(ds);
        }       

        private void Trans_Search(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                LibBookTrans lbt = new LibBookTrans();
                lbt.transSearch(gridTrans,txtSearch_trans.Text);
            }            
        }

        private void btnAll_Trans_Click(object sender, EventArgs e)
        {
            String strQuery = "select top 100 BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans";
            DataSet ds = new DataSet();
            ds = returnDS(strQuery, "booktrans");
            gridTrans.DataSource = ds;
            gridTrans.DataMember = "booktrans";
        }

        private void btnLoan_Click(object sender, EventArgs e)
        {
            String strQuery = "select top 100 BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans inner join (select TransID as transid from booktrans where booktrans.BookID like '%" + txtSearch_trans.Text + "%' union select TransID as transid from booktrans where booktrans.MemberID like '%" + txtSearch_trans.Text + "%') as temp on booktrans.TransID =  temp.TransID where returndate is null";
            DataSet ds = new DataSet();
            ds = returnDS(strQuery, "booktrans");
            gridTrans.DataSource = ds;
            gridTrans.DataMember = "booktrans";
        }

        private void leaveBookID(object sender, EventArgs e)
        {
            setBookLabel();
        }

        public void setBookLabel()
        {
            String strQuery = "select BookName from books where bookid='" + txtTransBookid.Text + "'";
            DataSet ds = new DataSet();
            ds = returnDS(strQuery, "books");
            if (ds.Tables[0].Rows.Count > 0)
                lblBookLabel.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            else
                lblBookLabel.Text = "Book not available";
        }

        private void updateLoanReturn(bookTrans bt, bool bBook, bool bLoanReturn)
        {

            int intFine = 0, intTransRate = 0, intLibBal = 0, intRenewal = 0,intMemBal=0;
            LibBookSearch cLBS = new LibBookSearch();


            if (bLoanReturn)
            {

                String strValBook = "select count(*) from booktrans where bookid='" + bt.bookId + "' and returndate is null";
                int intCnt = returnInt(strValBook, "books");
                if (intCnt > 0)
                {
                    MessageBox.Show(bt.bookId + " is already loaned out. Please check.");
                    return;
                }
                if (!bt.strFine.Equals(""))
                    intFine = Int32.Parse(bt.strFine);
                if (!bt.strTransRate.Equals(""))
                    intTransRate = Int32.Parse(bt.strTransRate);
                if (!bt.strLibBal.Equals(""))
                    intLibBal = Int32.Parse(bt.strLibBal);
                if (!bt.strMemBal.Equals(""))
                    intMemBal = Int32.Parse(bt.strMemBal);
                if (!bt.strRenewal.Equals(""))
                    intRenewal = Int32.Parse(bt.strRenewal);

                String strQuery = "insert into booktrans(BookID,MemberID,LendDate,DueDate,ReturnDate,LendRate,Fine,LibBal,MemBal,RenewalDays,CreatedDate) values('" + bt.bookId + "','" + bt.memberId + "','" + bt.dateFrom + "','" + bt.dateTo + "',null," + intTransRate + "," + intFine + "," + intLibBal + "," + intMemBal + "," + intRenewal + ",'" + DateTime.Now.ToString() + "')";
                InsertUpdateQuery(strQuery);
                if (bBook)
                {
                    cLBS.displayBookDetails(lstAlphas, gridBooks);
                    tempBookid = bt.bookId;
                }

            }
            else
            {

                String strValBook = "select count(*) from booktrans where bookid='" + bt.bookId + "' and memberid='" + bt.memberId + "' and returndate is not null";
                int intCnt = returnInt(strValBook, "books");
                if (intCnt > 0)
                {
                    MessageBox.Show(bt.bookId + " is not loaned out");
                    return;
                }
                String strQuery = "update booktrans set LendRate=" + intTransRate + ",Fine=" + intFine + ",LibBal=" + intLibBal + ",MemBal=" + intMemBal + ",RenewalDays=" + intRenewal + ",ReturnDate='" + bt.dateReturn + "' where bookid='" + bt.bookId + "' and memberid='" + bt.memberId + "'";
                InsertUpdateQuery(strQuery);
                if (bBook)
                {
                    cLBS.displayBookDetails(lstAlphas, gridBooks);
                    tempBookid = txtBookId.Text;
                }

            }

        }

        private void btnLoan_Ret_Trans_Click(object sender, EventArgs e)
        {
            bookTrans bt = new bookTrans();
            StringBuilder arBookid = new StringBuilder();

            String strTemp = "";
            int strFine = 0;
            int strLendRate = 0;
            int strBalance = 0;
            bool isMulLoan = false;



            if (gridTrans.RowCount > 0)
            {
                for (int i = 0; i < gridTrans.RowCount; i++)
                {
                    if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                    {
                        isMulLoan = true;
                        bt.bookId = gridTrans[1, i].Value.ToString();
                        if (arBookid.Length == 0)
                            strTemp = gridTrans[2, i].Value.ToString();
                        else
                        {
                            if (strTemp != gridTrans[2, i].Value.ToString())
                            {
                                MessageBox.Show("Loan processing can be done only for one member at a time");
                                return;
                            }
                        }

                        bt.memberId = gridTrans[2, i].Value.ToString();


                        if (bt.memberId.Equals(""))
                        {
                            MessageBox.Show("MemberID cannot be empty");
                            return;
                        }
                        LibMembers lm = new LibMembers();
                        if (!lm.returnMemberStatus(bt.memberId))
                        {
                            if (MessageBox.Show("Do you want to continue?", "Inactive Member", MessageBoxButtons.YesNo) == DialogResult.No)
                                return;
                        }
                        bt.dateFrom = gridTrans[3, i].Value.ToString();
                        bt.dateTo = gridTrans[4, i].Value.ToString();

                        if (dateTransReturn.Checked)
                        {
                            //    bt.dateReturn = DateTime.Parse(dateTransReturn.Text).ToShortDateString();
                            //else
                            //{
                            MessageBox.Show("You cannot specify Return date for Loan Processing");
                            return;
                        }

                        if (checkLoaned(bt.bookId, bt.memberId, true) > 0)
                        {
                            MessageBox.Show("Bookid " + bt.bookId + " already loaned out");
                            return;
                        }

                        if (checkLoaned(bt.bookId, bt.memberId, false) > 0)
                        {
                            if (MessageBox.Show("Do you want to continue?", bt.bookId+ " already read by member", MessageBoxButtons.YesNo) == DialogResult.No)
                                return;
                        }

                        bt.strTransRate = gridTrans[7, i].Value.ToString();
                        bt.strFine = gridTrans[8, i].Value.ToString();
                        bt.strLibBal = gridTrans[9, i].Value.ToString();
                        bt.strMemBal = gridTrans[10, i].Value.ToString();
                        bt.strRenewal = gridTrans[11, i].Value.ToString();

                        if (!bt.strFine.Equals(""))
                            strFine = Int32.Parse(bt.strFine) + strFine;
                        if (!bt.strTransRate.Equals(""))
                            strLendRate = Int32.Parse(bt.strTransRate) + strLendRate;
                        if (!bt.strLibBal.Equals(""))
                            strBalance = Int32.Parse(bt.strLibBal) + strBalance;

                        arBookid.Append(bt.bookId + ",");
                        //updateLoanReturn(bt, false, false);

                    }
                }
                if (isMulLoan)
                {
                    if (arBookid.Length > 0) arBookid.Remove(arBookid.Length - 1, 1);
                    if (MessageBox.Show("Books Loaned:" + arBookid.ToString() + "\nMemberID:" + bt.memberId + "\nLend Rate:" + strLendRate + "\nFine:" + strFine + "\nBalance:" + strBalance, "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < gridTrans.RowCount; i++)
                        {
                            if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                            {
                                bt.bookId = gridTrans[1, i].Value.ToString();
                                bt.memberId = gridTrans[2, i].Value.ToString();
                                bt.dateFrom = gridTrans[3, i].Value.ToString();
                                bt.dateTo = gridTrans[4, i].Value.ToString();
                                //bt.dateReturn = DateTime.Parse(dateTransReturn.Text).ToShortDateString();
                                bt.strTransRate = gridTrans[7, i].Value.ToString();
                                bt.strFine = gridTrans[8, i].Value.ToString();
                                bt.strLibBal = gridTrans[9, i].Value.ToString();
                                bt.strMemBal = gridTrans[10, i].Value.ToString();
                                bt.strRenewal = gridTrans[11, i].Value.ToString();
                                updateLoanReturn(bt, false, true);
                            }

                        }
                        bt.dateFrom = "";
                        bt.dateTo = "";
                        bt.dateReturn = "";
                        LibBookTrans lbt = new LibBookTrans();
                        lbt.displayTrans(bt, gridTrans);
                    }
                }
                else
                {
                    if (dateTransReturn.Checked)
                    {
                        MessageBox.Show("You cannot specify Return date for Loan Processing");
                        return;
                    }

                    if (checkLoaned(txtTransBookid.Text, txtTransMemId.Text, true) > 0)
                    {
                        MessageBox.Show("Bookid " + txtTransBookid.Text + " already loaned out");
                        return;
                    }
                    if (MessageBox.Show("Books Loaned:" + txtTransBookid.Text + "\nMemberID:" + txtTransMemId.Text + "\nLend Rate:" + txtTransLendRate.Text + "\nFine:" + txtTransFine.Text + "\nBalance:" + txtMemBal.Text + "\nRenewal Days:" + txtRenewal.Text, "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bt.bookId = txtTransBookid.Text.ToString();
                        bt.memberId = txtTransMemId.Text;
                        bt.dateFrom = DateTime.Parse(dateTransLoan.Text).ToShortDateString();
                        bt.dateTo = DateTime.Parse(dateTransDue.Text).ToShortDateString();
                        bt.strTransRate = txtTransLendRate.Text;
                        bt.strFine = txtTransFine.Text;
                        bt.strLibBal = txtMemBal.Text;
                        //bt.strMemBal=txt
                        bt.strRenewal = txtRenewal.Text;

                        updateLoanReturn(bt, false, true);

                        bt.dateFrom = "";
                        bt.dateTo = "";
                        bt.dateReturn = "";

                        LibBookTrans lbt = new LibBookTrans();
                        lbt.displayTrans(bt, gridTrans);
                    }
                }
            }
        }

        private int checkLoaned(String bookId, String memberId, bool bOnlyLoaned)
        {
            if (bOnlyLoaned)
            {
                String strQuery = "select count(1) from booktrans where bookid='" + bookId + "' and memberid='" + memberId + "' and returndate is null";
                return (returnInt(strQuery, "booktrans"));
            }
            else
            {
                String strQuery = "select count(1) from booktrans where bookid='" + bookId + "' and memberid='" + memberId + "'";
                return (returnInt(strQuery, "booktrans"));

            }
        }

        private void btn_Ret_Trans_Click(object sender, EventArgs e)
        {
            bookTrans bt = new bookTrans();
            StringBuilder arBookid = new StringBuilder();
            String strTemp = "";
            int strFine = 0;
            int strLendRate = 0;
            int strMemBalance = 0; int strLibBalance = 0;
            bool isMulLoan = false;
            DataSet ds = new DataSet();
            LibBookTrans lbt = new LibBookTrans();

            
            if (gridTrans.RowCount > 0)
            {
                for (int i = 0; i < gridTrans.RowCount; i++)
                {
                    if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                    {
                        isMulLoan = true;
                        ds = lbt.returnLoanDetails(Int32.Parse(gridTrans[6, i].Value.ToString()));
                        bt.bookId = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        if (arBookid.Length == 0)
                            strTemp = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        else
                        {
                            if (strTemp != ds.Tables[0].Rows[0].ItemArray[1].ToString())
                            {
                                MessageBox.Show("Return processing can be done only for one member at a time");
                                return;
                            }
                        }

                        bt.memberId = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        if (bt.memberId.Equals(""))
                        {
                            MessageBox.Show("MemberID cannot be empty");
                            return;
                        }

                        bt.dateFrom = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                        bt.dateTo = ds.Tables[0].Rows[0].ItemArray[3].ToString();

                        if (dateTransReturn.Checked)
                            bt.dateReturn = DateTime.Parse(dateTransReturn.Text).ToShortDateString();
                        else
                        {
                            MessageBox.Show("Please specify a Return Date");
                            return;
                        }
                        bt.strTransRate = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                        bt.strFine = txtTransFine.Text;
                        bt.strLibBal = txtLibBal.Text;
                        bt.strMemBal = txtMemBal.Text;

                        if (!bt.strFine.Equals(""))
                            strFine = Int32.Parse(bt.strFine);// +strFine;
                        if (!bt.strTransRate.Equals(""))
                            strLendRate = Int32.Parse(bt.strTransRate) + strLendRate;
                        if (!bt.strMemBal.Equals(""))
                            strMemBalance = Int32.Parse(bt.strMemBal); //+ strMemBalance;
                        if (!bt.strLibBal.Equals(""))
                            strLibBalance = Int32.Parse(bt.strLibBal); //+ strLibBalance;
                        

                        bt.strTransRate = "";
                        bt.strFine = "";
                        bt.strLibBal = "";
                        arBookid.Append(bt.bookId + ",");
                        //updateLoanReturn(bt, false, false);
                    }
                }
                if (isMulLoan)
                {
                    if (arBookid.Length > 0) arBookid.Remove(arBookid.Length - 1, 1);
                    if (MessageBox.Show("Books Returned:" + arBookid.ToString() + "\nMemberID:" + bt.memberId + "\nMem Bal:" + strMemBalance + "\nLib Bal:" + strLibBalance + "\nFine:" + strFine, "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < gridTrans.RowCount; i++)
                        {
                            if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                            {
                                bt.bookId = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                                bt.memberId = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                                bt.dateFrom = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                                bt.dateTo = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                                bt.dateReturn = DateTime.Parse(dateTransReturn.Text).ToShortDateString();
                                bt.strTransRate = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                                bt.strFine = txtTransFine.Text;
                                bt.strLibBal = txtLibBal.Text;
                                bt.strMemBal = txtMemBal.Text;
                                bt.strRenewal = ds.Tables[0].Rows[0].ItemArray[10].ToString();
                                updateLoanReturn(bt, false, false);
                            }

                        }
                        bt.dateFrom = "";
                        bt.dateReturn = "";
                        bt.dateTo = "";
                        //LibBookTrans lbt = new LibBookTrans();
                        lbt.displayTrans(bt, gridTrans);
                    }
                }
                else
                {
                    if (dateTransReturn.Checked)
                        bt.dateReturn = DateTime.Parse(dateTransReturn.Text).ToShortDateString();
                    else
                    {
                        MessageBox.Show("Please specify a Return Date");
                        return;
                    }
                    if (MessageBox.Show("Books Returned:" + txtTransBookid.Text + "\nMemberID:" + txtTransMemId.Text + "\nLend Rate:" + txtTransLendRate.Text + "\nFine:" + txtTransFine.Text, "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bt.bookId = txtTransBookid.Text;
                        bt.memberId = txtTransMemId.Text;
                        bt.dateFrom = DateTime.Parse(dateTransLoan.Text).ToShortDateString();
                        bt.dateTo = DateTime.Parse(dateTransDue.Text).ToShortDateString();
                        bt.dateReturn = DateTime.Parse(dateTransReturn.Text).ToShortDateString();
                        bt.strTransRate = txtTransLendRate.Text;
                        bt.strFine = txtTransFine.Text;
                        bt.strLibBal = txtLibBal.Text;
                        bt.strMemBal = txtMemBal.Text;
                        bt.strRenewal = txtRenewal.Text;
                        updateLoanReturn(bt, false, false);

                        bt.dateFrom = "";
                        bt.dateReturn = "";
                        bt.dateTo = "";
                        //LibBookTrans lbt = new LibBookTrans();
                        lbt.displayTrans(bt, gridTrans);
                    }
                }
            }
        }

        private void btnShowMemTrans_Click(object sender, EventArgs e)
        {
            LibBookTrans lbt = new LibBookTrans();
            //System.Windows.Forms.Control.ControlCollection
            if (txtMembers.Text == "")
            {
                MessageBox.Show("Please select a MemberId");
                return;
            }
            mainTab.SelectedTab = pageTrans;
            bookTrans bt = new bookTrans();
            bt.memberId = txtMembers.Text;//lstMembers.SelectedValue.ToString();
            lbt.displayTrans(bt, gridTrans);

        }

       


        private void gridTrans_RowContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataSet ds = new DataSet();
                LibBookTrans lbt = new LibBookTrans();
                txtTransBookid.Text = gridTrans[1, e.RowIndex].Value.ToString();
                txtTransMemId.Text = "";
                txtTransMemId.Text = gridTrans[2, e.RowIndex].Value.ToString();
                lblTransId.Text = gridTrans[6, e.RowIndex].Value.ToString();
                setTransMemName();
                setBookLabel();
                //select BookID,MemberID,LendDate,DueDate,ReturnDate,LendRate,Fine,Balance,booktrans.TransID,RenewalDays from booktrans where BookID='" + bookId + "' and memberid='"+memberID;
                ds = lbt.returnLoanDetails(Int32.Parse(lblTransId.Text.ToString()));
                dateTransLoan.Text=ds.Tables[0].Rows[0].ItemArray[2].ToString();
                //dateTransLoan.Text = gridTrans[3, e.RowIndex].Value.ToString();
                dateTransDue.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                dateTransReturn.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                txtTransLendRate.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                txtTransFine.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                txtLibBal.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();                
                txtMemBal.Text = ds.Tables[0].Rows[0].ItemArray[8].ToString();                
                txtRenewal.Text = ds.Tables[0].Rows[0].ItemArray[10].ToString();
            }
        }

        //private void lstTransMemId_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    foreach (DataRow rw in dsMemberList.Tables[0].Rows)
        //    {
        //        if (rw[0].ToString().Equals(lstTransMemId.SelectedValue.ToString()))
        //        {
        //            txtTransMemName.Text = rw[1].ToString();
        //            break;
        //        }
        //    }
        //}

        private void btnSearchDate_Click(object sender, EventArgs e)
        {
            bookTrans bt = new bookTrans();
            bt.dateFrom = dateFrom.Text;
            bt.dateTo = dateTo.Text;
            LibBookTrans lbt = new LibBookTrans();
            lbt.displayTrans(bt, gridTrans);
        }

        private void btnAddLoan_Click(object sender, EventArgs e)
        {
            DataSet dsTemp = new DataSet();
            //gridTrans.Columns.Add("LendRate", "LendRate");
            dsTemp = (DataSet)gridTrans.DataSource;
            //gridTrans.Rows.Insert(5,rw);     
            if (!(dsTemp.Tables[0].Columns.Contains("Fine")))
            {
                dsTemp.Tables[0].Columns.Add("Lendrate");
                dsTemp.Tables[0].Columns.Add("Fine");
                dsTemp.Tables[0].Columns.Add("LibBal");
                dsTemp.Tables[0].Columns.Add("MemBal");
               // dsTemp.Tables[0].Columns.Add("TransId");
                dsTemp.Tables[0].Columns.Add("RenewalDays");
            }
            DataRow rw = dsTemp.Tables[0].NewRow();
            rw["BookID"] = txtTransBookid.Text;
            rw["MemberID"] = txtTransMemId.Text;
            rw["LendDate"] = dateTransLoan.Text;
            rw["DueDate"] = dateTransDue.Text;
            if (dateTransReturn.Checked)
                rw["ReturnDate"] = dateTransReturn.Text;
            else
                rw["ReturnDate"] = DateTime.MinValue;

            
            rw["LendRate"] = txtTransLendRate.Text;
            rw["Fine"] = txtTransFine.Text;
            rw["LibBal"] = txtLibBal.Text;
            rw["MemBal"] = txtMemBal.Text;
            rw["TransId"] = lblTransId.Text;
            rw["RenewalDays"] = txtRenewal.Text;
            dsTemp.Tables[0].Rows.Add(rw);
            
            gridTrans.DataSource = dsTemp;

        }


        private void btnTransClear_Click(object sender, EventArgs e)
        {
            int rowCount = gridTrans.RowCount - 1;
            for (int i = rowCount; i >= 0; i--)
                gridTrans.Rows.Remove(gridTrans.Rows[i]);
        }

        private void leave_MemberID(object sender, EventArgs e)
        {
            setMemName();
        }

        public void setMemName()
        {
            bool bMemAvail = false;
            foreach (DataRow rw in dsMemberList.Tables[0].Rows)
            {
                if (rw[0].ToString().Equals(txtMembers.Text))
                {
                    txtMemberName.Text = rw[1].ToString();
                    bMemAvail = true;
                    break;
                }
            }
            if (!bMemAvail)
                txtMemberName.Text = "";
        }

        private void leave_MemId_Trans(object sender, EventArgs e)
        {
            if (!setTransMemName())
                txtTransMemName.Text = "";
        }

        public bool setTransMemName()
        {
            bool bMemAvail = false;
            foreach (DataRow rw in dsMemberList.Tables[0].Rows)
            {
                if (rw[0].ToString().Equals(txtTransMemId.Text))
                {
                    txtTransMemName.Text = rw[1].ToString();
                    String strQuery = "select Mobile from members where memberid='" + rw[0].ToString() + "'";
                    lblPhone.Text=returnString(strQuery,"members");
                    //strQuery = "select sum(LibBal) from booktrans where memberid='" + rw[0].ToString() + "' and returndate is null";
                   // lblLibBalAmt.Text = returnString(strQuery, "booktrans").ToString();
                    bMemAvail = true;
                    break;
                }
            }
            return bMemAvail;
        }

        private void btnTransUpdate_Click(object sender, EventArgs e)
        {
            String strQuery;
            if (dateTransReturn.Checked)
                strQuery = "update booktrans set LendRate=" + txtTransLendRate.Text + ",Fine=" + txtTransFine.Text + ",LibBal=" + txtLibBal.Text + ",MemBal=" + txtMemBal.Text + ",RenewalDays=" + txtRenewal.Text + ",ReturnDate='" + dateTransReturn.Text + "',LendDate='" + dateTransLoan.Text + "',DueDate='" + dateTransDue.Text + "',bookid='" + txtTransBookid.Text + "',memberid='" + txtTransMemId.Text + "' where TransID=" + lblTransId.Text;
            else
                strQuery = "update booktrans set LendRate=" + txtTransLendRate.Text + ",Fine=" + txtTransFine.Text + ",LibBal=" + txtLibBal.Text + ",MemBal=" + txtMemBal.Text + ",RenewalDays=" + txtRenewal.Text + ",ReturnDate=null,LendDate='" + dateTransLoan.Text + "',DueDate='" + dateTransDue.Text + "',bookid='" + txtTransBookid.Text + "',memberid='" + txtTransMemId.Text + "' where TransID=" + lblTransId.Text;

            InsertUpdateQuery(strQuery);
            bookTrans bt = new bookTrans();
            //bt.memberId=txtTransMemId.Text;
            txtSearch_trans.Text = txtTransMemId.Text;

            LibBookTrans lbt = new LibBookTrans();
            lbt.displayTrans(bt, gridTrans);
        }    

        #endregion Transaction Page

        #region Members Page
        private void initializeMemPage()
        {
            LibMembers lm = new LibMembers();
            lm.displayMembers(gridMembers);
            initializeMemDetails();

        }
        private void initializeMemDetails()
        {
            txtMemMemberid.Text = gridMembers[0, 0].Value.ToString();
            txtMemMemName.Text = gridMembers[1, 0].Value.ToString();
            txtAddress.Text = gridMembers[2, 0].Value.ToString();
            dateDoj.Text = gridMembers[3, 0].Value.ToString();
            txtMemNotes.Text = gridMembers[4, 0].Value.ToString();
            if (Int32.Parse(gridMembers[5, 0].Value.ToString()) == 0)
                lstStatus.SelectedIndex = 2;
            else
                lstStatus.SelectedIndex = 1;
            txtMemMob.Text = gridMembers[6, 0].Value.ToString();
            txtMemland.Text = gridMembers[7, 0].Value.ToString();
            txtEmail.Text = gridMembers[8, 0].Value.ToString();
            switch (gridMembers[9, 0].Value.ToString())
            {
                case "1":
                    lstMemType.SelectedIndex = 1;
                    break;
                case "2":
                    lstMemType.SelectedIndex = 2;
                    break;
                case "3":
                    lstMemType.SelectedIndex = 3;
                    break;
                case "4":
                    lstMemType.SelectedIndex = 4;
                    break;
                case "5":
                    lstMemType.SelectedIndex = 1;
                    break;
                default: lstMemType.SelectedIndex = 0; break;
            }
            txtMemAmt.Text = gridMembers[10, 0].Value.ToString();
        }

        

        private void gridMem_RowContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtMemMemberid.Text = gridMembers[0, e.RowIndex].Value.ToString();
                //int mem = Int32.Parse(txtMemMemberid.Text.Substring(1).ToString());
                txtMemMemName.Text = gridMembers[1, e.RowIndex].Value.ToString();
                txtAddress.Text = gridMembers[2, e.RowIndex].Value.ToString();
                dateDoj.Text = gridMembers[3, e.RowIndex].Value.ToString();
                txtMemNotes.Text = gridMembers[4, e.RowIndex].Value.ToString();
                if (Int32.Parse(gridMembers[5, e.RowIndex].Value.ToString()) == 0)
                    lstStatus.SelectedIndex = 2;
                else
                    lstStatus.SelectedIndex = 1;
                txtMemMob.Text = gridMembers[6, e.RowIndex].Value.ToString();
                txtMemland.Text = gridMembers[7, e.RowIndex].Value.ToString();
                txtEmail.Text = gridMembers[8, e.RowIndex].Value.ToString();
                switch (gridMembers[9, e.RowIndex].Value.ToString())
                {
                    case "1":
                        lstMemType.SelectedIndex = 1;
                        break;
                    case "2":
                        lstMemType.SelectedIndex = 2;
                        break;
                    case "3":
                        lstMemType.SelectedIndex = 3;
                        break;
                    case "4":
                        lstMemType.SelectedIndex = 4;
                        break;
                    case "5":
                        lstMemType.SelectedIndex = 1;
                        break;
                    default: lstMemType.SelectedIndex = 0; break;
                }
                txtMemAmt.Text = gridMembers[10, e.RowIndex].Value.ToString();

            }
        }

        private void btnAddMem_Click(object sender, EventArgs e)
        {
            String strValBook = "select count(*) from members where memberid='" + txtMemMemberid.Text + "'";
            int intCnt = returnInt(strValBook, "books");
            if (intCnt > 0)
            {
                MessageBox.Show("MemberID already exists");
                return;
            }
            else
            {
                int intStatus = 0;
                if (lstStatus.SelectedItem.ToString() == "Active")
                    intStatus = 1;
                String strMemType = lstMemType.SelectedItem.ToString();
                int intMemType = 0;
                switch (strMemType)
                {
                    case "Budget":
                        intMemType = 1;
                        break;
                    case "Children":
                        intMemType = 2;
                        break;
                    case "Family":
                        intMemType = 3;
                        break;
                    case "Individual":
                        intMemType = 4;
                        break;
                    case "Unlimited":
                        intMemType = 1;
                        break;
                    default: lstMemType.SelectedIndex = 0; break;
                }
                //String strQuery = "update members set MemberName='" + txtMemMemName.Text + "',Address='" + txtAddress.Text + "',notes='" + txtMemNotes.Text + "',doj='" + dateDoj.Text + "',Status=" + intStatus + ",Mobile='" + txtMemMob.Text + "',Landline='" + txtMemland.Text + "',MemberType=" + intMemType + ",Amount=" + txtMemAmt.Text + " where memberid='" + txtMemMemberid.Text + "' ";
                String strQuery = "declare @memid int;select @memid=max(id)+1 from members;insert into members(ID,MemberID,MemberName,Address,Notes,Doj,Status,Mobile,Landline,Email,MemberType,Amount,SortID) values(@memid,'" + txtMemMemberid.Text + "','" + txtMemMemName.Text + "','" + txtAddress.Text + "','" + txtMemNotes.Text + "','" + dateDoj.Text + "'," + intStatus + ",'" + txtMemMob.Text + "','" + txtMemland.Text + "','" + txtEmail.Text + "'," + intMemType + "," + txtMemAmt.Text + "," + Int32.Parse(txtMemMemberid.Text.Substring(1)) + ")";
                InsertUpdateQuery(strQuery);
                LibMembers lm = new LibMembers();
                lm.displayMembers(gridMembers);
            }
        }

        private void btnDelMem_Click(object sender, EventArgs e)
        {
            String strValBook = "select count(*) from members where memberid='" + txtMemMemberid.Text + "'";
            int intCnt = returnInt(strValBook, "members");
            if (intCnt > 0)
            {
                String strQuery = "delete from members where memberid='" + txtMemMemberid.Text + "'";
                InsertUpdateQuery(strQuery);
                LibMembers lm = new LibMembers();
                lm.displayMembers(gridMembers);
            }
            else
            {
                MessageBox.Show("MemberID doesn't exists");
                return;
            }

        }

        private void btnClearMem_Click(object sender, EventArgs e)
        {
            txtMemMemberid.Text = "";
            txtMemMemName.Text = "";
            txtAddress.Text = "";
            lstStatus.SelectedIndex = 0;
            txtMemMob.Text = "";
            txtMemland.Text = "";
            txtEmail.Text = "";
            lstMemType.SelectedIndex = 0;
            txtMemAmt.Text = "";
            txtMemNotes.Text = "";
        }

        private void btnMemInsert_Click(object sender, EventArgs e)
        {
            int intStatus = 0;
            if (lstStatus.SelectedItem.ToString() == "Active")
                intStatus = 1;
            String strMemType = lstMemType.SelectedItem.ToString();
            int intMemType = 0;
            switch (strMemType)
            {
                case "Budget":
                    intMemType = 1;
                    break;
                case "Children":
                    intMemType = 2;
                    break;
                case "Family":
                    intMemType = 3;
                    break;
                case "Individual":
                    intMemType = 4;
                    break;
                case "Unlimited":
                    intMemType = 1;
                    break;
                default: lstMemType.SelectedIndex = 0; break;
            }
            String strQuery = "update members set MemberName='" + txtMemMemName.Text + "',Address='" + txtAddress.Text + "',notes='" + txtMemNotes.Text + "',doj='" + dateDoj.Text + "',Status=" + intStatus + ",Mobile='" + txtMemMob.Text + "',Landline='" + txtMemland.Text + "',Email='" + txtEmail.Text + "',MemberType=" + intMemType + ",Amount=" + txtMemAmt.Text + " where memberid='" + txtMemMemberid.Text + "' ";
            InsertUpdateQuery(strQuery);
            LibMembers lm = new LibMembers();
            lm.displayMembers(gridMembers);

        }

        private void btnMemSearch_Click(object sender, EventArgs e)
        {
            LibMembers lm = new LibMembers();
            lm.Mem_Search(gridMembers,txtMemSearch.Text);
        }        

        private void Mem_Search(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                LibMembers lm = new LibMembers();
                lm.Mem_Search(gridMembers, txtMemSearch.Text);
            }              
        }

        private void lstNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNumbers.SelectedItem != null)
            {
                int iCount = Int32.Parse(lstNumbers.SelectedItem.ToString());
                String strQuery = "select MemberID,MemberName,Address,Doj,Notes,Status,Mobile,Landline,Email,MemberType,Amount from members where sortid between " + (iCount * 20 - 20+1) + " and " + (iCount * 20) + " order by sortid asc";

                DataSet ds = new DataSet();
                ds = returnDS(strQuery, "members");


                gridMembers.DataSource = ds;
                gridMembers.DataMember = "members";
            }
        }

        private void btnMemAll_Click(object sender, EventArgs e)
        {
            LibMembers lm = new LibMembers();
            lm.displayMembers(gridMembers);
        }
        

        private void btnAccGo_Click(object sender, EventArgs e)
        {
            String strQuery = "SELECT sum(lendrate),sum(fine),sum(LibBal) FROM booktrans where lenddate between '" + dateAccStr.Text + "' and '" + dateAcc.Text + "'";

            DataSet ds = new DataSet();
            ds = returnDS(strQuery, "booktrans");
            lblAmtTrans.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            lblAmtFine.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            lblAccAmtBal.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
        }

        


        private void lstAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblBkPrice_Click(object sender, EventArgs e)
        {

        }

        private void txtBookPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblLendRate_Click(object sender, EventArgs e)
        {

        }

        private void txtLendrate_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblAuthor_Click(object sender, EventArgs e)
        {

        }

        private void txtLibBal_TextChanged(object sender, EventArgs e)
        {

        }

        
        

           

        

        //private void lstTransMemId_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    foreach (DataRow rw in dsMemberList.Tables[0].Rows)
        //    {
        //        if (rw[0].ToString().Equals(lstTransMemId.SelectedValue.ToString()))
        //        {
        //            txtTransMemName.Text = rw[1].ToString();
        //            break;
        //        }
        //    }
        //}                  
        #endregion Members Page

        #region Common Functions
        public DataSet returnDS(String strQuery, String strTable)
        {
            DataSet ds = new DataSet();
            //String connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\\Bookmark\\Datasource\\Bookmark.mdb'";
            String connStr = System.Configuration.ConfigurationSettings.AppSettings["connStr"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter dataAdap = new  SqlDataAdapter(strQuery, conn);
            //OleDbConnection conn = new OleDbConnection(connStr);
            //OleDbDataAdapter dataAdap = new OleDbDataAdapter(strQuery, conn);
            dataAdap.Fill(ds, strTable);
            conn.Close();
            return ds;
        }

        public DataSet returnDisplayTransDS(String strQuery, String strTable, bookTrans bt)
        {
            DataSet ds = new DataSet();
            //String connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\\Bookmark\\Datasource\\Bookmark.mdb'";
            String connStr = System.Configuration.ConfigurationSettings.AppSettings["connStr"].ToString();
            //OleDbConnection conn = new OleDbConnection(connStr);
            //OleDbDataAdapter dataAdap = new OleDbDataAdapter(strQuery, conn);
            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter dataAdap = new SqlDataAdapter(strQuery, conn);
            dataAdap.SelectCommand.Parameters.Add("@fromDate", OleDbType.Date);
            dataAdap.SelectCommand.Parameters.Add("@fromTo", OleDbType.Date);
            dataAdap.SelectCommand.Parameters["@fromDate"].Value = bt.dateFrom;
            dataAdap.SelectCommand.Parameters["@fromTo"].Value = bt.dateTo;

            dataAdap.Fill(ds, strTable);
            conn.Close();
            return ds;
        }

        public int returnInt(String strQuery, String strTable)
        {
            int intRs = 0;
            //String connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\\Bookmark\\Datasource\\Bookmark.mdb'";
            String connStr = System.Configuration.ConfigurationSettings.AppSettings["connStr"].ToString();
            //OleDbConnection conn = new OleDbConnection(connStr);
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(strQuery, conn);
            //OleDbCommand cmd = new OleDbCommand(strQuery, conn);
            intRs = Int32.Parse(cmd.ExecuteScalar().ToString());
            conn.Close();
            return intRs;
        }
        public String returnString(String strQuery, String strTable)
        {
            String returnRs = "";
            //String connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\\Bookmark\\Datasource\\Bookmark.mdb'";
            String connStr = System.Configuration.ConfigurationSettings.AppSettings["connStr"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(strQuery, conn);
            returnRs = cmd.ExecuteScalar().ToString();
            conn.Close();
            return returnRs;
        }

        public void InsertUpdateQuery(String strQuery)
        {
            String connStr = System.Configuration.ConfigurationSettings.AppSettings["connStr"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(strQuery, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void mainTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = mainTab.SelectedIndex;

            switch (indx)
            {
                case 0: initializeMain();
                    break;
                case 1: initializeTransPage();
                    break;
                case 2: initializeMemPage();
                    break;

            }

        }

        private void setLstValue(ComboBox lstItems, String strValue)
        {
            int i = 0;
            //System.Data.DataRowView dr = new DataRowView();
            if (strValue.Equals(""))
                lstItems.SelectedIndex = 0;

            foreach (DataRowView strTemp in lstItems.Items)
            {
                if (strTemp.Row[1].ToString().Equals(strValue))
                    lstItems.SelectedIndex = i;
                i++;
            }
        }

        #endregion Common Functions

        private void gridTrans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

       

    }
}
