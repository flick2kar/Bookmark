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
        public void DisplayTransDetails()
        {
            btnMemWish.Enabled = false;
            gridTrans.DataSource = transRep.GetTransDetails();
        }

        public void initializeTransData(int transID, int rowIndex)
        {
            rowIndex = transID>0? 0 : rowIndex < 0 ? 0 : rowIndex;
            if (gridTrans.Rows.Count > 0)
            {  
                DataTable ds = new DataTable();
                btnMemWish.Enabled = false;
                if (transID == 0)
                    ds = CommonUIMethods.ConvertGridToTable(gridTrans);
                else
                    ds = transRep.GetTransIDDetails(transID);
                lblMemberNotes.Text = memRep.GetMemberNotes(ds.Rows[rowIndex]["MemberID"].ToString());
                lblAlreadyRead.Text = "";
                txtTransBookid.Text = ds.Rows[rowIndex]["BookID"].ToString();
                txtTransMemId.Text = ds.Rows[rowIndex]["MemberID"].ToString();                
                setTransMemName();
                lblTransId.Text = ds.Rows[rowIndex]["TransID"].ToString();
                dateTransLoan.Text = ds.Rows[rowIndex]["LendDate"].ToString();
                dateTransDue.Text = ds.Rows[rowIndex]["DueDate"].ToString();
                if(String.Compare(DateTime.MinValue.ToString(),ds.Rows[rowIndex]["ReturnDate"].ToString())==0)
                    dateTransReturn.Text = "";
                else
                    dateTransReturn.Text = ds.Rows[rowIndex]["ReturnDate"].ToString();
                txtTransLendRate.Text = ds.Rows[rowIndex]["LendRate"].ToString();

                //logic to calculate and update fine
                //if(rowIndex<0)
                //    txtTransFine.Text = ds.Rows[0]["Fine"].ToString();
                //else
                //    txtTransFine.Text = gridTrans["Fine", rowIndex].Value.ToString();
                
                txtRenewal.Text = ds.Rows[rowIndex]["RenewalDays"].ToString();                
                CommonUIMethods.setBookLabel(lblBookLabel,txtTransBookid.Text);
                if (wishRep.SearchWishList(txtTransMemId.Text).Rows.Count > 0)
                    btnMemWish.Enabled = true;
            }
        }
        

        private void leaveBookID(object sender, EventArgs e)
        {
            CommonUIMethods.setBookLabel(lblBookLabel, txtTransBookid.Text);
            CommonUIMethods.setLendRate(txtTransLendRate, txtTransBookid.Text);
        }

        #region TransEvents
        private void gridTrans_RowContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                initializeTransData(Int32.Parse(gridTrans["TransID", e.RowIndex].Value.ToString()),e.RowIndex);
            }
        }

        private void btnSearch_trans_Click(object sender, EventArgs e)
        {
            gridTrans.DataSource = transRep.GetTransDetails(txtSearch_trans.Text);            
            
        }

        private void Trans_Search(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                gridTrans.DataSource = transRep.GetTransDetails(txtSearch_trans.Text);            
            }
        }

        private void btnLoan_Click(object sender, EventArgs e)
        {
            gridTrans.DataSource = transRep.GetTransLoanDetails(txtSearch_trans.Text);
        }

        private void btnTransClear_Click(object sender, EventArgs e)
        {   
            int rowCount = gridTrans.RowCount - 1;
            for (int i = rowCount; i >= 0; i--)
                gridTrans.Rows.Remove(gridTrans.Rows[i]);
        }

        private void btnAddLoan_Click(object sender, EventArgs e)
        {
            DataTable dsTemp = new DataTable();
            dsTemp = (DataTable)gridTrans.DataSource;                
            if (!(dsTemp.Columns.Contains("Lendrate")))
            {
                dsTemp.Columns.Add("Lendrate");
                //dsTemp.Columns.Add("Fine");
                //dsTemp.Columns.Add("LibBal");
                //dsTemp.Columns.Add("MemBal");
                // dsTemp.Tables[0].Columns.Add("TransId");
                dsTemp.Columns.Add("RenewalDays");
            }
            DataRow rw = dsTemp.NewRow();
            rw["BookID"] = txtTransBookid.Text;
            rw["MemberID"] = txtTransMemId.Text;
            rw["LendDate"] = dateTransLoan.Text;
            rw["DueDate"] = dateTransDue.Text;
            if (dateTransReturn.Checked)
                rw["ReturnDate"] = dateTransReturn.Text;
            else
                rw["ReturnDate"] = DateTime.MinValue;


            rw["LendRate"] = txtTransLendRate.Text;
            //rw["Fine"] = txtTransFine.Text;
            //rw["LibBal"] = txtLibBal.Text;
            //rw["MemBal"] = txtMemBal.Text;
            //rw["LibBal"] = 0;
            //rw["MemBal"] = 0;
            rw["TransId"] = 0;
            rw["RenewalDays"] = txtRenewal.Text;
            dsTemp.Rows.Add(rw);

            gridTrans.DataSource = dsTemp;
        }

        private void btnLoan_Ret_Trans_Click(object sender, EventArgs e)
        {
            Entities.booktran bt = new Entities.booktran();
            
            StringBuilder arBookid = new StringBuilder();

            String strTemp = "";
            int? strFine = 0;
            int? strLendRate = 0;
            int? strBalance = 0;
            bool isMulLoan = false;

            if (gridTrans.RowCount > 0)
            {
                for (int i = 0; i < gridTrans.RowCount; i++)
                {
                    if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                    {
                        isMulLoan = true;
                        bt.BookID = gridTrans["BookID", i].Value.ToString();
                        if (arBookid.Length == 0)
                            strTemp = gridTrans["MemberID", i].Value.ToString();
                        else
                        {
                            if (strTemp != gridTrans["MemberID", i].Value.ToString())
                            {
                                MessageBox.Show("Loan processing can be done only for one member at a time");
                                return;
                            }
                        }

                        bt.MemberID = gridTrans["MemberID", i].Value.ToString();


                        if (bt.MemberID.Equals(""))
                        {
                            MessageBox.Show("MemberID cannot be empty");
                            return;
                        }
                        
                        if (Int32.Parse(memRep.GetMemberIDDetails(bt.MemberID).Rows[0]["Status"].ToString())<1)
                        {
                            if (MessageBox.Show("Do you want to continue?", "Inactive Member", MessageBoxButtons.YesNo) == DialogResult.No)
                                return;
                        }
                        bt.LendDate = DateTime.Parse(gridTrans["LendDate", i].Value.ToString()) ;
                        bt.DueDate = DateTime.Parse(gridTrans["DueDate", i].Value.ToString());

                        if (dateTransReturn.Checked)
                        {                            
                            MessageBox.Show("You cannot specify Return date for Loan Processing");
                            return;
                        }

                        if (transRep.GetLoanDetailsBooksMem(bt.BookID, bt.MemberID, true).Rows.Count > 0)
                        {
                            MessageBox.Show("Bookid " + bt.BookID + " already loaned out");
                            return;
                        }

                        if (transRep.GetLoanDetailsBooksMem(bt.BookID, bt.MemberID).Rows.Count > 0)
                        {
                            if (MessageBox.Show("Do you want to continue?", bt.BookID + " already read by member", MessageBoxButtons.YesNo) == DialogResult.No)
                                return;
                        }

                        bt.LendRate = Int32.Parse(gridTrans["LendRate", i].Value.ToString());
                        //bt.Fine = Int32.Parse(gridTrans["Fine", i].Value.ToString());
                        bt.Fine = 0;
                        //bt.LibBal = Int32.Parse(gridTrans["LibBal", i].Value.ToString());
                        //bt.MemBal = Int32.Parse(gridTrans["MemBal", i].Value.ToString());
                        bt.LibBal = 0;
                        bt.MemBal = 0;
                        bt.RenewalDays = Int32.Parse(gridTrans["RenewalDays", i].Value.ToString());

                        //if (!bt.Fine.Equals(""))
                        //    strFine = bt.Fine + strFine;
                        if (!bt.LendRate.Equals(""))
                            strLendRate = bt.LendRate + strLendRate;
                        //if (!bt.MemBal.Equals(""))
                        //    strBalance = bt.MemBal + strBalance;

                        arBookid.Append(bt.BookID + ",");
                        //updateLoanReturn(bt, false, false);

                    }
                }
                if (isMulLoan)
                {
                    if (arBookid.Length > 0) arBookid.Remove(arBookid.Length - 1, 1);
                    if (MessageBox.Show("Books Loaned:" + arBookid.ToString() + "\nMemberID:" + bt.MemberID + "\nLend Rate:" + strLendRate , "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < gridTrans.RowCount; i++)
                        {
                            if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                            {
                                bt.BookID = gridTrans["BookID", i].Value.ToString();
                                bt.MemberID = gridTrans["MemberID", i].Value.ToString();
                                bt.LendDate = DateTime.Parse(gridTrans["LendDate", i].Value.ToString());
                                bt.DueDate = DateTime.Parse(gridTrans["DueDate", i].Value.ToString());
                                //bt.ReturnDate = DateTime.Parse(dateTransReturn.Text).ToShortDateString();
                                bt.CreatedDate = DateTime.Now;
                                bt.LendRate = Int32.Parse(gridTrans["LendRate", i].Value.ToString());
                                //bt.Fine = Int32.Parse(gridTrans["Fine", i].Value.ToString());
                                bt.Fine = 0;
                                //bt.LibBal = Int32.Parse(gridTrans["LibBal", i].Value.ToString());
                                //bt.MemBal = Int32.Parse(gridTrans["MemBal", i].Value.ToString());
                                bt.LibBal = 0;
                                bt.MemBal = 0;
                                bt.RenewalDays = Int32.Parse(gridTrans["RenewalDays", i].Value.ToString());
                                transRep.InsertBookTrans(bt);
                            }

                        }
                        gridTrans.DataSource = transRep.GetTransDetails();
                        gridTrans.Refresh();
                        //bt.LendDate = "";
                        //bt.DueDate = "";
                        //bt.ReturnDate = "";
                        //LibBookTrans lbt = new LibBookTrans();
                        //lbt.displayTrans(bt, gridTrans);
                    }
                }
                
            }
        }

        private void btn_Ret_Trans_Click(object sender, EventArgs e)
        {
            Entities.booktran bt = new Entities.booktran();
            StringBuilder arBookid = new StringBuilder();
            String strTemp = "";
            int? strFine = 0;
            int? strLendRate = 0;
            int? strMemBalance = 0; int? strLibBalance = 0;
            bool isMulLoan = false;
            DataTable ds = new DataTable();
     
            if (gridTrans.RowCount > 0)
            {
                for (int i = 0; i < gridTrans.RowCount; i++)
                {
                    if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                    {
                        isMulLoan = true;
                        ds = transRep.GetTransIDDetails(Int32.Parse(gridTrans["TransID", i].Value.ToString()));
                        bt.BookID = ds.Rows[0]["BookID"].ToString();
                        if (arBookid.Length == 0)
                            strTemp = ds.Rows[0]["MemberID"].ToString();
                        else
                        {
                            if (strTemp != ds.Rows[0]["MemberID"].ToString())
                            {
                                MessageBox.Show("Return processing can be done only for one member at a time");
                                return;
                            }
                        }

                        bt.MemberID = ds.Rows[0]["MemberID"].ToString();
                        if (bt.MemberID.Equals(""))
                        {
                            MessageBox.Show("MemberID cannot be empty");
                            return;
                        }

                        bt.LendDate = DateTime.Parse(ds.Rows[0]["LendDate"].ToString());
                        bt.DueDate = DateTime.Parse(ds.Rows[0]["DueDate"].ToString());

                        if (dateTransReturn.Checked)
                            bt.ReturnDate = DateTime.Parse(dateTransReturn.Text);
                        else
                        {
                            MessageBox.Show("Please specify a Return Date");
                            return;
                        }
                        bt.LendRate = Int32.Parse(ds.Rows[0]["LendRate"].ToString());
                        //DateTime countDate=DateTime.Today-DateTime.Parse(ds.Rows[0]["DueDate"].ToString());
                        //int suggFine = Int32.Parse((DateTime.Today - DateTime.Parse(ds.Rows[0]["DueDate"].ToString())).TotalDays.ToString());
                        //if (suggFine > 0)
                        //suggFine = Int32.Parse(((suggFine / 15) * bt.LendRate*0.75).ToString());
                        //bt.Fine = Int32.Parse(txtTransFine.Text);
                        //bt.LibBal = Int32.Parse(txtLibBal.Text);
                        //bt.MemBal = Int32.Parse(txtMemBal.Text);
                        bt.Fine = 0;
                        bt.LibBal = 0;
                        bt.MemBal = 0;
                         
                        //if (!bt.Fine.Equals(""))
                            //strFine = bt.Fine;// +strFine;
                        if (!bt.LendRate.Equals(""))
                            strLendRate = bt.LendRate + strLendRate;
                        //if (!bt.MemBal.Equals(""))
                        //    strMemBalance = bt.MemBal; //+ strMemBalance;
                        //if (!bt.LibBal.Equals(""))
                        //    strLibBalance = bt.LibBal; //+ strLibBalance;
                        //strFine = Int32.Parse(gridTrans["fine", i].Value.ToString()) + strFine;
                        strFine = 0;
                        //bt.LendRate = "";
                        //bt.Fine = "";
                        //bt.LibBal = "";
                        strLibBalance=0;
                        strMemBalance = 0;
                        arBookid.Append(bt.BookID + ",");
                        //updateLoanReturn(bt, false, false);
                    }
                }
                if (isMulLoan)
                {
                    if (arBookid.Length > 0) arBookid.Remove(arBookid.Length - 1, 1);
                    if (MessageBox.Show("Books Returned:" + arBookid.ToString() + "\nMemberID:" + bt.MemberID + "\nLend Rate:" + strLendRate , "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < gridTrans.RowCount; i++)
                        {
                            //int tempID = 0;
                            if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                            {
                                Entities.booktran btTran = new booktran();                                
                                btTran.TRANSID = Int32.Parse(gridTrans["TransID", i].Value.ToString());
                                btTran.ReturnDate = DateTime.Parse(dateTransReturn.Text);
                                //btTran.Fine = strFine;
                                //btTran.Fine = Int32.Parse(txtTransFine.Text);
                                //btTran.LibBal = Int32.Parse(txtLibBal.Text);
                                //btTran.MemBal = Int32.Parse(txtMemBal.Text);
                                btTran.Fine = 0;
                                btTran.LibBal = 0;
                                btTran.MemBal = 0;
                                transRep.UpdateBookReturn(btTran, btTran.TRANSID);
                                //tempID = i;
                            }
                            //if (tempID > 0)
                            //{
                            //    Entities.booktran btTran = new booktran();
                            //    btTran.TRANSID = Int32.Parse(gridTrans["TransID", tempID].Value.ToString());
                            //    bt.CreatedDate = DateTime.Now;
                            //    btTran.ReturnDate = DateTime.Parse(dateTransReturn.Text);
                            //    btTran.Fine = Int32.Parse(txtTransFine.Text);
                            //    btTran.LibBal = Int32.Parse(txtLibBal.Text);
                            //    btTran.MemBal = Int32.Parse(txtMemBal.Text);
                            //    transRep.UpdateBookTrans(btTran, Int32.Parse(gridTrans["TransID", tempID].Value.ToString()));
                            //}

                        }
                        gridTrans.DataSource = transRep.GetTransDetails();
                        gridTrans.Refresh();                       
                    }
                }               
            }
        }

        private void btnTransUpdate_Click(object sender, EventArgs e)
        {
            Entities.booktran bt = new booktran();
            bt.BookID = txtTransBookid.Text;
            bt.MemberID = txtTransMemId.Text;
            bt.LendDate = DateTime.Parse(dateTransLoan.Text);
            bt.DueDate = DateTime.Parse(dateTransDue.Text);
            bt.ReturnDate =null;
            bt.CreatedDate = DateTime.Now;
            bt.LendRate = Int32.Parse(txtTransLendRate.Text);
            //bt.Fine = Int32.Parse(txtTransFine.Text);
            bt.Fine = 0;
            //bt.LibBal = Int32.Parse(txtLibBal.Text);
            //bt.MemBal = Int32.Parse(txtMemBal.Text);
            bt.LibBal = 0;
            bt.MemBal = 0;
            bt.RenewalDays = Int32.Parse(txtRenewal.Text);
            bt.TRANSID=Int32.Parse(lblTransId.Text);
            if(dateTransReturn.Checked)
                bt.ReturnDate = DateTime.Parse(dateTransReturn.Text);
            transRep.UpdateBookTrans(bt, bt.TRANSID);
            txtSearch_trans.Text = txtTransMemId.Text;
            gridTrans.DataSource=transRep.GetTransDetails(txtTransMemId.Text);
        }

        private void btnMemWish_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtTransMemId.Text))
            {
                mainTab.TabPages[0].Show();
                mainTab.SelectedIndex = 0;                
                gridBooks.DataSource = wishRep.SearchWishList(txtTransMemId.Text);
                txtSearch.Text = txtTransMemId.Text;
                
            }
        }

        private void btnFineCalc_Click(object sender, EventArgs e)
        {

            if (gridTrans.RowCount > 0)
            {
                for (int i = 0; i < gridTrans.RowCount; i++)
                {
                    if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                    {
                        string transMemberid = gridTrans["memberid", i].Value.ToString();
                        string transBookid = gridTrans["bookid", i].Value.ToString();

                        //gridTrans[gridTrans.Columns["fine"].Index, i].Value = transRep.CalculateFine(transBookid, transMemberid, chkRenewal.Checked);
                        MessageBox.Show(transRep.CalculateFine(transBookid, transMemberid, chkRenewal.Checked), "Fine for MemberID:" + transMemberid+ "and BookID:"+transBookid);

                        //if (bt.MemberID.Equals(""))
                        //{
                        //    MessageBox.Show("MemberID cannot be empty");
                        //    return;
                        //}



                    }

                }
                
                chkRenewal.Checked = false;
            }
        }

        private void leave_MemId_Trans(object sender, EventArgs e)
        {
            setTransMemName();            
        }

        private void setTransMemName()
        {
            DataTable ds = new DataTable();
            DataTable dsTrans = new DataTable();
            lblAlreadyRead.Text = "";
            ds = memRep.GetMemberIDDetails(txtTransMemId.Text);
            if (ds.Rows.Count > 0)
            {
                lblTransMemName.Text = ds.Rows[0]["MemberName"].ToString();
                lblPhone.Text = ds.Rows[0]["Mobile"].ToString();
            }
            dsTrans = transRep.GetLoanDetailsBooksMem(txtTransBookid.Text, txtTransMemId.Text);
            if(dsTrans.Rows.Count>0)
            {
                lblAlreadyRead.Text = "R";
            }
            lblBalAmt.Text = transRep.GetMemBalAmt(txtTransMemId.Text).ToString();
            lblLastVisitedvalue.Text = transRep.GetMemLastVisited(txtTransMemId.Text);
            lblMemberNotes.Text = memRep.GetMemberNotes(txtTransMemId.Text);
        }

        private void scanAndRead(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {                
                leaveBookID(sender, e);
                btnAddLoan_Click(sender, e);
                txtTransBookid.Text = "";
            }
        }
        #endregion


    }
}
