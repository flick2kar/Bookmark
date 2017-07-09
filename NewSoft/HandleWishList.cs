using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Entities;

namespace NewSoft
{
    public partial class frmLibSoft
    {
        private void initializeWishList()
        {
            DataTable dt = new DataTable();
            dt = wishRep.GetWishList();            
            gridBooks.DataSource = dt;
            //initializeMemDetails(Int32.Parse(gridMembers["ID", 0].Value.ToString()));

        }

        private void initializeWishListDetails(int wishID)
        {
            DataTable ds=new DataTable();
            ds=wishRep.GetWishListID(wishID);
            CommonUIMethods.ClearControl(this);
            txtMembers.Text=ds.Rows[0]["MemberID"].ToString();
            txtBookId.Text=ds.Rows[0]["BookID"].ToString();
            txtBookName.Text = ds.Rows[0]["BookName"].ToString();
            if (ds.Rows[0]["AuthorName"].ToString() == "--Select--")
                lstAuthor.SelectedIndex = 0;
            else
                lstAuthor.Text = ds.Rows[0]["AuthorName"].ToString();
            chkBookGiven.Checked = bool.Parse(ds.Rows[0]["Status"].ToString());
            lblWishID.Text = wishID.ToString();
            if (String.IsNullOrEmpty(ds.Rows[0]["GrantDate"].ToString()))
            {
                dateGrantBook.Text = DateTime.Now.ToShortDateString();
                dateGrantBook.Checked = false;
            }
            else
            {
                dateGrantBook.Checked = true;
                dateGrantBook.Text = ds.Rows[0]["GrantDate"].ToString();
            }

        }

        private void btnShowWishList_Click(object sender, EventArgs e)
        {
            gridBooks.DataSource=wishRep.SearchWishList(txtSearch.Text);
            //initializeWishList();
        }

        private void btnGrantList_Click(object sender, EventArgs e)
        {
            gridBooks.DataSource = wishRep.GetGrantList(txtSearch.Text);
        }

        private void btnAddtoWishList_Click(object sender, EventArgs e)
        {  
            if (memRep.GetMemberIDDetails(txtMembers.Text).Rows.Count <= 0)
            {
                MessageBox.Show("MemberID doesn't exists");
                return;
            }
            WishList wsList = new WishList();
            wsList.MemberID = txtMembers.Text;
            wsList.BookID = txtBookId.Text;
            wsList.BookName = txtBookName.Text;
            if (lstAuthor.Text == "--Select--")
                wsList.AuthorName = "";
            else
                wsList.AuthorName = lstAuthor.Text;
            wsList.CreatedDate = DateTime.Now;            
            wsList.GrantDate = null;
            wsList.Status = false;
            wishRep.InsertWishList(wsList);
            initializeWishList();
        }

        private void btnUpdateWishList_Click(object sender, EventArgs e)
        {
            if (memRep.GetMemberIDDetails(txtMembers.Text).Rows.Count <= 0)
            {
                MessageBox.Show("MemberID doesn't exists");
                return;
            }
            WishList wsList = new WishList();
            wsList.MemberID = txtMembers.Text;
            wsList.BookID = txtBookId.Text;
            wsList.BookName = txtBookName.Text;
            if (lstAuthor.Text == "--Select--")
                wsList.AuthorName = "";
            else
                wsList.AuthorName = lstAuthor.Text;
            wsList.CreatedDate = DateTime.Now;
            wsList.Status = chkBookGiven.Checked ;
            if (dateGrantBook.Checked)
                wsList.GrantDate = DateTime.Parse(dateGrantBook.Text);
            if (!String.IsNullOrEmpty(lblWishID.Text))
                wishRep.UpdateWishList(wsList,Int32.Parse(lblWishID.Text));
            initializeWishList();
        }
    }
}
