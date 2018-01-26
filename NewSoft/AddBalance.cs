using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entities;
using DALLayer;

namespace NewSoft
{
    public partial class AddBalance : Form
    {
        TransRepository transRep = new TransRepository();
        public AddBalance()
        {
            InitializeComponent();
           // this.txtMemberID.Text = this.ParentForm.MemberID;


        }

        private void btnAddBal_Click(object sender, EventArgs e)
        {
            Entities.balance bal = new Entities.balance();
            bal.Balance = Int32.Parse(txtAddBal.Text);
            bal.MemberID = txtMemberID.Text;
            bal.BalDate = DateTime.Parse(dateTimePicker1.Text);
            bal.CreatedDate = DateTime.Now;
            int retValue=transRep.AddBalance(bal);
            if (retValue > 0)
                this.Close();
            
        }
    }
}
