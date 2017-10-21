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

        public void initializeTransData(int transID)
        {
            if (gridTrans.Rows.Count > 0)
            {  
                DataTable ds = new DataTable();
                btnMemWish.Enabled = false;
                if (transID == 0)
                    ds = CommonUIMethods.ConvertGridToTable(gridTrans);
                else
                    ds = transRep.GetTransIDDetails(transID);
                lblMemberNotes.Text = memRep.GetMemberNotes(ds.Rows[0]["MemberID"].ToString()).Rows[0]["Notes"].ToString();
                lblAlreadyRead.Text = "";
                txtTransBookid.Text = ds.Rows[0]["BookID"].ToString();
                txtTransMemId.Text = ds.Rows[0]["MemberID"].ToString();                
                setTransMemName();
                lblTransId.Text = ds.Rows[0]["TransID"].ToString();
                dateTransLoan.Text = ds.Rows[0]["LendDate"].ToString();
                dateTransDue.Text = ds.Rows[0]["DueDate"].ToString();
                if(String.Compare(DateTime.MinValue.ToString(),ds.Rows[0]["ReturnDate"].ToString())==0)
                    dateTransReturn.Text = "";
                else
                    dateTransReturn.Text = ds.Rows[0]["ReturnDate"].ToString();
                txtTransLendRate.Text = ds.Rows[0]["LendRate"].ToString();
                txtTransFine.Text = ds.Rows[0]["Fine"].ToString();
                txtLibBal.Text = ds.Rows[0]["LibBal"].ToString();
                txtMemBal.Text = ds.Rows[0]["MemBal"].ToString();
                txtRenewal.Text = ds.Rows[0]["RenewalDays"].ToString();                
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
                initializeTransData(Int32.Parse(gridTrans["TransID", e.RowIndex].Value.ToString()));
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
            if (!(dsTemp.Columns.Contains("Fine")))
            {
                dsTemp.Columns.Add("Lendrate");
                dsTemp.Columns.Add("Fine");
                dsTemp.Columns.Add("LibBal");
                dsTemp.Columns.Add("MemBal");
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
            rw["Fine"] = txtTransFine.Text;
            rw["LibBal"] = txtLibBal.Text;
            rw["MemBal"] = txtMemBal.Text;
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
                        bt.Fine = Int32.Parse(gridTrans["Fine", i].Value.ToString());
                        bt.LibBal = Int32.Parse(gridTrans["LibBal", i].Value.ToString());
                        bt.MemBal = Int32.Parse(gridTrans["MemBal", i].Value.ToString());
                        bt.RenewalDays = Int32.Parse(gridTrans["RenewalDays", i].Value.ToString());

                        if (!bt.Fine.Equals(""))
                            strFine = bt.Fine + strFine;
                        if (!bt.LendRate.Equals(""))
                            strLendRate = bt.LendRate + strLendRate;
                        if (!bt.MemBal.Equals(""))
                            strBalance = bt.MemBal + strBalance;

                        arBookid.Append(bt.BookID + ",");
                        //updateLoanReturn(bt, false, false);

                    }
                }
                if (isMulLoan)
                {
                    if (arBookid.Length > 0) arBookid.Remove(arBookid.Length - 1, 1);
                    if (MessageBox.Show("Books Loaned:" + arBookid.ToString() + "\nMemberID:" + bt.MemberID + "\nLend Rate:" + strLendRate + "\nFine:" + strFine + "\nBalance:" + strBalance, "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                                bt.Fine = Int32.Parse(gridTrans["Fine", i].Value.ToString());
                                bt.LibBal = Int32.Parse(gridTrans["LibBal", i].Value.ToString());
                                bt.MemBal = Int32.Parse(gridTrans["MemBal", i].Value.ToString());
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
                //else
                //{
                //    if (dateTransReturn.Checked)
                //    {
                //        MessageBox.Show("You cannot specify Return date for Loan Processing");
                //        return;
                //    }

                //    if (transRep.GetLoanDetailsBooksMem(bt.BookID, bt.MemberID, true).Rows.Count > 0)
                //    {
                //        MessageBox.Show("Bookid " + txtTransBookid.Text + " already loaned out");
                //        return;
                //    }
                //    if (MessageBox.Show("Books Loaned:" + txtTransBookid.Text + "\nMemberID:" + txtTransMemId.Text + "\nLend Rate:" + txtTransLendRate.Text + "\nFine:" + txtTransFine.Text + "\nBalance:" + txtMemBal.Text + "\nRenewal Days:" + txtRenewal.Text, "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //    {
                //        bt.BookID = txtTransBookid.Text.ToString();
                //        bt.MemberID = txtTransMemId.Text;
                //        bt.LendDate = DateTime.Parse(dateTransLoan.Text);
                //        bt.DueDate = DateTime.Parse(dateTransDue.Text);
                //        bt.LendRate = Int32.Parse(txtTransLendRate.Text);
                //        bt.Fine = Int32.Parse(txtTransFine.Text);
                //        bt.LibBal = Int32.Parse(txtLibBal.Text);
                //        bt.MemBal = Int32.Parse(txtMemBal.Text);
                //        //bt.MemBal=txt
                //        bt.RenewalDays = Int32.Parse(txtRenewal.Text);
                //        transRep.InsertBookTrans(bt);
                //        gridTrans.DataSource = transRep.GetTransLoanDetails(bt.MemberID);
                //        //updateLoanReturn(bt, false, true);

                //        //bt.LendDate = "";
                //        //bt.DueDate = "";
                //        //bt.ReturnDate = "";

                //        //LibBookTrans lbt = new LibBookTrans();
                //        //lbt.displayTrans(bt, gridTrans);
                //    }
                //}
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
                        bt.LibBal = Int32.Parse(txtLibBal.Text);
                        bt.MemBal = Int32.Parse(txtMemBal.Text);

                        //if (!bt.Fine.Equals(""))
                            //strFine = bt.Fine;// +strFine;
                        if (!bt.LendRate.Equals(""))
                            strLendRate = bt.LendRate + strLendRate;
                        if (!bt.MemBal.Equals(""))
                            strMemBalance = bt.MemBal; //+ strMemBalance;
                        if (!bt.LibBal.Equals(""))
                            strLibBalance = bt.LibBal; //+ strLibBalance;


                        //bt.LendRate = "";
                        //bt.Fine = "";
                        //bt.LibBal = "";
                        arBookid.Append(bt.BookID + ",");
                        //updateLoanReturn(bt, false, false);
                    }
                }
                if (isMulLoan)
                {
                    if (arBookid.Length > 0) arBookid.Remove(arBookid.Length - 1, 1);
                    if (MessageBox.Show("Books Returned:" + arBookid.ToString() + "\nMemberID:" + bt.MemberID + "\nLend Rate:" + strLendRate + "\nMem Bal:" + strMemBalance + "\nLib Bal:" + strLibBalance, "Confirm Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < gridTrans.RowCount; i++)
                        {
                            //int tempID = 0;
                            if (gridTrans[0, i].EditedFormattedValue.ToString().Equals("True"))
                            {
                                Entities.booktran btTran = new booktran();                                
                                btTran.TRANSID = Int32.Parse(gridTrans["TransID", i].Value.ToString());
                                btTran.ReturnDate = DateTime.Parse(dateTransReturn.Text);                                
                                //btTran.Fine = Int32.Parse(txtTransFine.Text);
                                //btTran.LibBal = Int32.Parse(txtLibBal.Text);
                                //btTran.MemBal = Int32.Parse(txtMemBal.Text);
                                transRep.UpdateBookReturn(btTran, Int32.Parse(gridTrans["TransID", i].Value.ToString()));
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
            bt.Fine = Int32.Parse(txtTransFine.Text);
            bt.LibBal = Int32.Parse(txtLibBal.Text);
            bt.MemBal = Int32.Parse(txtMemBal.Text);
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
                txtTransMemName.Text = ds.Rows[0]["MemberName"].ToString();
                lblPhone.Text = ds.Rows[0]["Mobile"].ToString();
            }
            dsTrans = transRep.GetLoanDetailsBooksMem(txtTransBookid.Text, txtTransMemId.Text);
            if(dsTrans.Rows.Count>0)
            {
                lblAlreadyRead.Text = "R";
            }
            lblBalAmt.Text = transRep.GetMemBalAmt(txtTransMemId.Text).ToString();
            lblLastVisitedvalue.Text = transRep.GetMemLastVisited(txtTransMemId.Text);
            lblMemberNotes.Text = memRep.GetMemberNotes(txtTransMemId.Text).Rows[0]["Notes"].ToString();
        }
        #endregion

        
    }
}
