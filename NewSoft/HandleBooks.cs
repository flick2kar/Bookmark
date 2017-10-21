using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Utils;
using Entities;

namespace NewSoft
{
    public partial class frmLibSoft
    {
        AutoCompleteStringCollection autoAuthors = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoSubCategory = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoSeries = new AutoCompleteStringCollection();
        
        public void DisplayBookDetails()
        {
            lstAlphas.SelectedIndex = 0;
            gridBooks.DataSource = bookRep.GetBookDetails();            
        }

        public void DisplayAuthDetails()
        {
            DataTable ds = new DataTable();
            ds = bookRep.GetAuthorDetails();
            ds = cmMethods.InitializeList(ds);
            lstAuthor.DataSource = ds;
            lstAuthor.DisplayMember = "Authorname";
            lstAuthor.ValueMember = "AuthorId";
        }

        private void DisplaySeriesDetails()
        {
            DataTable ds = new DataTable();
            ds = bookRep.GetSeriesDetails();
            ds = cmMethods.InitializeList(ds);
            lstSeries.DataSource = ds;
            lstSeries.DisplayMember = "Seriesname";
            lstSeries.ValueMember = "SeriesId";
        }

        private void DisplayCategoryDetails(String srcPage)
        {
            DataTable ds = new DataTable();
            ds = bookRep.GetCategoryDetails();
            ds = cmMethods.InitializeList(ds);

            if (srcPage.Equals("Main"))
            {
                lstCategory.DataSource = ds;
                lstCategory.DisplayMember = "CategoryName";
                lstCategory.ValueMember = "CategoryID";
            }            
        }

        private void DisplaySubCategoryDetails(String srcPage)
        {
            DataTable ds = new DataTable();
            ds = bookRep.GetSubCategoryDetails();
            ds = cmMethods.InitializeList(ds);

            if (srcPage.Equals("Main"))
            {
                lstSubcat.DataSource = ds;
                lstSubcat.DisplayMember = "Name";
                lstSubcat.ValueMember = "Id";
            }
        }

        private void InitializeData(string bookID)
        {            
            if (gridBooks.RowCount > 0)
            {
                lstAuthor.SelectedIndex = 0;
                lstCategory.SelectedIndex = 0;
                lstSeries.SelectedIndex = 0;
                txtMembers.Text = "";
                //lstAlphas.SelectedIndex = 0;
                txtBookId.Text = bookID;
                tempBookid = txtBookId.Text;
                
                DataTable ds = new DataTable();
                ds = bookRep.GetBookIDDetails(tempBookid);
                txtBookName.Text = ds.Rows[0]["BookName"].ToString();
                if (transRep.GetTransLoanDetails(tempBookid).Rows.Count > 0)
                    lblBookLendTo.Text = "Book Loaned To:" + transRep.GetTransLoanDetails(tempBookid).Rows[0]["MemberID"].ToString();
                else
                    lblBookLendTo.Text = "";
                CommonUIMethods.setLstValue(lstAuthor, ds.Rows[0]["Authorname"].ToString());
                txtBookPrice.Text = ds.Rows[0]["BookPrice"].ToString();
                txtOrgprice.Text = ds.Rows[0]["OrgPrice"].ToString();
                CommonUIMethods.setLstValue(lstCategory, ds.Rows[0]["CategoryName"].ToString());
                CommonUIMethods.setLstValue(lstSubcat, ds.Rows[0]["Name"].ToString());
                CommonUIMethods.setLstValue(lstSeries, ds.Rows[0]["SeriesName"].ToString());               
                
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

        private void Book_Search(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                gridBooks.DataSource = bookRep.Book_Search(txtSearch.Text);
            }

        }

        #region Events Only
        private void gridBooks_RowContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (gridBooks.Columns.Contains("WishID"))
                    initializeWishListDetails(Int32.Parse(gridBooks["WishID",e.RowIndex].Value.ToString()));
                else
                    InitializeData(gridBooks[0, e.RowIndex].Value.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            gridBooks.DataSource = bookRep.Book_Search(txtSearch.Text);

        }

        private void btnGetall_Click(object sender, EventArgs e)
        {            
            DisplayBookDetails();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CommonUIMethods.ClearControl(this);
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {            

            if (txtBookId.Text == "" || txtBookName.Text == "" || lstCategory.SelectedIndex == 0 || txtBookPrice.Text == "" || txtOrgprice.Text == "")
            {
                MessageBox.Show("BookId/BookName/Category/Book Price/Lend Rate cannot be empty");
                return;
            }

            if (bookRep.GetBookIDDetails(txtBookId.Text).Rows.Count > 0)
            {
                MessageBox.Show("BookId already exists");
                return;
            }
            else
            {
                book objBook = new book();
                objBook.AuthorID = Int32.Parse(lstAuthor.SelectedValue.ToString());
                objBook.BookID = txtBookId.Text;
                objBook.BookName = txtBookName.Text;
                objBook.BookPrice = Int32.Parse(txtBookPrice.Text);
                objBook.CategoryID = Int32.Parse(lstCategory.SelectedValue.ToString());
                objBook.CreatedDate = DateTime.Now;
                objBook.LendRate = 0;
                objBook.OrgPrice= Int32.Parse(txtOrgprice.Text);
                objBook.SeriesID = Int32.Parse(lstSeries.SelectedValue.ToString());
                objBook.SubCategoryID = Int32.Parse(lstSubcat.SelectedValue.ToString());
                bookRep.InsertBooks(objBook);
                gridBooks.DataSource = bookRep.GetBookDetails();            
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtBookId.Text == "" || txtBookName.Text == "" || lstCategory.SelectedIndex == 0 || txtBookPrice.Text == "" || txtOrgprice.Text == "")
            {
                MessageBox.Show("BookId/BookName/Category/Book Price/Lend Rate cannot be empty");
                return;
            }
            if (bookRep.GetBookIDDetails(txtBookId.Text).Rows.Count <= 0)
            {
                MessageBox.Show("BookId doesn't exists");
                return;
            }
            else
            {
                book objBook = new book();
                String createdDate = bookRep.GetBookIDDetails(txtBookId.Text).Rows[0]["CreatedDate"].ToString();
                objBook.AuthorID = Int32.Parse(lstAuthor.SelectedValue.ToString());
                objBook.BookID = txtBookId.Text;
                objBook.BookName = txtBookName.Text;
                objBook.BookPrice = Int32.Parse(txtBookPrice.Text);
                objBook.CategoryID = Int32.Parse(lstCategory.SelectedValue.ToString());
                if (!String.IsNullOrEmpty(createdDate))
                    objBook.CreatedDate = DateTime.Parse(createdDate);
                else
                    objBook.CreatedDate = DateTime.Now;
                objBook.LendRate = 0;
                objBook.OrgPrice = Int32.Parse(txtOrgprice.Text);
                objBook.SeriesID = Int32.Parse(lstSeries.SelectedValue.ToString());
                objBook.SubCategoryID = Int32.Parse(lstSubcat.SelectedValue.ToString());;
                lstAuthor.SelectedText = lstAuthor.Text;
                bookRep.UpdateBooks(objBook, txtBookId.Text);
                gridBooks.DataSource = bookRep.GetBookDetails();            
            }
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (txtBookId.Text.Equals(""))
            {
                MessageBox.Show("BookId cannot be empty");
            }

            if (bookRep.GetBookIDDetails(txtBookId.Text).Rows.Count <= 0)
            {
                MessageBox.Show("BookId doesn't exists");
                return;
            }
            else
            {
                if (MessageBox.Show("Are you sure do you want to delete this book?", "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bookRep.DeleteBooks(txtBookId.Text);
                    gridBooks.DataSource = bookRep.GetBookDetails();            
                   
                }
            }            
        }

        private void btnAddAuth_Click(object sender, EventArgs e)
        {
            author objAuth = new author();
            String strAuthor = lstAuthor.Text;            
            if (bookRep.GetAuthorDetails(strAuthor).Rows.Count > 0)
            {
                MessageBox.Show("Author already present");
                return;
            }
            objAuth.AuthorID = bookRep.GetMaxID("authors", "authorid")+1;
            objAuth.AuthorName = strAuthor;

            bookRep.InsertAuthors(objAuth);
            lstAuthor.DataSource = bookRep.GetAuthorDetails();
            lstAuthor.DisplayMember = "Authorname";
            lstAuthor.ValueMember = "AuthorId";
            //setLstValue(lstAuthor, strAuthor);
            lstAuthor.Text = strAuthor;
            autoAuthors.Clear();
            foreach (DataRowView dr in lstAuthor.Items)
                autoAuthors.Add(dr.Row[1].ToString());
            lstAuthor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lstAuthor.AutoCompleteCustomSource = autoAuthors;
        }

        private void btnSubCatAdd_Click(object sender, EventArgs e)
        {
            SubCategory objSubCat = new SubCategory();
            String strSubcat = lstSubcat.Text;
            if (bookRep.GetSubCategoryDetails(strSubcat).Rows.Count > 0)
            {
                MessageBox.Show("Sub-Category already present");
                return;
            }
            objSubCat.Id = bookRep.GetMaxID("SubCategory", "id") + 1;
            objSubCat.Name = strSubcat;
            bookRep.InsertSubCat(objSubCat);

            lstSubcat.DataSource = bookRep.GetSubCategoryDetails();
            lstSubcat.DisplayMember = "Name";
            lstSubcat.ValueMember = "Id";
            lstSubcat.Text = strSubcat;
            
            autoSubCategory.Clear();

            foreach (DataRowView dr in lstSubcat.Items)
                autoSubCategory.Add(dr.Row[1].ToString());

            lstSubcat.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lstSubcat.AutoCompleteCustomSource = autoSubCategory;

        }

        private void btnDelAuth_Click(object sender, EventArgs e)
        {
            Utils.CommonMethods cmUI = new CommonMethods();
            if (bookRep.GetAuthorDetails(lstAuthor.Text).Rows.Count <= 0)
            {
                MessageBox.Show("Author doesn't exists");
                return;
            }
            else
            {
                bookRep.DeleteAuthors(Int32.Parse(lstAuthor.SelectedValue.ToString()));
                lstAuthor.DataSource = cmUI.InitializeList(bookRep.GetAuthorDetails());
                lstAuthor.DisplayMember = "Authorname";
                lstAuthor.ValueMember = "AuthorId";
                autoAuthors.Clear();
                foreach (DataRowView dr in lstAuthor.Items)
                    autoAuthors.Add(dr.Row[1].ToString());
                lstAuthor.AutoCompleteSource = AutoCompleteSource.CustomSource;
                lstAuthor.AutoCompleteCustomSource = autoAuthors;
            }
        }

        private void btnSubCatDel_Click(object sender, EventArgs e)
        {
            Utils.CommonMethods cmUI = new CommonMethods();
            
            if (bookRep.GetSubCategoryDetails(lstSubcat.Text).Rows.Count <= 0)
            {
                MessageBox.Show("Sub Category doesn't exists");
                return;
            }
            else
            {
                bookRep.DeleteSubCat(Int32.Parse(lstSubcat.SelectedValue.ToString()));
                lstSubcat.DataSource =  cmUI.InitializeList(bookRep.GetSubCategoryDetails());
                lstSubcat.DisplayMember = "Name";
                lstSubcat.ValueMember = "Id";
                autoSubCategory.Clear();

                foreach (DataRowView dr in lstSubcat.Items)
                    autoSubCategory.Add(dr.Row[1].ToString());

                lstSubcat.AutoCompleteSource = AutoCompleteSource.CustomSource;
                lstSubcat.AutoCompleteCustomSource = autoSubCategory;
            }

        }

        private void btnSeriesAdd_Click(object sender, EventArgs e)
        {
            String strSeries = lstSeries.Text;
            Series objSeries = new Series();
            Utils.CommonMethods cmMethod = new CommonMethods();
            if (bookRep.GetSeriesDetails(strSeries).Rows.Count > 0)
            {
                MessageBox.Show("Series already present");
                return;
            }

            objSeries.SeriesName = strSeries;
            bookRep.InsertSeries(objSeries);

            lstSeries.DataSource = cmMethod.InitializeList(bookRep.GetSeriesDetails());
            lstSeries.DisplayMember = "Seriesname";
            lstSeries.ValueMember = "SeriesId";            
            
            CommonUIMethods.setLstValue(lstSeries, strSeries);
            autoSeries.Clear();


            foreach (DataRowView dr in lstSeries.Items)
                autoSeries.Add(dr.Row[1].ToString());

            lstSeries.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lstSeries.AutoCompleteCustomSource = autoSubCategory;
        }

        private void btnSeriesDel_Click(object sender, EventArgs e)
        {
            Series objSeries = new Series();
            Utils.CommonMethods cmMethod = new CommonMethods();
            if (bookRep.GetSeriesDetails(lstSeries.Text).Rows.Count <= 0)
            {
                MessageBox.Show("Series doesn't exists");
                return;
            }
            else
            {
                bookRep.DeleteSeries(Int32.Parse(lstSeries.SelectedValue.ToString()));
                lstSeries.DataSource = cmMethod.InitializeList(bookRep.GetSeriesDetails());
                lstSeries.DisplayMember = "SeriesName";
                lstSeries.ValueMember = "SeriesId";
                lstSeries.SelectedValue = 0;
                autoSeries.Clear();

                foreach (DataRowView dr in lstSeries.Items)
                    autoSeries.Add(dr.Row[1].ToString());

                lstSeries.AutoCompleteSource = AutoCompleteSource.CustomSource;
                lstSeries.AutoCompleteCustomSource = autoSeries;
            }
        }

        private void lstAlphas_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridBooks.DataSource = bookRep.GetBookDetails(lstAlphas.SelectedItem.ToString());
            //String strQuery = "select BookID,BookName,Authorname,BookPrice,LendRate,CategoryName,SubCategory.Name from ((books left join authors on books.authorid=authors.authorid) inner join category on category.CategoryID=books.CategoryID) Left join Subcategory on Subcategory.ID=books.SubCategoryID where bookid like '" + lstAlphas.SelectedItem + "%' order by bookid asc";

            //DataSet ds = new DataSet();
            //ds = returnDS(strQuery, "books");

            //gridBooks.DataSource = ds;
            //gridBooks.DataMember = "books";
        }

        #endregion 

    }
}
