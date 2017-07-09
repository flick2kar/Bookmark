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
        private void btnAccGo_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ds=accRep.GetAccountDetails(dateAccStr.Text, dateAcc.Text);           
            lblAmtTrans.Text = ds.Rows[0]["lendrate"].ToString();
            lblAmtFine.Text = ds.Rows[0]["fine"].ToString();
            lblAccAmtBal.Text = ds.Rows[0]["LibBal"].ToString();
            lblAccMemBalValue.Text = ds.Rows[0]["MemBal"].ToString();
        }
    }
}
