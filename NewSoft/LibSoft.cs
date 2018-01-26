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
using DALLayer;
using Utils;

namespace NewSoft
{
    public partial class frmLibSoft : Form
    {
        BookRepository bookRep = new BookRepository();
        TransRepository transRep = new TransRepository();
        MemberRepository memRep = new MemberRepository();
        LibAccRepository accRep = new LibAccRepository();
        WishListRepository wishRep = new WishListRepository();
        CommonMethods cmMethods = new CommonMethods();
        String tempBookid = "";
        public DataSet dsMemberList = new DataSet();
        //public string MemberID { get {return txtTransMemId.Text; } }

        //LibBookSearch libBooks=new LibBookSearch(frmLibSoft);
        public frmLibSoft()
        {
            InitializeComponent();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializePage();
        }

        
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
            DisplayBookDetails();
            DisplayAuthDetails();            
            DisplaySeriesDetails();
            DisplayCategoryDetails("Main");
            DisplaySubCategoryDetails("Main");
            InitializeData(gridBooks[0, 0].Value.ToString());            
        }

        
        #endregion Initialize page
        #region Transaction Page
        private void initializeTransPage()
        {   
            DisplayTransDetails();            
            initializeTransData(Int32.Parse(gridTrans[6, 0].Value.ToString()),-1);                 
        }

        

               

        

        private void btnAll_Trans_Click(object sender, EventArgs e)
        {
            //String strQuery = "select top 100 BookID,MemberID,LendDate,DueDate,ReturnDate,booktrans.TransID from booktrans";
            //DataSet ds = new DataSet();
            //ds = returnDS(strQuery, "booktrans");
            //gridTrans.DataSource = ds;
            //gridTrans.DataMember = "booktrans";
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
            //bookTrans bt = new bookTrans();
            //bt.dateFrom = dateFrom.Text;
            //bt.dateTo = dateTo.Text;
            //LibBookTrans lbt = new LibBookTrans();
            //lbt.displayTrans(bt, gridTrans);
        }

        


        

        private void leave_MemberID(object sender, EventArgs e)
        {
//setMemName();
        }

        //public void setMemName()
        //{
        //    bool bMemAvail = false;
        //    foreach (DataRow rw in dsMemberList.Tables[0].Rows)
        //    {
        //        if (rw[0].ToString().Equals(txtMembers.Text))
        //        {
        //            txtMemberName.Text = rw[1].ToString();
        //            bMemAvail = true;
        //            break;
        //        }
        //    }
        //    if (!bMemAvail)
        //        txtMemberName.Text = "";
        //}

        

        

        #endregion Transaction Page

        #region Members Page
        
    
        

        


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
       
        #endregion Common Functions

        private void gridTrans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkSetDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTransLoan.Text = DateTime.Today.ToShortDateString();
            dateTransDue.Text = DateTime.Today.AddDays(15).ToShortDateString();
            dateTransReturn.Checked = false;
        }

        private void btnAddBal_Click(object sender, EventArgs e)
        {
            var form = new AddBalance();
            form.Show();
        }

        private void btnAddFine_Click(object sender, EventArgs e)
        {
            var form = new AddFine();
            form.Show();
        }

        
    }
}
